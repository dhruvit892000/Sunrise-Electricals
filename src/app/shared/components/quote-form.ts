import { Component, effect, inject, input, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { CatalogService } from '../services/catalog.service';

@Component({
  selector: 'app-quote-form',
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
  ],
  templateUrl: './quote-form.html',
  styleUrl: './quote-form.css',
})
export class QuoteForm {
  private readonly catalog = inject(CatalogService);
  private readonly fb = inject(FormBuilder);

  readonly sku = input('');
  readonly submitted = signal(false);

  readonly categories = this.catalog.categories;

  readonly form = this.fb.nonNullable.group({
    company: ['', Validators.required],
    contactName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phone: ['', Validators.required],
    category: [''],
    sku: [''],
    quantity: [''],
    message: ['', Validators.required],
  });

  constructor() {
    effect(() => {
      const sku = this.sku();
      if (sku && this.form.controls.sku.value !== sku) {
        this.form.controls.sku.setValue(sku);
      }
    });
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitted.set(true);
    this.form.reset();
  }
}
