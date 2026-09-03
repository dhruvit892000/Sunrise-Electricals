# Sunrise Electricals Development Rules

This file defines the working standards for the Sunrise Electricals frontend project. All future development must follow these rules unless a higher-priority requirement is explicitly approved.

## 1. UI / Design Standards

- Use the latest modern design systems and component patterns.
- Prefer Angular Material (latest stable version) for reusable UI components where possible.
- Use Material 3-inspired styling patterns with responsive spacing, modern cards, elevated surfaces, and clean typography.
- Keep the interface visually premium, clean, and conversion-focused.
- Use a consistent design language across the app: colors, spacing, buttons, cards, forms, tables, and dialogs.
- Use polished, business-friendly layouts for retail, wholesale, and B2B electrical sales.

## 2. Layout and Styling

- Build layouts using modern responsive grids and flexbox patterns.
- Ensure every page works across mobile, tablet, and desktop.
- Use accessible typography and strong visual hierarchy.
- Keep the design lightweight but premium.
- Prefer reusable component styling with CSS variables, utility classes, and modular SCSS/CSS rules.
- Do not use outdated plain starter template styling as final product UI.

## 3. Dynamic / Responsive Development

- Build the application dynamically using reusable, data-driven components.
- Avoid hardcoded static pages unless required for initial scaffolding.
- Use component-driven architecture so new sections can be easily reused.
- Use responsive behavior for all core screens and elements.
- Design for real-world business scenarios: product listings, categories, inquiry forms, quote requests, contact, and checkout-like flows.

## 4. Modern Frontend Practices

- Prefer standalone Angular components.
- Prefer Angular signals for local reactive state where useful.
- Keep code modular and maintainable.
- Use service-layer patterns for API/data handling.
- Use lazy loading for major features when the app grows.
- Keep business logic separate from UI logic.

## 5. Technical Standards

- Keep TypeScript strict and type-safe.
- Avoid `any` where possible.
- Prefer clean component structure and maintainable naming.
- Use semantic HTML and accessible interactions.
- Keep performance in mind: avoid heavy unnecessary rendering and assets.

## 6. Business-Focused UI Requirements

The website should feel like a professional electrical business website, including:

- strong hero section
- product/category showcases
- trust-building content
- customer inquiry / quote request flow
- contact and support section
- brand-focused landing page
- modern cards and CTA buttons
- business-ready, commercial appearance

## 7. Project Documentation Requirement

Every development activity must be recorded in `PROJECT_CONTEXT.md` in parallel with implementation.

When changes are made, update `PROJECT_CONTEXT.md` with:

- date
- feature or module developed
- files modified
- design decisions
- key implementation notes
- next steps or follow-up tasks

This ensures project continuity across sessions and machines.

## 8. Development Workflow

- Start by clarifying feature requirements.
- Build the minimum needed UI and logic.
- Keep improvements modern and reusable.
- Update `PROJECT_CONTEXT.md` immediately after meaningful work.
- Keep the repo organized and maintainable.

## 9. Rules for Future Tasks

All future work should follow this principle:

- modern UI
- dynamic, responsive, and scalable
- professional business design
- maintainable code
- always documented in `PROJECT_CONTEXT.md`

## 10. Current Project Direction

The project is now a first-pass B2B wholesale frontend for Sunrise Electricals. Continue with real catalog data, quote submission, PDF downloads, and brand polish while keeping the UI modern, responsive, and documented in `PROJECT_CONTEXT.md`.
