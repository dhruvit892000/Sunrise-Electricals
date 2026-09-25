                         SUNRISE ELECTRICALS
                                │
                                ▼
                             HOME
                                │
              ┌─────────────────┼─────────────────┐
              ▼                 ▼                 ▼
          PRODUCTS         PRICE LISTS       ABOUT / CONTACT
              │
              ▼
          CATEGORY
              │
              ▼
       SEARCH / FILTER
              │
              ▼
        PRODUCT DETAIL
          │         │
          │         └──────────► RELATED PRODUCTS
          │
          ▼
    REQUEST BULK QUOTE
              │
              ▼
          QUOTE PAGE
              │
              ▼
      CUSTOMER DETAILS
              │
              ▼
      PRODUCT / QUANTITY
              │
              ▼
       SUBMIT ENQUIRY

       

                                ┌───────────────────┐
                         │ Manufacturer Sites│
                         └─────────┬─────────┘
                                   │
                         Import / Sync Process
                                   │
                                   ▼
                         ┌───────────────────┐
                         │   SQL Server      │
                         │                   │
                         │ Brands            │
                         │ Categories        │
                         │ Products          │
                         │ Specifications    │
                         │ Images            │
                         │ ProductSources    │
                         │ Quotes            │
                         │ Replies           │
                         └─────────┬─────────┘
                                   │
                              Dapper/Data
                                   │
                                   ▼
                         ┌───────────────────┐
                         │ Business Layer    │
                         └─────────┬─────────┘
                                   │
                                   ▼
                         ┌───────────────────┐
                         │ ASP.NET Core API  │
                         └─────────┬─────────┘
                                   │
                                   ▼
                         ┌───────────────────┐
                         │ Angular 22        │
                         │ Sunrise Website   │
                         └───────────────────┘






                         EXTERNAL WORLD
     │
     ├── Schneider API
     ├── Siemens data/feed
     ├── Hager catalogue/API
     ├── L&T data/feed
     ├── Polycab catalogue
     ├── KEI catalogue
     ├── RR Kabel catalogue
     ├── Crompton catalogue
     ├── Havells catalogue
     └── Other sources
              │
              ▼
       DATA INGESTION
              │
              ▼
       Sunrise Database
              │
      ┌───────┼────────┐
      ▼       ▼        ▼
   Brands  Products  Categories
              │
       ┌──────┼───────┐
       ▼      ▼       ▼
   Images   Specs   Source Data
              │
              ▼
       Sunrise API
              │
              ▼
       Angular Website