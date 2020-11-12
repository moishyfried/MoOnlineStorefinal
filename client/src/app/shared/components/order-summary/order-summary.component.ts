import { Iorder } from './../../Models/Order';
import { BasketService } from './../../../basket/basket.service';
import { Observable } from 'rxjs';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { IBasket, IBasketItem } from '../../Models/IBasket';

@Component({
  selector: 'app-order-summary',
  templateUrl: './order-summary.component.html',
  styleUrls: ['./order-summary.component.scss']
})
export class OrderSummaryComponent implements OnInit {

@Output() decerment : EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
@Output() incerment : EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
@Output() remove : EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
@Input() isbasket = true;
@Input() isorder = false;
@Input() items : IBasket[] | Iorder[]= [];


  constructor(private basketservice : BasketService) { }

  ngOnInit(): void {

  }
  decermentQuantity(item:IBasketItem){
    this.decerment.emit(item);
  }
  incermentQuantity(item:IBasketItem){
    this.incerment.emit(item);
  }
  removeItemFromBasket(item:IBasketItem){
    this.remove.emit(item);
  }
}
