# Sunrise Electricals - Project Context

This file is the project memory for the Sunrise Electricals frontend. It is meant to help another AI or another machine continue work without depending on chat history.

## 1. Project Overview

- Project name: Sunrise Electricals
- Type: Angular 22 B2B electrical wholesale storefront and lead-generation frontend
- Current stack: Angular 22, TypeScript, Angular Material, Vite-based Angular build tooling, SSR support
- Package manager: npm
- Main goal: power a B2B electrical distribution website that showcases products, category pages, price lists, and quote requests for industrial and commercial buyers

## 2. Tech Stack

- Angular: ^22.1.0
- TypeScript: ~6.0.2
- RxJS: ~7.8.0
- Angular SSR: @angular/ssr
- Express: ^5.1.0
- Angular Material: @angular/material
- Vitest: ^4.0.8
- Prettier: ^3.8.1

## 3. Project Structure

```text
sunrise-electricals/
  AGENTS.md
  CLAUDE.md
  DEVELOPMENT_RULES.md
  PROJECT_CONTEXT.md
  README.md
  angular.json
  package.json
  public/
  src/
    app/
      app.routes.ts
      app.config.ts
      app.ts
      core/
        layout/
          site-shell.ts
          site-shell.html
          site-header.ts
          site-header.html
          site-footer.ts
          site-footer.html
      features/
        home/
        products/
          product-detail.ts
          product-detail.html
          products.ts
          products.html
        price-lists/
        quote/
        about/
        contact/
      shared/
        components/
          product-card.ts
          product-card.html
          quote-form.ts
          quote-form.html
        data/
          catalog.data.ts
        models/
          catalog.models.ts
        services/
          catalog.service.ts
    styles.scss
    index.html
```

## 4. Important Rule Files

These files are not part of the build, but they define coding expectations for AI assistants:

- `AGENTS.md`: project instructions for AI coding agents
- `CLAUDE.md`: Claude-specific instructions for this repo
- `DEVELOPMENT_RULES.md`: engineering and design standards for the project
- `PROJECT_CONTEXT.md`: this file, intended as persistent project memory and handoff context

## 5. Current State of the App

The app is now a functioning B2B electrical wholesale prototype rather than a default Angular starter. The current implementation includes:

- responsive site shell with skip link, mobile drawer navigation, header, footer, and routed content layout
- lazy-loaded Angular routes for Home, Products, Product Detail, Price Lists, Quote, About, and Contact
- a shared catalog model layer for products, categories, brands, stats, and pricing metadata
- a mock `CatalogService` that exposes navigation, category data, product data, price list items, and filter helpers
- product listing with live search, brand filtering, category filtering, sorting, and URL-synced query params
- product detail pages with SKU, MOQ, availability, application context, and specification blocks
- home page with hero messaging, trusted brand highlights, category-level feature cards, and lead capture CTAs
- quote request and contact form flows using Angular reactive forms and reusable shared form components
- B2B-oriented content and business messaging oriented around industrial supply, bulk procurement, and quote requests

The project is still using mock catalog data and placeholder pricing/inventory patterns. A real backend, live order system, PDF generation, and CRM integration are not yet implemented.

## 6. How to Run the Project

From the project root:

```bash
npm install
npm start
```

Or:

```bash
ng serve
```

Production build:

```bash
npm run build
```

Test command:

```bash
npm test
```

SSR run command:

```bash
npm run serve:ssr:sunrise-electricals
```

## 7. Coding Conventions Expected by the Repo

Follow the rules in `AGENTS.md`, `CLAUDE.md`, and `DEVELOPMENT_RULES.md`:

- Prefer TypeScript strict typing
- Avoid `any`; use `unknown` when needed
- Prefer standalone Angular components
- Do not explicitly set `standalone: true` because Angular v20+ defaults to it
- Do not explicitly set `OnPush` because Angular v22+ defaults to it
- Prefer signals for state management
- Prefer native Angular template control flow (`@if`, `@for`, `@switch`)
- Prefer `input()`, `output()`, and `model()` over decorator-based APIs
- Prefer `class` bindings over `ngClass`
- Prefer `style` bindings over `ngStyle`
- Use `NgOptimizedImage` for static images when applicable
- Keep accessibility in mind; aim for WCAG AA and AXE-friendly markup
- Keep components small and focused

