import { TestBed } from '@angular/core/testing';
import { DynamicFormService } from './services/dynamic-form.service';
import { RestService } from '@abp/ng.core';

describe('DynamicFormService', () => {
  let service: DynamicFormService;
  const mockRestService = jasmine.createSpyObj('RestService', ['request']);
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: RestService,
          useValue: mockRestService,
        },
      ],
    });
    service = TestBed.inject(DynamicFormService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
