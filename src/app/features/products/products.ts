import { Component, computed, effect, inject } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { map } from 'rxjs';
import { ProductCard } from '../../shared/components/product-card';
import { CatalogService } from '../../shared/services/catalog.service';

@Component({
  selector: 'app-products',
  imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatSelectModule, ProductCard],
  templateUrl: './products.html',
  styleUrl: './products.css',
})
export class Products {
  private readonly catalog = inject(CatalogService);
  private readonly route = inject(ActivatedRoute);

  readonly search = new FormControl('', { nonNullable: true });
  readonly brand = new FormControl('all', { nonNullable: true });
  readonly category = new FormControl('all', { nonNullable: true });

  readonly searchValue = toSignal(this.search.valueChanges, { initialValue: this.search.value });
  readonly brandValue = toSignal(this.brand.valueChanges, { initialValue: this.brand.value });
  readonly categoryValue = toSignal(this.category.valueChanges, {
    initialValue: this.category.value,
  });

  readonly brands = this.catalog.productBrands();
  readonly categories = this.catalog.productCategories();

  readonly filteredProducts = computed(() =>
    this.catalog.filterProducts(this.searchValue(), this.brandValue(), this.categoryValue()),
  );

  readonly resultCount = computed(() => this.filteredProducts().length);

  private readonly queryCategory = toSignal(
    this.route.queryParamMap.pipe(map((params) => params.get('category') ?? '')),
    { initialValue: this.route.snapshot.queryParamMap.get('category') ?? '' },
  );

  constructor() {
    effect(() => {
      const category = this.queryCategory();
      if (category && this.categories.includes(category) && this.category.value !== category) {
        this.category.setValue(category);
      }
    });
  }
}
