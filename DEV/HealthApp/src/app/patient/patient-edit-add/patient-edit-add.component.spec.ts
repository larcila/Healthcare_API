import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientEditAddComponent } from './patient-edit-add.component';

describe('PatientEditAddComponent', () => {
  let component: PatientEditAddComponent;
  let fixture: ComponentFixture<PatientEditAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientEditAddComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientEditAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
