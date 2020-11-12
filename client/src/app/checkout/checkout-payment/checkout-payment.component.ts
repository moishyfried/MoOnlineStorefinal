import { Router, NavigationExtras } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { BasketService } from './../../basket/basket.service';
import { CheckoutService } from './../checkout.service';
import { Component, OnInit, Input, ViewChild, AfterViewInit, ElementRef, OnDestroy } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
declare var Stripe;
@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements  AfterViewInit, OnDestroy{
  @Input() checkOutForm : FormGroup;
  @ViewChild('cardNumber',{static :true} ) cardNumberElement : ElementRef;
  @ViewChild('cardExpiry',{static :true} ) cardExpiryElement : ElementRef;
   @ViewChild('cardCvc',{static :true} ) cardCvcElement : ElementRef;
   stripe:any;
   cardNumber:any;
   cardExpiry:any;
   cardCvc :any;
   cardErrors:any;
   cardHandler = this.onChange.bind(this)
   loading : boolean = false;
   cardNumberComplete = false;
   cardExpiryComlete = false;
   cradCvcComplete = false;


  constructor(
    private checkoutservice:CheckoutService,
    private basketservice:BasketService,
    private toaster :ToastrService,
    private router:Router ) { }

  ngAfterViewInit() {

    this.stripe =  Stripe('pk_test_51HPTRhCxfGGKD59DcG9UF5ofk3IskBJOXzf5gV1UmhnOEwSYqvGZr54oxkOshFHZeL0CQL9546kpmcHdnUL7sim800YhQ1XRnA');
   const elements = this.stripe.elements();

   this.cardNumber = elements.create('cardNumber');
   this.cardNumber.mount(this.cardNumberElement.nativeElement);
   this.cardNumber.addEventListener('change',this.cardHandler)

   this.cardExpiry = elements.create('cardExpiry');
   this.cardExpiry.mount(this.cardExpiryElement.nativeElement);
   this.cardExpiry.addEventListener('change',this.cardHandler)

   this.cardCvc = elements.create('cardCvc');
   this.cardCvc.mount(this.cardCvcElement.nativeElement);
   this.cardCvc.addEventListener('change',this.cardHandler)
  }

  ngOnDestroy(){
    this.cardNumber.destroy();
    this.cardExpiry.destroy();
    this.cardCvc.destroy();
  }
  onChange(event){
if(event.error){
  this.cardErrors = event.error.message;}
  else{
    this.cardHandler = null
  }
switch (event.elementType) {
  case 'cardNumber':
    this.cardNumberComplete = event.complete
    break;
    case 'cardCvc':
      this.cradCvcComplete = event.complete
      break
      case 'cardExpiry':
      this.cardExpiryComlete = event.complete
      break;
  default:
    break;
}  }

  createPaymetintend(){
    this.basketservice.CreatePaymentIntend().subscribe((basket:any) => {
    },error => {
      this.toaster.error(error.message)
      console.log(error)
    })  
  }
async submitOrder(){
  try {
    this.loading = true;
    const  ordertocreate = this.GavetheOrder();
   const CreatedOrder = await this.CreateOrder(ordertocreate);
   const paymentResult = await this.ConfirmPaymentWithStripe();

   if(paymentResult.paymentIntent){
    this.toaster.success('Order Created Sucsesfly');
   this.basketservice.deleteBasket(this.basketservice.getCurrentBasketValue());
   const navigationextras :NavigationExtras = {state: CreatedOrder};
   this.router.navigate(['checkout/success'],navigationextras)
   
 }else{
   this.toaster.error('payment failed ' + paymentResult.error.message)
 }this.loading= false;
  } catch (error) {
    console.log(error)
  }

}
 private async ConfirmPaymentWithStripe() {
   return this.stripe.confirmCardPayment(this.basketservice.getCurrentBasketValue().clientSecret,{
    payment_method:{
     card: this.cardNumber,
     billing_details:{
       name:this.checkOutForm.get('paymentform').get('nameoncard').value
     }
    } 
  });
  }
  private async CreateOrder(ordertocreate) {
 return   this.checkoutservice.createOrder(ordertocreate).toPromise();
  }
 private GavetheOrder() {
    return {
        basketId: localStorage.getItem('basket_id'),
        deliveryMethodId :+ this.checkOutForm.get('deliveryform').get('deliverymethod').value,
        shipToAddress: this.checkOutForm.get('addressform').value
    }
  }
}
