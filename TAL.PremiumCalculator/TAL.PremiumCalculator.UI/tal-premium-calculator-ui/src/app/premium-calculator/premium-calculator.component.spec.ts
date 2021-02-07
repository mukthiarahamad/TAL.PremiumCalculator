import { HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';

import { PremiumCalculatorComponent } from './premium-calculator.component';

describe('PremiumCalculatorComponent', () => {
  let component: PremiumCalculatorComponent;
  let fixture: ComponentFixture<PremiumCalculatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PremiumCalculatorComponent ],
      imports: [
        HttpClientModule,
        FormsModule
      ],
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PremiumCalculatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
