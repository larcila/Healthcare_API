import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSnackBar } from '@angular/material/snack-bar'; // Para mostrar mensajes de error

import { MenuComponent } from '../../shared/menu/menu.component';
import { RouterModule } from '@angular/router'; // to routerLink

import { ActivatedRoute } from '@angular/router';

//Services
import { PatientService } from '../../services/patient.service';
import { MedicalFollowUpService } from '../../services/medical-follow-up.service';

@Component({
  selector: 'app-medical-follow-up',
  imports: [
      CommonModule,
      MatButtonModule,
      MatInputModule,
      MatFormFieldModule,
      MatTableModule,
      MatToolbarModule,
      MatPaginatorModule,
      MenuComponent,
      RouterModule,
      ReactiveFormsModule,
    ],
  templateUrl: './medical-follow-up.component.html',
  styleUrl: './medical-follow-up.component.css'
})

export class MedicalFollowUpComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['registration_Date', 'allergy', 'symptom', 'screening', 'actions'];
  dataSource = new MatTableDataSource<any>([]); // empty Initialized
 
  //patient infromation header
  displayedColumnsPatient: string[] = ['name', 'familyName', 'email', 'phoneNumber', 'identification', 'birthDate' ];
  dataSourcePatient = new MatTableDataSource<any>([]); //empty Initialized

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  followUpForm!: FormGroup;
  editMode = false;
  currentId!: number;
  storedFolloUp: any; 
  currentUser_Id: any;
  currentPatient_id: Number = 0;

  constructor(private router: Router, 
    private fb: FormBuilder,
    private patientService: PatientService,
    private medicalFollowUpService: MedicalFollowUpService,
    private route: ActivatedRoute, // to receive the Patient_ID from patient component
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.followUpForm = this.fb.group({
      allergies: ['', [Validators.required ]],
      symptoms: ['', [Validators.required ]],
      screenings: ['', [Validators.required ]],
    });
    
    this.currentUser_Id = localStorage.getItem('User_Id'); // get token from localStorage
    const id = this.route.snapshot.paramMap.get('id');
  
    //load patient information and follow-ups
    if (id) {
      this.currentPatient_id = Number(id);
      this.loadPatientById(Number(id));
      this.loadFollowUps(Number(id)); // Load data when initialising the component
    }

  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  loadPatientById(patientId: number) {
    this.patientService.getPatientById(patientId).subscribe({
      next: (patient) => {
       const patientInformation = {
        name: patient.first_Name, 
        familyName: patient.family_Name,
        email: patient.email, 
        phoneNumber: patient.phone_Number,
        identification: patient.identification, 
        birthDate: patient.birth_Date
       };
       const updatedData = [patientInformation];
       this.dataSourcePatient.data = updatedData;

      },
      error: (err) => {
        console.error('Error loading patient information:', err);
        this.snackBar.open('Error loading patient data.', 'Close', { duration: 5000 });
      },
    }); 
  }

  loadFollowUps(patientId: number) {
    //console.error('Error getting patient_ID:', patientId);
    this.medicalFollowUpService.getAllFollowUp(patientId).subscribe({
      next: (data) => {
        this.dataSource.data = data; 
      },
      error: (error) => {
        console.error('Error getting users:', error);
      },
    });
  }

  ToEdit(id: number) {
    const record = this.dataSource.data.find(item => item.follow_Up_ID === id);
    if (record) {
      this.followUpForm.setValue({
        allergies: record.allergy,
        symptoms: record.symptom,
        screenings: record.screening,
      });
      this.storedFolloUp = record;
      this.editMode = true;
      this.currentId = id;
    }
  }

  onSubmit() {
    if (this.followUpForm.valid) {
      const followUp = {
        follow_Up_ID: this.editMode ? this.storedFolloUp.follow_Up_ID : 0,
        allergy: this.followUpForm.value.allergies,
        symptom: this.followUpForm.value.symptoms,
        screening: this.followUpForm.value.screenings,
        patient_ID: this.currentPatient_id, //this.storedFolloUp.patient_ID,

        registration_Date: this.editMode ? this.storedFolloUp.registration_Date :new Date(),
        modify_Date: new Date(),
        registration_User_ID: this.editMode ? this.storedFolloUp.registration_User_ID : this.currentUser_Id,
        update_User_ID: this.currentUser_Id,
      };

      if (this.editMode) {
        // followUp Update
        this.medicalFollowUpService.updateFollowUp(followUp).subscribe({
          next: () => {
            this.snackBar.open('Follow-up updated successfully!', 'Close', { duration: 3000 });
            //this.router.navigate(['/patient']);
            this.toCancel();
            this.loadFollowUps(Number(this.currentPatient_id));
          },
          error: (err) => {
            console.error('Error updating follow-up:', err);
            this.snackBar.open('Error updating follow-up. Please try again.', 'Close', { duration: 5000 });
          },
        });
      } else {
        //Add new follow-up
        this.medicalFollowUpService.createFollowUp(followUp).subscribe({
          next: () => {
            this.snackBar.open('Follow-up created successfully!', 'Close', { duration: 3000 });
            //this.router.navigate(['/patient']);
            this.toCancel();
            this.loadFollowUps(Number(this.currentPatient_id));
          },
          error: (err) => {
            console.error('Error creating follow-up:', err);
            this.snackBar.open('Error creating follow-up. Please try again.', 'Close', { duration: 5000 });
          },
        });
      }

    }
  }

  toCancel(){
    this.followUpForm.reset();
    this.editMode = false;
  }

  goBack(){
    this.router.navigate(['/patient']);
  }

}
