export interface StatItem {
  value: string;
  label: string;
}

export interface CategoryItem {
  slug: string;
  title: string;
  description: string;
  count: string;
  filterKey: string;
}

export type ProductAvailability =
  | 'In Stock'
  | 'Made to Order'
  | 'Limited';

export interface ProductSpecification {
  label: string;
  value: string;
}

export interface ProductItem {
  id: string;
  slug: string;
  name: string;
  brand: string;
  category: string;
  subcategory: string;
  sku: string;
  description: string;
  image: string;
  priceLabel: string;
  badge: string;
  availability: ProductAvailability;
  moq: string;
  application: string;
  specifications: ProductSpecification[];
}

export interface PriceListItem {
  id: string;
  brand: string;
  title: string;
  date: string;
  size: string;
  fileUrl: string;
}

export interface NavLink {
  label: string;
  path: string;
}