import { IBasketItem, IBasketTotals } from './../shared/Models/IBasket';
import { BasketService } from './basket.service';
import { Component, OnInit } from '@angular/core';
import { IBasket } from '../shared/Models/IBasket';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {
  basket$: Observable<IBasket>;
  basketTotals$: Observable<IBasketTotals>;
  constructor(private basketService : BasketService) { }

  ngOnInit(): void {
  this.basket$ =  this.basketService.basket$;
  this.basketTotals$ = this.basketService.TotalCost$;
  }
decermentQuantity(item: IBasketItem){
  this.basketService.decrementItemQuantity(item)
}
incrmentQuantity(item: IBasketItem){
  this.basketService.incrementItemQuantity(item)
}
removeItemFromBasket(item: IBasketItem){
  this.basketService.deleteItemFromBasket(item);
}
}
