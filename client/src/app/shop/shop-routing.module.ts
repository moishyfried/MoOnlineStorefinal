import { BreadcrumbService } from 'xng-breadcrumb';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ShopComponent } from './shop.component';
import { ProductdetailComponent } from './productdetail/productdetail.component';


const routes: Routes = [
  {path: '',component: ShopComponent},
  {path: ':id',component: ProductdetailComponent , data: {breadcrumb : {alias: 'productDetails'}}},
  ];      
  
@NgModule({
  declarations: [],
  imports: [
   RouterModule.forChild(routes)
  ],
  exports:[RouterModule]
}) 

  
export class ShopRoutingModule { }
