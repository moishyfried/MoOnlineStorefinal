import { AccountService } from './../../account/account.service';
import { IUser } from './../../shared/Models/Iuser';
import { IBasketTotals, IBasket } from './../../shared/Models/IBasket'; 
import { BasketService } from './../../basket/basket.service';
import { Component, OnInit } from '@angular/core';
import {  Observable } from 'rxjs';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  basket$: Observable<IBasket>;
  user$: Observable<IUser>;
  constructor(private basketService: BasketService,private accountservice : AccountService) { }

  ngOnInit() {
    this.basket$ = this.basketService.basket$;
    this.user$ = this.accountservice.user$;
  }
logout(){
  this.accountservice.logout();
  
}
}
