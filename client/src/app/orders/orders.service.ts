import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Iorder } from '../shared/Models/Order';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(private http:HttpClient) { }
  baseurl = environment.apiUrl;
   
  
  loadorders()
  {
   return this.http.get<Iorder[]>(this.baseurl + 'orders');}
  
   loadorderdetail(id:number){
     return this.http.get(this.baseurl +'orders/'+id);
   }
}

  
