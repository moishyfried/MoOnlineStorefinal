import { IUser } from './shared/Models/Iuser';
import { AccountService } from './account/account.service';
import { BasketService } from './basket/basket.service';
import { Component,OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private basketservice : BasketService,private accountservice : AccountService){ }
  title = 'client';

  ngOnInit(): void {
  this.loaduser();
  this.loadbasket();
  }

  private loadbasket(){
    const id = localStorage.getItem('basket_id');
    if(id !== null && id !== 'null'){
    this.basketservice.getbasket(id).subscribe(()  =>
    {
    },
    error =>{
      console.log(error)
    })}
  }
private loaduser(){
const token  = localStorage.getItem('token');
this.accountservice.loaduser(token).subscribe((user:IUser) => 
{
},error =>{
  console.log(error)
})
}}