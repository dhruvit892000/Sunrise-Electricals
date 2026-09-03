import { Injectable } from '@angular/core';
import {
  catalogBrands,
  catalogCategories,
  catalogHighlights,
  catalogPriceLists,
  catalogProducts,
  catalogStats,
  siteNav,
} from '../data/catalog.data';
import { CategoryItem, ProductItem } from '../models/catalog.models';

@Injectable({ providedIn: 'root' })
export class CatalogService {
  readonly nav = siteNav;
  readonly stats = catalogStats;
  readonly brands = catalogBrands;
  readonly highlights = catalogHighlights;
  readonly categories = catalogCategories;
  readonly products = catalogProducts;
  readonly priceLists = catalogPriceLists;

  productBrands(): string[] {
    return [...new Set(this.products.map((product) => product.brand))];
  }

  productCategories(): string[] {
    return [...new Set(this.products.map((product) => product.category))];
  }

  filterProducts(search: string, brand: string, category: string): ProductItem[] {
    const query = search.trim().toLowerCase();

    return this.products.filter((product) => {
      const matchesSearch =
        query.length === 0 ||
        product.name.toLowerCase().includes(query) ||
        product.sku.toLowerCase().includes(query) ||
        product.brand.toLowerCase().includes(query) ||
        product.category.toLowerCase().includes(query);

      const matchesBrand = brand === 'all' || product.brand === brand;
      const matchesCategory = category === 'all' || product.category === category;

      return matchesSearch && matchesBrand && matchesCategory;
    });
  }

  categoryBySlug(slug: string): CategoryItem | undefined {
    return this.categories.find((category) => category.slug === slug);
  }
}
