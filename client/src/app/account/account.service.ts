import { BasketService } from './../basket/basket.service';
import { IAddress } from './../shared/Models/IAddress';
import { IUser } from './../shared/Models/Iuser';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject, of } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
baseurl = environment.apiUrl;
private usersource = new ReplaySubject<IUser>(1)
user$  = this.usersource.asObservable();


  constructor(private http :HttpClient,private router:Router,private basket:BasketService) { }
  
  loaduser(token:string){
    if(!token){
      this.usersource.next(null)
      return of(null);
    }
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
 
   return this.http.get<IUser>(this.baseurl + 'account',{headers}).pipe(
      map((user :IUser) => {
        if(user){
          localStorage.setItem('token',user.token); 
          localStorage.setItem('user_email',user.email);
          this.usersource.next(user);
        }
      })
    )
  }
login(value :any){
return this.http.post(this.baseurl + 'account/login',value).pipe(
  map((user:IUser) => {
 if(user){
 localStorage.setItem('token',user.token),
 localStorage.setItem('user_email',user.email),
 this.basket.changeBasketIDByLogin(user.email);
  this.usersource.next(user)

  }}))}
  

  register(value :any){
    return this.http.post(this.baseurl + 'account/register',value).pipe(
      map((user:IUser) => {
     if(user){
     localStorage.setItem('token',user.token),
     localStorage.setItem('user_email',user.email),
    this.usersource.next(user)
      }}))}

      
      logout(){
        localStorage.removeItem('token');
        localStorage.removeItem('user_email');
        this.basket.DeleteBasketFormAngular();
        this.usersource.next(null);
        this.router.navigateByUrl('/')
      }
      checkemailexists(email){
        return this.http.get(this.baseurl + 'account/emailexists?email=' + email)
      }
      GetUserAddress(){
       return this.http.get<IAddress>(this.baseurl + 'Account/address');
      }
        updateUserAddress(address:IAddress){
        return this.http.put<IAddress>(this.baseurl + 'Account/address',address);    
       }
}

