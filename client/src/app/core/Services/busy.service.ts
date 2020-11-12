import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {

  constructor(private ngxspinnerservices: NgxSpinnerService) { }
  busyrequestCount = 0;

  busy(){
this.busyrequestCount++;
this.ngxspinnerservices.show(undefined);
  }
  delay(){
this.busyrequestCount--;
if(this.busyrequestCount <= 0)
{
  this.ngxspinnerservices.hide();
}
  }
}
