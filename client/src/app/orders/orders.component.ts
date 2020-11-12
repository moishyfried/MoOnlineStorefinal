import { Iorder } from './../shared/Models/Order';
import { OrdersService } from './orders.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
orders : Iorder[];
  constructor(private orderservice:OrdersService) { }

  ngOnInit(): void {
  this.loadorder();
  }
loadorder(){
this.orderservice.loadorders().subscribe((order:Iorder[]) => {
  this.orders = order},
error => {
  console.log(error)
})
}
}
