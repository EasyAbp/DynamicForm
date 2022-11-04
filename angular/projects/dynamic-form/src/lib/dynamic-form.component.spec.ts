import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DynamicFormComponent } from './components/dynamic-form.component';
import { DynamicFormService } from '@easy-abp/dynamic-form';
import { of } from 'rxjs';

describe('DynamicFormComponent', () => {
  let component: DynamicFormComponent;
  let fixture: ComponentFixture<DynamicFormComponent>;
  const mockDynamicFormService = jasmine.createSpyObj('DynamicFormService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [DynamicFormComponent],
      providers: [
        {
          provide: DynamicFormService,
          useValue: mockDynamicFormService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DynamicFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
