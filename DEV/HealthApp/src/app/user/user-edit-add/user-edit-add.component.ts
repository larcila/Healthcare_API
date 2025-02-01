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
import { MatRadioModule } from '@angular/material/radio';

import { MenuComponent } from "../../shared/menu/menu.component";

//Services
import { UserService } from '../../services/user.service'; 
import { RoleComponent } from '../../role/role.component';


@Component({
  selector: 'app-user-edit-add',
  standalone: true,
  imports: [CommonModule,
    MatFormFieldModule,
    FormsModule,
    MatInputModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatNativeDateModule, 
    MatToolbarModule,
    MatRadioModule,
    MenuComponent,
  ],
  templateUrl: './user-edit-add.component.html',
  styleUrl: './user-edit-add.component.css'
})

export class UserEditAddComponent implements OnInit {
  userForm!: FormGroup;
  editMode = false;
  generatedPassword = '';
  storedUser: any; 
  currentUser_Id: any;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.userForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      familyName: ['', [Validators.required, Validators.maxLength(50)]],
      identification: ['', [Validators.required, Validators.maxLength(20)]],
      username: ['', [Validators.required, Validators.maxLength(20)]],
      birthDate: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      role: ['2', Validators.required], // Default value is 2: 'USER'
    });
    
    this.currentUser_Id = localStorage.getItem('User_Id'); // get token from localStorage

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadUserById(Number(id));
    }
  }

  generatePassword(): void {
    this.generatedPassword = Math.random().toString(36).slice(-8);
  }

  onSubmit() {
    if (this.userForm.valid) {
      const user = {
        //...this.userForm.value,
        user_ID: this.editMode ? this.storedUser.user_ID : 0,
        first_Name: this.userForm.value.name,
        family_Name: this.userForm.value.familyName,
        identification: this.userForm.value.identification,
        birth_Date: this.userForm.value.birthDate,
        email: this.userForm.value.email,
        user_Name: this.userForm.value.username,
        password: this.generatedPassword,
        
        registration_Date: this.editMode ? this.storedUser.registration_Date :new Date(),
        modify_Date: new Date(),
        registration_User_ID: this.editMode ? this.storedUser.registration_User_ID : this.currentUser_Id,
        update_User_ID: this.currentUser_Id,

        role_By_Users: {
          role_By_User_Id: 0,
          user_ID: this.editMode ? this.storedUser.user_ID : 0, 
          role_ID: Number(this.userForm.value.role),
        }
      };


      if (this.editMode) {
        // user Update
        this.userService.updateUser(user).subscribe({
          next: () => {
            this.snackBar.open('User updated successfully!', 'Close', { duration: 3000 });
            this.router.navigate(['/user']);
          },
          error: (err) => {
            console.error('Error updating user:', err);
            this.snackBar.open('Error updating user. Please try again.', 'Close', { duration: 5000 });
          },
        });

      } else {
        // Add new user
        this.userService.createUser(user).subscribe({
          next: () => {
            this.snackBar.open('User created successfully!', 'Close', { duration: 3000 });
            this.router.navigate(['/user']);
          },
          error: (err) => {
            console.error('Error creating user:', err);
            this.snackBar.open('Error creating user. Please try again.', 'Close', { duration: 5000 });
          },
        });

      }
    }
  }

  goBack() {
    this.router.navigate(['/user']);
  }

  loadUserById(id: number): void {
    this.userService.getUserById(id).subscribe({
      next: (user) => {
        this.editMode = true;
        this.storedUser = user;

        this.userForm.patchValue({
          name: user.first_Name,
          familyName: user.family_Name,
          identification: user.identification,
          username: user.user_Name,
          birthDate: user.birth_Date,
          email: user.email,
        });
      },
      error: (err) => {
        console.error('Error loading user:', err);
        this.snackBar.open('Error loading user data.', 'Close', { duration: 5000 });
      },
    });
    //Get role
    this.userService.getRole_By_UserByIdUser(id).subscribe({
      next: (roleByUser) => {
        if(roleByUser.length > 0)
        {
          this.userForm.patchValue({
            role: roleByUser[0].role_ID.toString()
          });
        }
      },
      error: (err) => {
        console.error('Error loading user:', err);
        this.snackBar.open('Error loading user data.', 'Close', { duration: 5000 });
      },
    });

  }

}
