import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { PremiumCalculatorService } from './service/premium-calculator.service';  
import { FormsModule, ReactiveFormsModule } from '@angular/forms';  
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PremiumCalculatorComponent } from './premium-calculator/premium-calculator.component';
import {  CurrencyPipe} from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    PremiumCalculatorComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,  
    ReactiveFormsModule,  
    HttpClientModule, 
    AppRoutingModule
  ],
  providers: [HttpClientModule, PremiumCalculatorService, CurrencyPipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
