import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-ordertotal',
  templateUrl: './ordertotal.component.html',
  styles: [
  ]
})
export class OrdertotalComponent implements OnInit {
 @Input() shippingPrice: number;
 @Input() subtotal: number ;
 @Input() total: number;
  constructor() { }

  ngOnInit(): void {
  }

}
