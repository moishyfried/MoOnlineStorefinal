import { ControlValueAccessor, NgControl } from '@angular/forms';
import { Component, ElementRef, Input, OnInit, Self, ViewChild } from '@angular/core';

@Component({
  selector: 'app-text-erea',
  templateUrl: './text-erea.component.html',
  styleUrls: ['./text-erea.component.scss']
})
export class TextEreaComponent implements OnInit,ControlValueAccessor {
    @ViewChild('input',{static : true}) input :ElementRef;
    @Input() label : string;
     constructor(@Self() public controldir  : NgControl) { 
       this.controldir.valueAccessor = this;
     }
   
     ngOnInit(): void {
      const control = this.controldir.control;
      const validators  = control.validator ? [control.validator] : [];
      const asyncvalidators = control.asyncValidator ? [control.asyncValidator] : [];
   
     control.setValidators(validators);
     control.setAsyncValidators(asyncvalidators);
     control.updateValueAndValidity;
     }
   onchange(event){
   }
   ontouched(){}
     writeValue(obj: any): void {
       this.input.nativeElement.value = obj || '';
     }
     registerOnChange(fn: any): void {
       this.onchange= fn;
     }
     registerOnTouched(fn: any): void {
      this.ontouched = fn;
     }

}
