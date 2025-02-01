import { Routes } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { PasswordRecoveryComponent } from './password-recovery/password-recovery.component';
import { HomeComponent } from './home/home.component';

import { UserComponent } from './user/user.component';
import { UserEditAddComponent } from './user/user-edit-add/user-edit-add.component';
import { RoleComponent } from './role/role.component';
import { PatientComponent } from './patient/patient.component';
import { PatientEditAddComponent } from './patient/patient-edit-add/patient-edit-add.component';
import { MedicalFollowUpComponent } from './patient/medical-follow-up/medical-follow-up.component';

import { AuthGuard } from './auth/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'password-recovery', component: PasswordRecoveryComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'user', component: UserComponent, canActivate: [AuthGuard], data: { role: 'ADMIN'} },
  { path: 'user/edit', component: UserEditAddComponent, canActivate: [AuthGuard], data: { role: 'ADMIN'} },
  { path: 'user/edit/:id', component: UserEditAddComponent, canActivate: [AuthGuard], data: { role: 'ADMIN'} },
  //{ path: 'role', component: RoleComponent,canActivate: [AuthGuard], data: { role: 'ADMIN'} },
  { path: 'patient', component: PatientComponent, canActivate: [AuthGuard] },
  { path: 'patient/add', component:  PatientEditAddComponent, canActivate: [AuthGuard] },
  { path: 'patient/edit/:id', component:  PatientEditAddComponent, canActivate: [AuthGuard] },
  { path: 'patient/detail/:id', component:  MedicalFollowUpComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: 'login' },
];