## 8. Current App Structure by Feature

```text
src/app/
  core/
    layout/
      site-shell        -> responsive shell and mobile navigation
      site-header       -> top navigation and menu trigger
      site-footer       -> business footer with CTAs
  features/
    home               -> landing page, trust stats, category highlights, quote CTA
    products           -> product catalog with search and filters
    product-detail     -> product-specific detail page
    price-lists        -> downloadable / informational pricing pages
    quote              -> quote request form experience
    about              -> company profile and positioning
    contact            -> inquiry/contact details and form
  shared/
    components/
      product-card     -> reusable catalog item card
      quote-form       -> reusable inquiry / quote form
    data/
      catalog.data     -> site nav, stats, brands, categories, mock products
    models/
      catalog.models   -> typed catalog and business entity models
    services/
      catalog.service  -> catalog queries and filter logic
```

## 9. Business Context

The business model remains a B2B wholesale electrical distribution company, not a retail storefront.

### Business Model

- Primary customers: factories, industrial units, commercial businesses, contractors, project buyers, maintenance teams
- Sales approach: bulk supply and dealer-led wholesale procurement
- Customer value proposition: competitive pricing, trusted brands, reliable supply, bulk availability, rapid quotations
- Revenue model: margin between purchase cost and customer selling price
- Sales channels: direct B2B inquiries, quote requests, downloadable price lists, supplier-based procurement support

### Product Scope

The platform supports a wide electrode / electrical product catalog covering:

- L&T
- Siemens
- Hager
- RR Kabel
- Polycab
- Syska LED
- KEI Wires and Cables
- Crompton
- Havells
- plus additional electrical brands and product families across the market

### Product Categories

- Contactors
- Relays
- Switches
- Plug sockets
- MCBs / breakers
- Distribution boards
- Cable and wire solutions
- Lighting products
- Industrial controls
- Safety devices
- Panel accessories
- Switchgear items
- Electrical accessories and spares

### Core Website Purpose

The website is designed to act as a B2B quotation and product enquiry platform where users can:

- browse electrical products and categories
- search products by brand, category, or application
- view business hero and trust sections
- request a quote for bulk orders
- send inquiry for product availability or pricing
- review price list content and product families
- contact the sales team for industrial supply requirements

## 10. Latest Implementation Updates

### 2026-09-22

The project has moved beyond the initial starter setup and now includes the following implemented work:

- Angular shell and routing were reworked into a complete storefront layout with a mobile side menu and route-based navigation
- `Home`, `Products`, `Product Detail`, `Price Lists`, `Quote`, `About`, and `Contact` pages were added as individual feature routes
- A reusable `ProductCard` component and `QuoteForm` component were created for catalog and lead capture reuse
- A central mock data layer was implemented with electrical product catalog entries, categories, stats, and price list metadata
- Catalog search and filtering logic were implemented with brand/category selectors, sort controls, and query param synchronization
- Product detail pages were added to support SKU-level browsing, application context, and specification listing
- Quote request and contact forms were structured as modern Angular forms connected to the business flow
- The application content and messaging were adjusted to fit a wholesale electrical distribution brand rather than a generic starter template

**Changed files (2026-09-22)**

- `src/app/app.routes.ts`: Added lazy-loaded route definitions for Home, Products, Product Detail, Price Lists, Quote, About, and Contact.
- `src/app/core/layout/site-shell.html`: Implemented responsive shell markup including skip link, mobile `mat-sidenav` drawer navigation, and route outlet placement.
- `src/app/features/home/home.ts` and `home.html`: Home component wiring and template to surface hero, highlights, stats, and quote CTA.
- `src/app/features/products/products.ts` and `products.html`: Product listing with search, brand/category filters, sorting, and URL-synced query params.
- `src/app/features/products/product-detail.ts` and `product-detail.html`: Product detail page showing SKU, MOQ, availability, application, and specifications.
- `src/app/shared/data/catalog.data.ts`: Central mock catalog with `siteNav`, `catalogProducts`, `catalogCategories`, `catalogBrands`, `catalogStats`, and `catalogPriceLists`.
- `src/app/shared/services/catalog.service.ts`: `CatalogService` exposing catalog data, product/category helpers, and filter logic.
- `src/app/shared/components/product-card.ts` and `product-card.html`: Reusable product card component used across catalog and listing pages.
- `src/app/shared/components/quote-form.ts` and `quote-form.html`: Reusable quote/contact form component used on Home, Quote, and Contact pages.
- `PROJECT_CONTEXT.md`: This file was updated to reflect the current implementation and latest status.

