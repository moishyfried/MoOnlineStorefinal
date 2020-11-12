import { BasketService } from './../../basket/basket.service';
import { IDeliveryOption } from './../../shared/Models/IDeliveryOption';
import { CheckoutService } from './../checkout.service';
import { FormGroup } from '@angular/forms';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss']
})
export class CheckoutDeliveryComponent implements OnInit {
 @Input() checkoutform : FormGroup
deliveryOptions:IDeliveryOption[];
  constructor(private checkoutservice :CheckoutService,private basketservice:BasketService) { }

  ngOnInit(): void {
    this.checkoutservice.GetDeliveryOptions().subscribe((dm :IDeliveryOption[]) =>{ 
      this.deliveryOptions = dm;
    },error => {
      console.log(error);
    })
  }
setDeliveryCost(shippingoption:IDeliveryOption){
this.basketservice.setshippingprice(shippingoption);
}
}
