import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from './../../basket/basket.service';
import { IProduct } from './../../shared/Models/Iproducts';
import { ShopService } from './../shop.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-productdetail',
  templateUrl: './productdetail.component.html',
  styleUrls: ['./productdetail.component.scss']
})
export class ProductdetailComponent implements OnInit {
product:IProduct
quantity = 1; 
  constructor(private shopservice : ShopService, 
    private activateroute : ActivatedRoute,
    private basketservice: BasketService,
    private bsService : BreadcrumbService
   ) 
    { }
  ngOnInit() {
    this.loadProduct();
  }
loadProduct(){
  return this.shopservice.getProduct(+this.activateroute.snapshot.paramMap.get('id')).
  subscribe(product => {
    this.product = product
  this.bsService.set('@productDetails',product.name)
  },error => {
    console.log(error)
  })}
  addItemToBasket(item : IProduct){
this.basketservice.addItemToBasket(item,this.quantity);
  }
  incrmentQuantity(){
this.quantity++;
  } 
  decermentQuantity(){
    if(this.quantity > 1){
      this.quantity--;
    }
   }
}