Each listed file contains B2B-facing content and wiring; most catalog and product data are mock fixtures pending API integration.

## 11. Status Summary

Current status:

- Angular 22 frontend with Angular Material styling and responsive layout
- B2B wholesale storefront structure is implemented and wired up
- Catalog browsing, product detail, quote, contact, about, and price-list sections are in place
- Product search, filtering, and sort behavior work through mock data
- Real backend integration, live pricing logic, PDF generation, and authentication flows are still pending
- Ready for further branding, business data, and backend/API integration work

## 12. Next Suggested Tasks

1. Replace mock product data with real catalog content from the business or CMS
2. Add backend/API integration for pricing, stock, and quote submissions
3. Build real PDF or downloadable price list assets for each brand or category
4. Add validation and submission handling for quote and contact lead forms
5. Improve conversion-focused design with stronger brand identity and case studies
6. Add additional dealer / supplier / partner pages if needed
7. Expand product taxonomy and category landing pages
8. Add tests for key catalog and quote flows

## 13. Development Rules

The project now follows a dedicated development standards file:

- `DEVELOPMENT_RULES.md` defines the UI, architecture, and documentation standards for all future work.
- All future implementation must use modern, responsive, business-ready design patterns.
- Angular Material / Material-inspired UI patterns are the preferred component standard for reusable controls and layouts.
- The app must be built dynamically and responsively with premium, conversion-focused design.
- Every substantial development step must be recorded in this file in parallel with implementation.

## 14. Initial Documentation Log

### 2026-09-02

- Created `DEVELOPMENT_RULES.md` to formalize project standards.
- Added the requirement to use modern Angular Material-inspired UI patterns and responsive premium design.
- Added the rule that every development task must be documented in `PROJECT_CONTEXT.md`.
- Confirmed the project remains in starter state and is ready for business UI development.

### 2026-09-22

- Reframed the app as a B2B electrical wholesale storefront instead of a starter Angular template.
- Documented the implemented site shell, route structure, catalog system, and quote/contact flow.
- Updated the project memory to reflect the current product catalog implementation and business positioning.

This is the working memory snapshot for the Sunrise Electricals frontend as of September 22, 2026, with the current implementation and standards captured in parallel.

## 15. Business Clarification and Product Strategy

### 2026-09-02

The business model is clarified as a B2B wholesale electrical distribution business, not a retail store.

#### Business Model

- Primary customers: factories, industrial units, commercial businesses, contractors, project buyers, maintenance teams
- Sales approach: bulk supply and dealer-led wholesale procurement
- Customer value proposition: competitive pricing, trusted brands, reliable supply, bulk availability, quick quotations
- Revenue model: margin between purchase cost and customer selling price
- Sales channels: direct B2B inquiries, quote requests, downloadable price lists, supplier-based procurement support

#### Product Scope

The platform should support a wide electrical product catalog covering:

- L&T
- Siemens
- Hager
- RR Kabel
- Polycab
- Syska LED
- KEI Wires and Cables
- Crompton
- Havells
- plus additional electrical brands and product families across the market

#### Product Categories

- Contactors
- Relays
- Switches
- Plug sockets
- MCBs / breakers
- Distribution boards
- Cable and wire solutions
- Lighting products
- Industrial controls
- Safety devices
- Panel accessories
- Switchgear items
- Electrical accessories and spares

#### Core Website Purpose

The website should become a B2B quotation and product enquiry platform where users can:

- browse electrical products and categories
- search products by brand, category, or application
- view latest price list downloads by brand or category
- request a quote for bulk orders
- send inquiry for product availability or pricing
- download price list documents or PDF catalogs
- contact the sales team for industrial supply requirements

#### Required App Sections

- Home page with business hero section and trust messaging
- Product catalog page with filter/search
- Category-based browsing
- Brand catalog section
- Downloadable price list page
- Quote request form
- Inquiry / bulk order form
- Contact page
- About / company profile page
- Maybe dealer / supplier partnership page in future
