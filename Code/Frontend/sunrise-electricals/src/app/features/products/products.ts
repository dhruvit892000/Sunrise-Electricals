import { Component, computed, inject } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ProductCard } from '../../shared/components/product-card';
import { CatalogService } from '../../shared/services/catalog.service';

@Component({
  selector: 'app-products',
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    ProductCard,
    RouterLink,
  ],
  templateUrl: './products.html',
})
export class Products {
  private readonly catalog = inject(CatalogService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  readonly search = new FormControl('', { nonNullable: true });
  readonly brand = new FormControl('all', { nonNullable: true });
  readonly category = new FormControl('all', { nonNullable: true });
  readonly sort = new FormControl('default', { nonNullable: true });

  readonly searchValue = toSignal(this.search.valueChanges, {
    initialValue: this.search.value,
  });

  readonly brandValue = toSignal(this.brand.valueChanges, {
    initialValue: this.brand.value,
  });

  readonly categoryValue = toSignal(this.category.valueChanges, {
    initialValue: this.category.value,
  });

  readonly sortValue = toSignal(this.sort.valueChanges, {
    initialValue: this.sort.value,
  });

  readonly brands = this.catalog.productBrands();
  readonly categories = this.catalog.productCategories();

  readonly filteredProducts = computed(() => {
    const products = this.catalog.filterProducts(
      this.searchValue(),
      this.brandValue(),
      this.categoryValue(),
    );

    const sortedProducts = [...products];

    switch (this.sortValue()) {
      case 'name-asc':
        return sortedProducts.sort((a, b) =>
          a.name.localeCompare(b.name),
        );

      case 'name-desc':
        return sortedProducts.sort((a, b) =>
          b.name.localeCompare(a.name),
        );

      case 'brand-asc':
        return sortedProducts.sort((a, b) =>
          a.brand.localeCompare(b.brand),
        );

      case 'availability':
        return sortedProducts.sort(
          (a, b) =>
            this.availabilityRank(a.availability) -
            this.availabilityRank(b.availability),
        );

      default:
        return sortedProducts;
    }
  });

  readonly resultCount = computed(() => this.filteredProducts().length);

  private initializing = true;

  constructor() {
    const params = this.route.snapshot.queryParamMap;

    const search = params.get('search') ?? '';
    const brand = params.get('brand') ?? 'all';
    const category = params.get('category') ?? 'all';
    const sort = params.get('sort') ?? 'default';

    /*
     * Restore filters and sorting from URL.
     * emitEvent is false so restoring values does not
     * immediately overwrite the URL.
     */

    this.search.setValue(search, { emitEvent: false });

    if (this.brands.includes(brand)) {
      this.brand.setValue(brand, { emitEvent: false });
    }

    if (this.categories.includes(category)) {
      this.category.setValue(category, { emitEvent: false });
    }

    const validSortValues = [
      'default',
      'name-asc',
      'name-desc',
      'brand-asc',
      'availability',
    ];

    if (validSortValues.includes(sort)) {
      this.sort.setValue(sort, { emitEvent: false });
    }

    this.initializing = false;

    /*
     * From this point onward, changes are actual user changes.
     */

    this.search.valueChanges.subscribe(() => {
      if (!this.initializing) {
        this.updateQueryParams();
      }
    });

    this.brand.valueChanges.subscribe(() => {
      if (!this.initializing) {
        this.updateQueryParams();
      }
    });

    this.category.valueChanges.subscribe(() => {
      if (!this.initializing) {
        this.updateQueryParams();
      }
    });

    this.sort.valueChanges.subscribe(() => {
      if (!this.initializing) {
        this.updateQueryParams();
      }
    });
  }

  clearFilters(): void {
    this.search.setValue('', { emitEvent: false });
    this.brand.setValue('all', { emitEvent: false });
    this.category.setValue('all', { emitEvent: false });
    this.sort.setValue('default', { emitEvent: false });

    this.updateQueryParams();
  }

  private availabilityRank(
    availability: 'In Stock' | 'Made to Order' | 'Limited',
  ): number {
    switch (availability) {
      case 'In Stock':
        return 1;

      case 'Limited':
        return 2;

      case 'Made to Order':
        return 3;

      default:
        return 99;
    }
  }

  private updateQueryParams(): void {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        search: this.search.value.trim() || null,
        brand: this.brand.value !== 'all' ? this.brand.value : null,
        category: this.category.value !== 'all' ? this.category.value : null,
        sort: this.sort.value !== 'default' ? this.sort.value : null,
      },
      replaceUrl: true,
    });
  }
}