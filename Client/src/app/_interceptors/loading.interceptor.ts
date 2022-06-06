import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { BusyService } from '../_services/busy.service';
import { delay, finalize } from 'rxjs/operators';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyService:BusyService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    //call busy method when reqeust is sent
    this.busyService.busy();

    //when request comes back
    return next.handle(request).pipe(
      //add fake delay
      delay(1000),
      //set busy service to idle
      finalize(()=>{
        this.busyService.idle();
      })
    );
  }
}
