import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./core/layout/site-shell').then((m) => m.SiteShell),
    children: [
      {
        path: '',
        loadComponent: () => import('./features/home/home').then((m) => m.Home),
        title: 'Sunrise Electricals | Wholesale Electrical Supply',
      },
      {
        path: 'products',
        loadComponent: () => import('./features/products/products').then((m) => m.Products),
        title: 'Products | Sunrise Electricals',
      },
      {
        path: 'price-lists',
        loadComponent: () =>
          import('./features/price-lists/price-lists').then((m) => m.PriceLists),
        title: 'Price Lists | Sunrise Electricals',
      },
      {
        path: 'quote',
        loadComponent: () => import('./features/quote/quote').then((m) => m.Quote),
        title: 'Request Quote | Sunrise Electricals',
      },
      {
        path: 'about',
        loadComponent: () => import('./features/about/about').then((m) => m.About),
        title: 'About | Sunrise Electricals',
      },
      {
        path: 'contact',
        loadComponent: () => import('./features/contact/contact').then((m) => m.Contact),
        title: 'Contact | Sunrise Electricals',
      },
    ],
  },
  { path: '**', redirectTo: '' },
];
