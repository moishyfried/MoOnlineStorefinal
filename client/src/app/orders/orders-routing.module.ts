import { OrderDetailComponent } from './order-detail/order-detail.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersComponent } from './orders.component';

const routes :Routes = [{path:'',component:OrdersComponent},
{path: ':id', component: OrderDetailComponent, data: {breadcrumb: {alias: 'OrderDetailed'}}}]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports:[RouterModule]
})
export class OrdersRoutingModule { }
