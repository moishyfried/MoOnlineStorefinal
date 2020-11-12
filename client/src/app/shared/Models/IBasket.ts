import {v4 as uuidv4} from 'uuid';

export interface IBasket {
    id: string;
    basketItems: IBasketItem[];
    deliveryMethodID?: number;
    shippingPrice?: number ;
    clientSecret?: string;
    paymentIntendID?: string;
}

export interface IBasketItem {
    id: number;
    productName: string;
    price: number;
    quantity: number;
    pictureUrl: string;
    brand: string;
    type: string;
}
const email:string = localStorage.getItem('user_email')
let basketid = '';
if(!email || email === ''){
    basketid = uuidv4();
}
else basketid = email
export class Basket implements IBasket {
    id = basketid;
    basketItems: IBasketItem[] = [];
}
export interface IBasketTotals{
    TotalPrice :number;
    Shipping : number;
    SubTotal : number;
} 
