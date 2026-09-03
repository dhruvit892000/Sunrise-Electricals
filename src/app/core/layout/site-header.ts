import { Component, inject, output } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CatalogService } from '../../shared/services/catalog.service';

@Component({
  selector: 'app-site-header',
  imports: [RouterLink, RouterLinkActive, MatToolbarModule, MatButtonModule],
  templateUrl: './site-header.html',
  styleUrl: './site-header.css',
})
export class SiteHeader {
  private readonly catalog = inject(CatalogService);

  readonly nav = this.catalog.nav;
  readonly menuToggle = output<void>();
}
