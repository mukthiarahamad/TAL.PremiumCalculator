import { Component, OnInit, ViewChild, AfterViewChecked } from '@angular/core';
import { Occupation } from '../Class/occupation';
import { NgForm } from '@angular/forms';
import {PremiumCalculatorService} from '../service/premium-calculator.service';
import { PremiumParameters } from '../Class/premiumparameters';

@Component({
  selector: 'app-premium-calculator',
  templateUrl: './premium-calculator.component.html',
  styleUrls: ['./premium-calculator.component.less']
})
export class PremiumCalculatorComponent implements OnInit {

  isError: boolean = false;
  allOccupations: Occupation[];
  premiumParamModel:PremiumParameters; 

  constructor(private premiumCalculatorService: PremiumCalculatorService) { }

  ngOnInit() {
    this.premiumParamModel = new PremiumParameters(); 
    this.loadAllOccupations();
  }

  loadAllOccupations() {
    debugger;
    this.premiumCalculatorService.getOccupations().subscribe(result => {
      this.allOccupations = result;
    }, error => {
        // we can catch different exceptions according to error codes
        // for now just implemented general exception
        console.log(error);
        this.isError = true;
      });
  }

  calculatePremium() {

  }

}
