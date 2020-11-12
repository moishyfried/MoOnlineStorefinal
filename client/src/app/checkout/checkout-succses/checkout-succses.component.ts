import { Iorder } from './../../shared/Models/Order';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-checkout-succses',
  templateUrl: './checkout-succses.component.html',
  styleUrls: ['./checkout-succses.component.scss']
})
export class CheckoutSuccsesComponent implements OnInit {
  order:Iorder;
  constructor(private router :Router) {
    const navigation = router.getCurrentNavigation();
    const  state = navigation && navigation.extras && navigation.extras.state;
    if(state){
      this.order = state as Iorder;
    }
   }

  ngOnInit(): void {
  }

}
