import { CdkStepper } from '@angular/cdk/stepper';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { BasketService } from './../../basket/basket.service';
import { IBasket } from './../../shared/Models/IBasket';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.scss']
})
export class CheckoutReviewComponent implements OnInit {
@Input() appstepper:CdkStepper;
  constructor(private basketservice :BasketService,private toster:ToastrService) { }
  basket$ :Observable<IBasket>;
  ngOnInit(): void {
  this.basket$ = this.basketservice.basket$; 

  }
createPaymetintend(){
  this.basketservice.CreatePaymentIntend().subscribe((basket:any) => {
    this.appstepper.next();
  },error => {
    this.toster.error(error.message)
    console.log(error)
  })  
}
}
