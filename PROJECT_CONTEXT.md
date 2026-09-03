# Sunrise Electricals - Project Context

This file is the project memory for the Sunrise Electricals frontend. It is meant to help another AI or another machine continue work without depending on chat history.

## 1. Project Overview

- Project name: Sunrise Electricals
- Type: Angular frontend application
- Current stack: Angular 22, TypeScript, Vite-based Angular build tooling, SSR support
- Package manager: npm
- Main goal: likely an electricals/electrical products business website or dashboard, but the current codebase is still a starter Angular app and not yet a complete business implementation

## 2. Tech Stack

- Angular: ^22.1.0
- TypeScript: ~6.0.2
- RxJS: ~7.8.0
- Angular SSR: @angular/ssr
- Express: ^5.1.0
- Vitest: ^4.0.8
- Prettier: ^3.8.1

## 3. Project Structure

```text
sunrise-electricals/
  AGENTS.md
  CLAUDE.md
  PROJECT_CONTEXT.md
  DEVELOPMENT_RULES.md
  README.md
  angular.json
  package.json
  public/
  src/
    app/
      app.config.ts
      app.routes.ts
      app.ts
      core/
        layout/
          site-shell.ts
          site-header.ts
          site-footer.ts
      features/
        home/
        products/
        price-lists/
        quote/
        about/
        contact/
      shared/
        components/
        data/
        models/
        services/
    index.html
    styles.css
```

## 4. Important Rule Files

These files are not part of the build, but they define coding expectations for AI assistants:

- `AGENTS.md`: project instructions for AI coding agents
- `CLAUDE.md`: Claude-specific instructions for this repo
- `PROJECT_CONTEXT.md`: this file, intended as persistent project memory and handoff context

## 5. Current State of the App

The app is no longer a default Angular starter. It is a first B2B wholesale site with:

- site shell, header, footer, and mobile sidenav
- lazy-loaded routes for Home, Products, Price Lists, Quote, About, and Contact
- shared catalog models, mock catalog data, and `CatalogService`
- product cards with SKU, MOQ, availability, and quote CTAs
- quote request form used on Home, Quote, and Contact
- category search/filter on the catalog page

Backend API integration and live PDF downloads are not implemented yet.

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

Follow the rules in `AGENTS.md` and `CLAUDE.md`:

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

## 8. Recommended Future App Structure

The project should likely evolve into something like:

```text
src/
  app/
    features/
      home/
      products/
      categories/
      cart/
      checkout/
      about/
      contact/
    shared/
      components/
      services/
      models/
      pipes/
      utils/
    core/
      routing/
      api/
      guards/
      interceptors/
```

This is a recommended architecture, not a current existing structure.

## 9. Business Context

The repository name and the app name suggest an electricals retail or service business. A future version of this app may need to include features such as:

- product catalog
- product categories
- pricing and availability
- search and filters
- cart / wishlist
- checkout / order form
- customer contact / inquiry pages
- service request workflow
- branch or showroom information
- contact details and map integration

These are not yet implemented in the current codebase.

## 10. Memory and Handoff Notes

When continuing this project in a new session or on another machine:

- use the repo files as the source of truth
- treat `AGENTS.md` / `CLAUDE.md` as rules for AI behavior
- treat this file as the project memory snapshot
- do not assume the app is production-ready; it is currently a starter Angular project
- if the project evolves, update this file to reflect the active implementation and architecture

## 11. Status Summary

Current status:

- Angular 22 frontend with Angular Material
- B2B wholesale landing page and primary business routes are in place
- Catalog, quote, and contact flows use mock data
- Ready for branding polish, real product data, and API integration

## 12. Next Suggested Tasks

1. Define the real business pages (home, products, categories, cart, checkout, contact)
2. Create app routing structure
3. Add shared data models for products and cart state
4. Build a product listing and detail flow
5. Implement styling and branding for Sunrise Electricals
6. Add forms and validation for inquiries / checkout
7. Connect to backend API if available
8. Add tests for critical business flows

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

This is the working memory snapshot for the Sunrise Electricals frontend as of September 2, 2026, with the agreed development rules now in place.

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

#### Product Data Expectations

Each product entry should ideally include:

