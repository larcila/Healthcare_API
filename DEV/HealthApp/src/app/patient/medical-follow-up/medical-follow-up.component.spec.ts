import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicalFollowUpComponent } from './medical-follow-up.component';

describe('MedicalFollowUpComponent', () => {
  let component: MedicalFollowUpComponent;
  let fixture: ComponentFixture<MedicalFollowUpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MedicalFollowUpComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedicalFollowUpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
