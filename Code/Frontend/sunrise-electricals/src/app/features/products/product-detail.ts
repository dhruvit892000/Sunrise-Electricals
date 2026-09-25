import { Component, computed, inject } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { map } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { CatalogService } from '../../shared/services/catalog.service';
import { ProductCard } from '../../shared/components/product-card';

@Component({
  selector: 'app-product-detail',
  imports: [RouterLink, MatButtonModule, ProductCard],
  templateUrl: './product-detail.html',
})
export class ProductDetail {
  private readonly catalog = inject(CatalogService);
  private readonly route = inject(ActivatedRoute);

  private readonly slug = toSignal(
    this.route.paramMap.pipe(
      map((params) => params.get('slug') ?? ''),
    ),
    {
      initialValue: this.route.snapshot.paramMap.get('slug') ?? '',
    },
  );

  readonly product = computed(() => {
    const slug = this.slug();

    return slug ? this.catalog.productBySlug(slug) : undefined;
  });

  readonly relatedProducts = computed(() => {
    const currentProduct = this.product();

    if (!currentProduct) {
      return [];
    }

    return this.catalog
      .filterProducts('', 'all', currentProduct.category)
      .filter((item) => item.slug !== currentProduct.slug)
      .slice(0, 4);
  });
}