import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagerHeaderComponent } from './components/pager-header/pager-header.component';
import { PagerComponent } from './components/pager/pager.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import {CarouselModule} from 'ngx-bootstrap/carousel';
import { OrdertotalComponent } from './components/ordertotal/ordertotal.component'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import { TextInputComponent } from './components/text-input/text-input.component';
import {CdkStepperModule} from '@angular/cdk/stepper';
import { StepperComponent } from './stepper/stepper.component';
import { OrderSummaryComponent } from './components/order-summary/order-summary.component';
import { SectionHeaderComponent } from './components/section-header/section-header.component';
import { ContactComponent } from './components/contact/contact.component';
import { TextEreaComponent } from './components/text-erea/text-erea.component';

@NgModule({
  declarations: [PagerHeaderComponent, PagerComponent,OrdertotalComponent, TextInputComponent, StepperComponent, OrderSummaryComponent, SectionHeaderComponent, ContactComponent, TextEreaComponent],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
     CarouselModule.forRoot(),
     BsDropdownModule.forRoot(),
     ReactiveFormsModule,
     FormsModule,
     CdkStepperModule,
     RouterModule
  ],
  exports: [PagerComponent,
    ReactiveFormsModule,
    FormsModule,
    PagerHeaderComponent,
    PaginationModule,
    CarouselModule,
    OrdertotalComponent,
   BsDropdownModule,
  TextInputComponent,
  CdkStepperModule,
  StepperComponent,
  OrderSummaryComponent
]
})
export class SharedModule { }
