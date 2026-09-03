import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { CatalogService } from '../../shared/services/catalog.service';

@Component({
  selector: 'app-price-lists',
  imports: [MatCardModule, MatButtonModule],
  templateUrl: './price-lists.html',
  styleUrl: './price-lists.css',
})
export class PriceLists {
  private readonly catalog = inject(CatalogService);
  readonly priceLists = this.catalog.priceLists;
}
