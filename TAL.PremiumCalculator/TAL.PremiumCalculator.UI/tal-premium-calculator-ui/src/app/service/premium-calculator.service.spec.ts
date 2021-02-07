import { getTestBed, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { PremiumCalculatorService } from './premium-calculator.service';
import { Occupation } from '../Class/occupation';
import { PremiumParameters } from '../Class/premiumparameters';

describe('PremiumCalculatorService', () => {
  let injector: TestBed;
  let service: PremiumCalculatorService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PremiumCalculatorService],
    });

    injector = getTestBed();
    service = injector.inject(PremiumCalculatorService);
    httpMock = injector.inject(HttpTestingController);
  });
  it('should be created', () => {
    const service: PremiumCalculatorService = TestBed.inject(PremiumCalculatorService);
    expect(service).toBeTruthy();
  });
  it('should be test getOccupations', () => {
    const service: PremiumCalculatorService = TestBed.inject(PremiumCalculatorService);
    service.getOccupations().subscribe((res) => {
      expect(res).toEqual(dummyUserListResponse);
    });
    const dummyUserListResponse: Occupation[] = [];
    const req = httpMock.expectOne('http://localhost:49978/Api/Premium/GetOccupations');
    expect(req.request.method).toBe('GET');
    req.flush(dummyUserListResponse);

  });

  it('should be test getPremiumValue', () => {
    const service: PremiumCalculatorService = TestBed.inject(PremiumCalculatorService);
    const premiumparameter: PremiumParameters = {
      Age: 1,
      Name: 'test',
      OccupationId :1,
      SumInsured : 11,
      DOB: new Date()
    }
    service.getPremiumValue(premiumparameter).then((res) => {
     
    });
    const req = httpMock.expectOne('http://localhost:49978/Api/Premium/GetPremiumValue');
    expect(req.request.method).toBe('POST');

  });


  afterEach(() => {
    httpMock.verify();
  }); 
  
});
