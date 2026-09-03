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

export interface ProductItem {
  id: string;
  name: string;
  brand: string;
  category: string;
  sku: string;
  description: string;
  priceLabel: string;
  badge: string;
  availability: 'In Stock' | 'Made to Order' | 'Limited';
  moq: string;
  application: string;
}

export interface PriceListItem {
  id: string;
  brand: string;
  title: string;
  date: string;
  size: string;
}

export interface NavLink {
  label: string;
  path: string;
}
