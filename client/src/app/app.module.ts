import { JwtInterceptor } from './core/interceptor/jwt.Interceptor';
import { HomeModule } from './home/home.module';
import { loadingInterceptor } from './core/interceptor/loading.interceptor';
import { ErrorInterceptor } from './core/interceptor/error.interceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CoreModule } from './core/core.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {NgxSpinnerModule} from 'ngx-spinner';
@NgModule({ 
  declarations: [
       AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    RouterModule,
    NgxSpinnerModule,
    HomeModule
  ],
  providers: [{provide:HTTP_INTERCEPTORS,useClass: ErrorInterceptor,multi:true}, 
              {provide:HTTP_INTERCEPTORS,useClass: loadingInterceptor ,multi:true},
              {provide:HTTP_INTERCEPTORS,useClass: JwtInterceptor ,multi:true}],

  bootstrap: [AppComponent]
})
export class AppModule { }
