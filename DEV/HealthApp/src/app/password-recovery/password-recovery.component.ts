import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { MatCardModule } from '@angular/material/card'
import { MatFormFieldModule } from '@angular/material/form-field'; // Importar MatFormFieldModule
import { ReactiveFormsModule } from '@angular/forms'; // Importa ReactiveFormsModule
import { BrowserModule } from '@angular/platform-browser';


@Component({
  selector: 'app-password-recovery',
  imports: [MatCardModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    BrowserModule
  ],
  templateUrl: './password-recovery.component.html',
  styleUrl: './password-recovery.component.css'
})
export class PasswordRecoveryComponent implements OnInit {
  recoveryForm!: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.recoveryForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onRecoverPassword() {
    if (this.recoveryForm.valid) {
      const { email } = this.recoveryForm.value;
      alert(`Recovery email sent to ${email}.`);
      // Aquí puedes agregar la lógica para enviar un correo de recuperación.
    }
  }
}
