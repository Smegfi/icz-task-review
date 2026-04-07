import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule, NgForm } from '@angular/forms';
import { Task, TaskApiService } from '../../../api-client';

@Component({
  selector: 'app-user-edit-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    FormsModule
  ],
  styles: [`
    .dialog-content {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      align-items: center;
      padding-top: 0.5rem;
    }

    mat-form-field {
      width: 100%;
      max-width: 400px;
    }
  `],
  template: `
<h2 mat-dialog-title>{{ data.user.id ? 'Edit Task' : 'New Task' }}</h2>

<form #form="ngForm" class="dialog-content" (ngSubmit)="save(form)">
<mat-dialog-content>
<mat-form-field appearance="fill">
    <mat-label>Name</mat-label>
    <input matInput [(ngModel)]="data.user.name" name="name" required>
    <mat-error *ngIf="form.submitted && form.controls['name']?.invalid">
      Name is required
    </mat-error>
  </mat-form-field>

  <mat-form-field appearance="fill">
    <mat-label>Description</mat-label>
    <input
      matInput
      [(ngModel)]="data.user.description"
      name="description"
      required
    >
  </mat-form-field>
  <div *ngIf="addError" class="error-message" style="color: red; padding-top: 8px;">
    {{ addError }}
  </div>
</mat-dialog-content>
<mat-dialog-actions align="end">
    <button mat-button type="button" (click)="cancel()">Cancel</button>
    <button mat-flat-button color="primary" type="submit" [disabled]="form.invalid">Save</button>
  </mat-dialog-actions>
</form>
  `
})
export class UserEditDialog {
  addError: string | null = null;

  private api = inject(TaskApiService);
  private dialogRef = inject(MatDialogRef);
  data = inject<{ user: Task }>(MAT_DIALOG_DATA);

  save(form: NgForm) {
    if (form.invalid) return;
    this.addError = null;
    const user = this.data.user;
    this.api.apiTaskPost({ name: user.name, description: user.description }).subscribe({
      next: () => this.dialogRef.close(true),
      error: (err) => {
        if (err.status === 400) {

            const errorDetails = err.error.errors;
            const messages = Object.values(errorDetails).flat();
            this.addError = messages.join('\n');

        }
      }
    });
  }

  cancel() {
    this.dialogRef.close(false);
  }
}
