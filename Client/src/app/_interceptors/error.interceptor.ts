import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  //for some errors, use router to redirect user to error page
  //for other errors, display toast notification
  constructor(private router:Router, private toastr: ToastrService) {}

  //intercept the HTTP request or the response                  returns an observable
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error =>   //error = HttpErrorResponse seen in browser console
      {  
        //if there is an error, catch error based on type of error
        if (error)
        { 
          switch (error.status) 
          {
            case 400:

              //400 validation error (empty form inputs)
              if (error.error.errors) 
              {
                //flatten array of validation errors into 1D array --> modalStateErrors=['Email is req', 'Name is req', 'Password is req.']
                const modalStateErrors = [];
                for (const key in error.error.errors) 
                {
                  if (error.error.errors[key]) {
                    modalStateErrors.push(error.error.errors[key]) 
                  }
                }
                //throw list/array of errors back to component
                //use es2019 in tsconfig.json for flat() to be valid
                throw modalStateErrors.flat();
              } 
              
              //400 bad request error
              else 
              {
                this.toastr.error(error.statusText, error.status);
              }
              break;
              
              //401 unauthorized- wrong password/email
              case 401:
                this.toastr.error(error.status, error.error);
                break;

              //404 not found
              case 404:
                this.router.navigateByUrl('/not-found');
                break;
              
              //500 server error
              case 500:
                //get details of error from api by setting state within router
                const navigationExtras: NavigationExtras = {state:{error:error.error}};
                //navigate to server error paage
                this.router.navigateByUrl('/server-error', navigationExtras);
                break;
            default:
              this.toastr.error('Something unexpected occurred!');
              console.log(error);
              break;
          }
        }
        //if error not caught,throw error
        return throwError(error);
      })
    );
  }
}
