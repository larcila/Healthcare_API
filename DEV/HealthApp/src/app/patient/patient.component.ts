import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';


import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';

import { MenuComponent } from '../shared/menu/menu.component';
import { RouterModule } from '@angular/router'; // to routerLink

import { PatientService } from '../services/patient.service';

@Component({
  selector: 'app-patient',
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
  ],
  templateUrl: './patient.component.html',
  styleUrls: ['./patient.component.css']
})
export class PatientComponent implements OnInit, AfterViewInit  {
  displayedColumns: string[] = ['first_Name', 'family_Name', 'email', 'phone_Number', 'identification', 'birth_Date', 'actions'];
  dataSource = new MatTableDataSource<any>([]); // empty Initialized
  

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private router: Router,
    private patientService: PatientService
  ) {}

  ngOnInit() {
    this.loadPatients(); // Load data when initialising the component
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  loadPatients() {
    this.patientService.getPatients().subscribe({
      next: (data) => {
        this.dataSource.data = data; 
      },
      error: (error) => {
        console.error('Error getting users:', error);
      },
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  deletePatient(id: number) {
    this.patientService.deletePatient(id).subscribe({
      next: (data) => {
        this.loadPatients();
      },
      error: (error) => {
        console.error('Error deleting patient:', error);
      },
    });
  }

  redirectToEdit(id: number) {
    this.router.navigate(['/patient/edit', id]);
  }

  redirectToDetail(id: number) {
    this.router.navigate(['/patient/detail', id]);
  }
}
