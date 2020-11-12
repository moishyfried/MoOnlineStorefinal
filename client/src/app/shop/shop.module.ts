import { CoreModule } from './../core/core.module';
import { ShopRoutingModule } from './shop-routing.module';
import { SharedModule } from './../shared/shared.module';
import { NgModule, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { ProductdetailComponent } from './productdetail/productdetail.component';



@NgModule({
  declarations: [ShopComponent, ProductItemComponent, ProductdetailComponent],
  imports: [
    CommonModule,
    SharedModule,
    ShopRoutingModule,
    CoreModule
  ],
 
})
export class ShopModule { }
