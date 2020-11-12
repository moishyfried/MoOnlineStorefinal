import { Observable } from 'rxjs';
import { BasketService } from './../basket/basket.service';
import { IAddress } from './../shared/Models/IAddress';
import { AccountService } from './../account/account.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { IBasketTotals } from '../shared/Models/IBasket';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {
checkoutform : FormGroup;
basketTotals$  : Observable<IBasketTotals>;
  constructor(private fb:FormBuilder,private accountservice :AccountService,private BasketService: BasketService ) { }

  ngOnInit(): void {
    this.createformgroup();
    this.filltheaddress();this.fillOutDeliveryOption();
    this.basketTotals$ = this.BasketService.TotalCost$;
  }
createformgroup(){
  this.checkoutform = this.fb.group({
    addressform:this.fb.group({
      firstName : [null,Validators.required],
      lastName : [null,Validators.required],
      street : [null,Validators.required],
      apartment : [null,Validators.required],
      city : [null,Validators.required],
      state : [null,Validators.required],
      zipcode : [null,Validators.required]
    }),
    deliveryform : this.fb.group({
      deliverymethod : [null,Validators.required]
    }),
    paymentform:this.fb.group({
   nameoncard:[null,Validators.required]
    })
  })
}
filltheaddress(){
  this.accountservice.GetUserAddress().subscribe((address:IAddress) => {
if(address){
  this.checkoutform.get('addressform').patchValue(address)
  }
}
    ,error => {
    console.log(error)
  })
}

fillOutDeliveryOption(){
const basket = this.BasketService.getCurrentBasketValue();
if(basket.deliveryMethodID !== null){
  this.checkoutform.get('deliveryform').get('deliverymethod')
.patchValue(basket.deliveryMethodID.toString())
}

}
}
