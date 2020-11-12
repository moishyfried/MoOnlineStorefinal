import { CheckoutSuccsesComponent } from './checkout-succses/checkout-succses.component';
import { CheckoutComponent } from './checkout.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

const routes :Routes= [
  {path :'',component : CheckoutComponent},
  {path :'success',component : CheckoutSuccsesComponent}

]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports:[RouterModule]
})
export class CheckoutRoutingModule { }
