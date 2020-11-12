import { SharedModule } from './../shared/shared.module';
import { BasketModule } from './../basket/basket.module';
import { RouterModule } from '@angular/router';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { ToastrModule } from 'ngx-toastr';
import { TestErrorComponent } from './test-error/test-error.component';
import { SectionHeaderComponent } from './section-header/section-header.component';
import {BreadcrumbModule} from 'xng-breadcrumb';
import { FooterComponent } from './footer/footer.component';

@NgModule({
  declarations: [NavBarComponent, NotFoundComponent, ServerErrorComponent, TestErrorComponent, SectionHeaderComponent, FooterComponent],
  imports: [
    CommonModule,
    BasketModule,
    SharedModule,
    RouterModule,
    BreadcrumbModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    })
  ],
  exports: [NavBarComponent,SectionHeaderComponent,FooterComponent]
})
export class CoreModule { }
