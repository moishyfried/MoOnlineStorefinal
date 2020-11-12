import { Iorder, IOrderToCreate } from './../shared/Models/Order';
import { IDeliveryOption } from './../shared/Models/IDeliveryOption';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
baseurl = environment.apiUrl;

  constructor(private http: HttpClient) { }


  GetDeliveryOptions(){
    return this.http.get(this.baseurl + 'orders/deliveryMethods').pipe(
      map((dm: IDeliveryOption[]) => {
        return dm.sort((a, b) => b.price - a.price);
       } ));
  }

  createOrder(order:IOrderToCreate){
    return this.http.post(this.baseurl + 'orders',order);
  }
}