- product name
- brand
- category
- subcategory
- SKU or part number
- description
- image or placeholder
- price per unit or price reference
- MOQ / bulk pricing if applicable
- availability status
- application / use case
- technical specifications

#### UI Strategy

- premium B2B industrial style
- strong trust-based design
- product cards with specs and CTA buttons
- quote buttons on every product card
- downloadable PDF cards for market price lists
- clean, modern, enterprise-style layout
- responsive design for desktop and mobile negotiation flows

#### Initial Delivery Plan

1. Create a premium landing page for Sunrise Electricals
2. Add a catalog layout with category-based product cards
3. Add product search and filter UI
4. Add quote request form for bulk buyers
5. Add downloadable price list section
6. Add contact and inquiry pages
7. Add business branding, trust sections, and CTA banners
8. Integrate real backend/API later when data source is ready

This update captures the new wholesale electrical business direction and the customer quotation-driven product flow for the app.

## 16. Development Log

### 2026-09-02 - Homepage and B2B Industrial Design

- Created the first premium Sunrise Electricals homepage in the Angular app.
- Introduced Angular Material-based UI components for navigation, cards, form inputs, and CTA blocks.
- Added a modern B2B industrial look with business-focused sections:
  - hero banner
  - trusted brands list
  - category cards
  - featured products
  - price list cards
  - bulk quote inquiry CTA
- Structured the app around the wholesale electrical supply use case rather than retail e-commerce.
- Added the first version of a business-ready, customer-facing landing page for quote-based electrical procurement.
- Added the `DEVELOPMENT_RULES.md` to define standard UI, documentation, and development expectations.

This log ensures the project memory reflects the current implementation and design direction for future work.

## 17. Homepage Layout Refinement

### 2026-09-02 - Industrial B2B Layout Improvement

- Reviewed the homepage and identified that the initial version still looked too generic and starter-like.
- Reworked the homepage into a cleaner, more structured B2B industrial layout.
- Improved the overall hierarchy with:
  - premium header and navigation
  - stronger hero section
  - business-focused CTA blocks
  - better category and product card structure
  - clearer pricing section and sales CTA
- Simplified the design to better match the wholesale electrical distribution business model.
- Kept the layout responsive and suitable for industrial/commercial buyers.

This update reflects the more appropriate business-first layout for Sunrise Electricals.

## 18. First Feature Architecture and Business Pages

### 2026-09-03 - Routing, catalog, and quote flow

- Read `PROJECT_CONTEXT.md` and `DEVELOPMENT_RULES.md` before continuing implementation.
- Replaced the remaining starter/placeholder homepage markup with a routed B2B site shell.
- Added a feature-based structure:
  - `core/layout` for header, footer, and mobile sidenav
  - `shared/models`, `shared/data`, and `CatalogService`
  - reusable `ProductCard` and `QuoteForm`
  - lazy-loaded Home, Products, Price Lists, Quote, About, and Contact pages
- Home now includes hero, stats, brands, categories, featured products, price-list cards, and an on-page quote form.
- Products page supports search plus brand/category filters, including query-param deep links from category cards.
- Quote form validates company, contact, email, phone, and requirement details. SKU can be passed from a product card.
- Price list PDFs remain placeholders until real files are available.
- Design system uses navy primary, gold accent, Material cards/forms, and responsive grids.

Files added or substantially updated:

- `src/app/app.ts`, `src/app/app.html`, `src/app/app.css`, `src/app/app.routes.ts`, `src/app/app.spec.ts`
- `src/index.html`, `src/styles.css`
- `src/app/core/layout/*`
- `src/app/features/home/*`, `products/*`, `price-lists/*`, `quote/*`, `about/*`, `contact/*`
- `src/app/shared/models/catalog.models.ts`
- `src/app/shared/data/catalog.data.ts`
- `src/app/shared/services/catalog.service.ts`
- `src/app/shared/components/product-card.*`
- `src/app/shared/components/quote-form.*`

Next steps:

- Connect quote submissions to a backend or email API
- Replace mock catalog with live product data
- Add real PDF assets for price lists
- Expand product detail pages and technical specification views
- Continue visual polish against a brand identity pack if provided
