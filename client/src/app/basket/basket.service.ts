import { IDeliveryOption } from './../shared/Models/IDeliveryOption';
import { IProduct } from './../shared/Models/Iproducts';
import { IBasket, IBasketItem, Basket, IBasketTotals } from './../shared/Models/IBasket';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
baseurl =  environment.apiUrl;
private basketsource = new BehaviorSubject<IBasket>(null)
basket$  = this.basketsource.asObservable();
private totalCostSource = new BehaviorSubject<IBasketTotals>(null)
TotalCost$  = this.totalCostSource.asObservable();
shippingprice = 0 ;

constructor(private http : HttpClient) { }
CreatePaymentIntend(){
  return this.http.post<IBasket>(this.baseurl +'Payment/'+ this.getCurrentBasketValue().id,{}).
  pipe(map((basket :IBasket) =>{
     this.basketsource.next(basket);
  }
  ))
}
setshippingprice(deliverymethod:IDeliveryOption){
this.shippingprice = deliverymethod.price;
const basket = this.getCurrentBasketValue();
basket.deliveryMethodID = deliverymethod.id;
basket.shippingPrice= deliverymethod.price;
this.colculateCost();
this.setBasket(basket);
}

  getbasket(id:any){
   return this.http.get(this.baseurl + 'basket?id=' + id).
   pipe(
     map((basket: IBasket) => {
       if(basket){
          this.basketsource.next(basket)
          this.shippingprice = basket.shippingPrice;
       }
          this.colculateCost()
     })
   );
  }
  setBasket(basket: IBasket) {
    return this.http.post(this.baseurl + 'basket', basket).subscribe((response: IBasket) => {
      this.basketsource.next(response);
      this.colculateCost();
    }, error => {
      console.log(error);
    });
  }
  incrementItemQuantity(item : IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const itemindex = basket.basketItems.findIndex(x => x.id === item.id);
    basket.basketItems[itemindex].quantity++;
    this.setBasket(basket);
  } 
  decrementItemQuantity(item : IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const itemindex = basket.basketItems.findIndex(x => x.id === item.id);
   if(basket.basketItems[itemindex].quantity > 1){
    basket.basketItems[itemindex].quantity--;
     this.setBasket(basket);
}
else{
  this.deleteItemFromBasket(item);
}
  } 
  deleteItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const itemindex = basket.basketItems.some(x => x.id === item.id);
    basket.basketItems = basket.basketItems.filter(x => x.id !== item.id);
    if(basket.basketItems.length > 0){
      this.setBasket(basket);
    }
    else{
      this.deleteBasket(basket);
    }
  }
  DeleteBasketFormAngular(){
    this.basketsource.next(null);
    this.colculateCost();
    localStorage.removeItem('basket_id');
  }
  deleteBasket(basket: IBasket) {
   this.http.delete(this.baseurl +  'basket?id=' + basket.id).subscribe(() =>
   {
     this.basketsource.next(null);
     this.totalCostSource.next(null);
     localStorage.removeItem('basket_id');
   },error => {
     console.log(error);
   }
   )
  }

public colculateCost(){
 const Shipping : number = this.shippingprice;
 const basket = this.getCurrentBasketValue();
 let TotalPrice =0;
 let SubTotal =0;
 if(basket){
 TotalPrice = basket.basketItems.reduce((a,b,) => (b.price * b.quantity) + a,0)
 SubTotal = TotalPrice + Shipping;
 }
 this.totalCostSource.next({TotalPrice,Shipping,SubTotal })
}

getCurrentBasketValue() {
    return this.basketsource.value;
  }

  addItemToBasket(item: IProduct, quantity = 1) {
    const itemToAdd: IBasketItem = this.convertFromProdctToBasket(item, quantity);
    let basket = this.getCurrentBasketValue();
    if (basket === null || basket.id === 'null' ) {
      basket = this.createBasket();
    }
    
    basket.basketItems = this.addOrUpdateItem(basket.basketItems, itemToAdd, quantity);
    this.setBasket(basket);
  }

private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
  const index = items.findIndex(i => i.id === itemToAdd.id);
  if (index === -1) {
    itemToAdd.quantity = quantity;
    items.push(itemToAdd);
  } else {
    items[index].quantity += quantity;
  }
  return items;
}
 changeBasketIDByLogin(email:string){
   const basket = this.getCurrentBasketValue();
   if (basket !== null  ) {
    basket.id = email;
   localStorage.setItem('basket_id',email)
   this.setBasket(basket);
     }
     else{
       localStorage.setItem('basket_id',email)
       const id = localStorage.getItem('basket_id');
       if(id !== null && id !== 'null'){
       this.getbasket(id).subscribe(()  =>
    {
    },
    error =>{
      console.log(error)
    })}
     }
 }
  createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id',basket.id);  
    return basket;
  }
  convertFromProdctToBasket(item: IProduct ,quantity :number): IBasketItem {
   return{
    id: item.id,
    productName: item.name,
    price: item.price,
    pictureUrl: item.pictureUrl,
    brand:item.productBrand,
    type: item.productType,
    quantity,
   }
  }
}
