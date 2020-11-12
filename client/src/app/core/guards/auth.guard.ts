import { AccountService } from './../../account/account.service';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountservice :AccountService,private router :Router){}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean>
    {
      return this.accountservice.user$.pipe(
        map(auth => {
          if (auth) {
            return true;
          }
          this.router.navigate(['account/login'], {queryParams: {returnUrl: state.url}});
        })
      );
  }
  
}
