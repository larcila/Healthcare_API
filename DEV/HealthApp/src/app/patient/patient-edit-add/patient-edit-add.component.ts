import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSnackBar } from '@angular/material/snack-bar'; // Para mostrar mensajes de error

import { MenuComponent } from '../../shared/menu/menu.component';

//Services
import { PatientService } from '../../services/patient.service';

@Component({
  selector: 'app-patient-edit-add',
  imports: [
    CommonModule,
    MatFormFieldModule,
    FormsModule,
    MatInputModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatToolbarModule,
    MenuComponent,
  ],
  templateUrl: './patient-edit-add.component.html',
  styleUrl: './patient-edit-add.component.css'
})

export class PatientEditAddComponent implements OnInit {
  patientForm!: FormGroup;
  isEditMode = false;
  storedPatient: any; 
  currentUser_Id: any;


  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private patientService: PatientService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
    this.isEditMode = !!this.route.snapshot.paramMap.get('id');

    this.patientForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      familyName: ['', [Validators.required, Validators.maxLength(50)]],
      identification: ['', [Validators.required, Validators.maxLength(20)]],
      birthDate: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.maxLength(20)]],
    });

    this.currentUser_Id = localStorage.getItem('User_Id'); // get token from localStorage
    
    const id = this.route.snapshot.paramMap.get('id');
    if(id)
    {
      this.loadPatientById(Number(id));
    }


    // if (this.isEditMode) {
    //   const id = this.route.snapshot.paramMap.get('id');
    // }
  }

  onSubmit() {
    if (this.patientForm.valid) {
      const patient = {
        //...this.patientForm.value,
        patient_ID: this.isEditMode ? this.storedPatient.patient_ID : 0,
        first_Name: this.patientForm.value.name,
        family_Name: this.patientForm.value.familyName,
        identification: this.patientForm.value.identification,
        birth_Date: this.patientForm.value.birthDate,
        email: this.patientForm.value.email,
        phone_Number: this.patientForm.value.phoneNumber,

        registration_Date: this.isEditMode ? this.storedPatient.registration_Date :new Date(),
        modify_Date: new Date(),
        registration_User_ID: this.isEditMode ? this.storedPatient.registration_User_ID : this.currentUser_Id,
        update_User_ID: this.currentUser_Id,
      };


      if (this.isEditMode) {
        // user Update
        this.patientService.updatePatient(patient).subscribe({
          next: () => {
            this.snackBar.open('Patient updated successfully!', 'Close', { duration: 3000 });
            this.router.navigate(['/patient']);
          },
          error: (err) => {
            console.error('Error updating patient:', err);
            this.snackBar.open('Error updating patient. Please try again.', 'Close', { duration: 5000 });
          },
        });

      } else {
        // Add new patient
        this.patientService.createPatient(patient).subscribe({
          next: () => {
            this.snackBar.open('Patient created successfully!', 'Close', { duration: 3000 });
            this.router.navigate(['/patient']);
          },
          error: (err) => {
            console.error('Error creating patient:', err);
            this.snackBar.open('Error creating patient. Please try again.', 'Close', { duration: 5000 });
          },
        });

      }
    }
  }
  
  loadPatientById(id: number): void {
    this.patientService.getPatientById(id).subscribe({
      next: (patient) => {
        this.isEditMode = true;
        this.storedPatient = patient;

        this.patientForm.patchValue({
          name: patient.first_Name,
          familyName: patient.family_Name,
          identification: patient.identification,
          birthDate: patient.birth_Date,
          email: patient.email,
          phoneNumber: patient.phone_Number
        });
      },
      error: (err) => {
        console.error('Error loading patient:', err);
        this.snackBar.open('Error loading patient data.', 'Close', { duration: 5000 });
      },
    });
  }


}