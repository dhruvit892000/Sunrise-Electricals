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
  README.md
  angular.json
  package.json
  tsconfig.json
  tsconfig.app.json
  tsconfig.spec.json
  public/
  src/
    app/
      app.config.ts
      app.config.server.ts
      app.css
      app.html
      app.routes.ts
      app.routes.server.ts
      app.spec.ts
      app.ts
    index.html
    main.server.ts
    main.ts
    server.ts
    styles.css
```

## 4. Important Rule Files

These files are not part of the build, but they define coding expectations for AI assistants:

- `AGENTS.md`: project instructions for AI coding agents
- `CLAUDE.md`: Claude-specific instructions for this repo
- `PROJECT_CONTEXT.md`: this file, intended as persistent project memory and handoff context

## 5. Current State of the App

As of now, the app is in an early default Angular starter state:

- `src/app/app.ts` uses `signal('sunrise-electricals')`
- `src/app/app.html` contains Angular starter template content and default branding
- no real business pages, product catalog, checkout flow, or electricals domain logic are implemented yet
- the app has route support configured but no custom feature modules/pages are present yet

This means the project is not fully built out; it is a clean Angular foundation ready for actual feature development.

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

- Angular project scaffolded successfully
- App boots and is ready for development
- No business-specific UI or functionality implemented yet
- Repo is prepared for feature development and AI-assisted coding

## 12. Next Suggested Tasks

1. Define the real business pages (home, products, categories, cart, checkout, contact)
2. Create app routing structure
3. Add shared data models for products and cart state
4. Build a product listing and detail flow
5. Implement styling and branding for Sunrise Electricals
6. Add forms and validation for inquiries / checkout
7. Connect to backend API if available
8. Add tests for critical business flows

This is the working memory snapshot for the Sunrise Electricals frontend as of September 2, 2026.
