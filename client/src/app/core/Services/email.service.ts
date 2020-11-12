import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmailService {
  baseurl = environment.apiUrl;
  
  constructor(private http :HttpClient) { }

  sendEmail(email:any){
    return this.http.post(this.baseurl + 'Email/sendemail',email);
  }
}
