import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../auth/auth.service';

import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';

import { FormsModule } from '@angular/forms';
import { MenuComponent } from '../shared/menu/menu.component';

//Services
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user',
  imports: [MatFormFieldModule,
    FormsModule,
    MatInputModule,
    MatTableModule,
    MatButtonModule,
    MatPaginatorModule,
    MatToolbarModule,
    CommonModule,
    MenuComponent,
  ],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})

export class UserComponent implements AfterViewInit {
  displayedColumns: string[] = ['first_Name', 'family_Name', 'user_Name', 'identification', 'email', 'birth_Date', 'actions'];
  dataSource = new MatTableDataSource<any>([]); // empty Initialized

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private router: Router, 
    private authService: AuthService, 
    private userService: UserService
    ) 
    {}  

    ngOnInit() {
      this.loadUsers(); // Load data when initialising the component
    }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  loadUsers() {
    this.userService.getUsers().subscribe({
      next: (data) => {
        this.dataSource.data = data; 
      },
      error: (error) => {
        console.error('Error getting users:', error);
      },
    });
  }

  applyFilter() {
    const filterValue = this.filterValue.trim().toLowerCase();
    this.dataSource.filter = filterValue;
  }

  addUser() {
    // console.log('Add user button clicked');
    this.router.navigate(['/user/edit']);
  }

  editUser(user: any) {
    this.router.navigate(['/user/edit',user.user_ID]);
  }

  deleteUser(id: number) {
    this.userService.deleteUser(id).subscribe({
      next: (data) => {
        this.loadUsers();
      },
      error: (error) => {
        console.error('Error deleting users:', error);
      },
    });
  }

  filterValue = '';

}