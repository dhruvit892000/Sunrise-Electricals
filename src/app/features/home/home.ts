import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { ProductCard } from '../../shared/components/product-card';
import { QuoteForm } from '../../shared/components/quote-form';
import { CatalogService } from '../../shared/services/catalog.service';

@Component({
  selector: 'app-home',
  imports: [RouterLink, MatButtonModule, MatCardModule, ProductCard, QuoteForm],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  private readonly catalog = inject(CatalogService);

  readonly stats = this.catalog.stats;
  readonly brands = this.catalog.brands;
  readonly highlights = this.catalog.highlights;
  readonly categories = this.catalog.categories;
  readonly products = this.catalog.products;
  readonly priceLists = this.catalog.priceLists;
}
