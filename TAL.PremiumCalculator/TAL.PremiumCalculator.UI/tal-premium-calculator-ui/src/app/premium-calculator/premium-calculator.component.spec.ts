import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import {  ComponentFixture, TestBed } from '@angular/core/testing';
import { Observable, of } from 'rxjs';
import { PremiumCalculatorService } from '../service/premium-calculator.service';
import { PremiumCalculatorComponent } from './premium-calculator.component';

describe('PremiumCalculatorComponent', () => {
  let component: PremiumCalculatorComponent;
  let fixture: ComponentFixture<PremiumCalculatorComponent>;
  let compiled: { textContent: any; } | null = null;
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PremiumCalculatorComponent ],
      imports: [
        HttpClientModule,
        FormsModule,
      ],
      providers: [{
        provide: PremiumCalculatorService,
     }],
     schemas:[CUSTOM_ELEMENTS_SCHEMA]
    })
    .compileComponents();

    const mockTopToolBarService = TestBed.inject(PremiumCalculatorService);
   
    const spy = spyOn(mockTopToolBarService, 'getOccupations').and.returnValue(
      of([])
    );

    const spy1 = spyOn(mockTopToolBarService, 'getPremiumValue').and.returnValue(
      Promise.resolve(1)
    );
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PremiumCalculatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    compiled = fixture.debugElement.nativeElement;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch loadAllOccupations', () => {
    component.loadAllOccupations();
  });

  it('should calculate the premium using calculatePremium', () => {
    const event = { target: { value: 42 }}; 
    component.calculatePremium(event);
  });

  it('should test transformAmount', () => {
    const event = { target: { value: 42111111 }}; 
    component.transformAmount(event);
  });

  it('should calculate the premium using calculatePremium', () => {
    component.premiumParamModel.DOB = new Date('2019-02-04');
    component.calculateAge();
  });
  
});
