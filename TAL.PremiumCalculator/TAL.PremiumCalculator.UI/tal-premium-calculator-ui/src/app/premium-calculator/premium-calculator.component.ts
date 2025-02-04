import { Component, OnInit, ViewChild, AfterViewChecked } from '@angular/core';
import { PremiumCalculatorService } from '../service/premium-calculator.service';
import { Occupation } from '../Class/occupation';
import { NgForm } from '@angular/forms';
import { PremiumParameters } from '../Class/premiumparameters';
import { CurrencyPipe} from '@angular/common';

@Component({
  selector: 'app-premium-calculator',
  templateUrl: './premium-calculator.component.html',
  styleUrls: ['./premium-calculator.component.less']
})
export class PremiumCalculatorComponent implements OnInit, AfterViewChecked {

  @ViewChild('premiumForm', {static: false}) currentForm: NgForm;
  premiumForm: NgForm;
  monthlyPremium = 0.0;
  isError: boolean = false;
  isValidationError: boolean = false;
  isOccupationLoaded: boolean = false;
  allOccupations: Occupation[];
  premiumParamModel:PremiumParameters; 
  dateOfBirth: string;
  formattedAmount: any;

  constructor(private premiumCalculatorService: PremiumCalculatorService,private currencyPipe : CurrencyPipe) { }

  ngOnInit() {
    this.premiumParamModel =  new PremiumParameters();
    this.loadAllOccupations();
    this.dateOfBirth = new Date().toISOString().slice(0, 10);
  }

  ngAfterViewChecked() {
    this.formChanged();
    if(this.premiumForm?.valid && this.isValidationError) {
      this.isValidationError = !this.isValidationError;
      return;
    }
  }

  calculateAge() {
    if (this.premiumParamModel.DOB) {
      var timeDiff = Math.abs(Date.now() - new Date(this.premiumParamModel.DOB).getTime());
         this.premiumParamModel.Age = Math.floor(timeDiff / (1000 * 3600 * 24) / 365.25);
        }
  }

  transformAmount(element: any){
    this.formattedAmount = this.currencyPipe.transform(this.premiumParamModel.SumInsured, '$');
    element.target.value = this.formattedAmount;
}

  formChanged() {
    if(this.currentForm == this.premiumForm){
      return;
    }
    this.premiumForm = this.currentForm;

    if(this.premiumForm) {
      this.premiumForm?.valueChanges?.subscribe(
        data => this.onValueChanged(data)
      );
    }
  }

  onValueChanged(data? : any){
    if(!this.premiumForm) { return; }
    const form = this.premiumForm.form;
    for(const field in this.formErrors){
      const control = form.get(field);
      if (this.hasKey(this.formErrors, field)) {
        this.formErrors[field]= '' // works fine!
        if(control && control.dirty &&  !control.valid){
          for(const key in control.errors){
            if (this.hasKey(this.validationMessages, field)) {
            var validationMessage = this.validationMessages[field];
            if (this.hasKey(validationMessage, key)) {
            this.formErrors[field]+= validationMessage[key];
            }
            }
          }
        }
      }
    
    }
  }

  // `keyof any` is short for "string | number | symbol"
// since an object key can be any of those types, our key can too
// in TS 3.0+, putting just "string" raises an error
 hasKey<O>(obj: O, key: keyof any): key is keyof O {
  return key in obj
}

  formErrors = {
    'Name': '',
    'DOB': '',
    'Age': '',
    'DeathInsured':''
  }

  validationMessages = {
    'Name': {
      'required': 'Name is required.',
      'pattern': 'Only Alphabets are allowed.'
    },
    'DOB':  {
      'required': 'DOB is required.'
    },
    'Age':  {
      'required': 'Age is required.',
      'pattern': 'Only Numbers are allowed.'
    },
    'DeathInsured': {
      'required': 'Death Sum Insured is required.',
      'pattern': 'Only Numbers are allowed.'
    },
  }

  loadAllOccupations() {
    this.premiumCalculatorService.getOccupations().subscribe(result => {
      this.allOccupations = result;
      this.isOccupationLoaded = true;
    }, error => {
        // we can catch different exceptions according to error codes
        // for now just implemented general exception
        console.log(error);
        this.isError = true;
        this.isOccupationLoaded = true;
      });
  }

  calculatePremium(event: any) {
    if(this.premiumForm.invalid) {
      this.isValidationError = true;
      return;
    }
    const occupationId = event?.target?.value;
    if (occupationId == 0) this.monthlyPremium = 0;
    
    this.premiumParamModel.OccupationId = occupationId;
    this.premiumCalculatorService.getPremiumValue(this.premiumParamModel)
    .then(result => {
      if(result){
        this.monthlyPremium = Number(result);
      }
    }, error => {
        console.log(error);
        this.isError = true;
    });
  }
}
