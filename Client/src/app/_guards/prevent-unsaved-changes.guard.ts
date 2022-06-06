import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { UserEditComponent } from '../user-edit/user-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  
  canDeactivate(component: UserEditComponent): Observable<boolean> | boolean {

    if (component.editForm.dirty){
      return confirm('Are you sure you want to continue? Any unsaved changes will be lost.')
    }
    return true;
  }
  
}
