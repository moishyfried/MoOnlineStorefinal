import { Component, OnInit, ViewChild, ElementRef, Input, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent implements OnInit ,ControlValueAccessor{
 @ViewChild('input',{static : true}) input :ElementRef;
 @Input() type = 'text';
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
