import { Component, inject, viewChild } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { NavigationEnd, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { filter } from 'rxjs';
import { CatalogService } from '../../shared/services/catalog.service';
import { SiteFooter } from './site-footer';
import { SiteHeader } from './site-header';

@Component({
  selector: 'app-site-shell',
  imports: [
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    MatSidenavModule,
    MatListModule,
    MatButtonModule,
    SiteHeader,
    SiteFooter,
  ],
  templateUrl: './site-shell.html',
  styleUrl: './site-shell.css',
})
export class SiteShell {
  private readonly catalog = inject(CatalogService);
  private readonly router = inject(Router);
  private readonly drawer = viewChild<MatSidenav>('drawer');

  readonly nav = this.catalog.nav;

  constructor() {
    this.router.events
      .pipe(
        filter((event): event is NavigationEnd => event instanceof NavigationEnd),
        takeUntilDestroyed(),
      )
      .subscribe(() => {
        void this.drawer()?.close();
      });
  }
}
