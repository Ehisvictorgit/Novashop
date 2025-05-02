import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@core/services/auth.service';
import { Category } from '@core/models/category.model';
import { CategoryService } from '@core/services/category.service';
import { ProductDto } from '@core/models/product-dto.model';
import { ProductService } from '@core/services/product.service';

@Component({
  standalone: false,
  selector: 'app-shop-main',
  templateUrl: './shop-main.component.html',
  styleUrls: ['./shop-main.component.css'],
})
export class ShopMainComponent implements OnInit {
  searchTerm: string = '';
  cartCount: number = 0;

  selectedCategory: any = null;
  categories: Category[] = [];
  products: ProductDto[] = [];
  filteredProducts: ProductDto[] = [];

  constructor(
    private auth: AuthService,
    private categoryService: CategoryService,
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getAllCategories();
    this.getAllProducts();
  }

  getAllCategories() {
    this.categoryService.getAll().subscribe({
      next: (data: Category[]) => {
        this.categories = data;
      },
      error: (error) => {
        console.error('Error getting categories:', error);
      },
    });
  }

  getAllProducts() {
    this.productService.getAll().subscribe({
      next: (data: ProductDto[]) => {
        this.products = data;
        this.applyFilters();
      },
      error: (error) => {
        console.error('Error getting products:', error);
      },
    });
  }

  // Search by patterns
  onSearch(): void {
    const term = this.searchTerm.trim();

    if (term.length === 0) {
      this.getAllProducts();
      return;
    }

    this.selectedCategory = null;
    this.productService.searchByName(term).subscribe({
      next: (data: ProductDto[]) => {
        this.filteredProducts = data;
      },
      error: (error) => {
        console.error('Error en búsqueda:', error);
      },
    });
  }

  // Filter by category
  filterByCategory(category: any) {
    this.selectedCategory = category;
    this.searchTerm = '';
    this.getAllProducts();
  }

  // Apply filter by category
  applyFilters() {
    this.filteredProducts = this.products.filter(
      (p) =>
        !this.selectedCategory ||
        this.selectedCategory.name === 'All Products' ||
        p.category === this.selectedCategory.name
    );
  }

  // Adding products to cart (no finished)
  addToCart(product: ProductDto) {
    this.cartCount++;
  }

  // Go to cart (no finished)
  goToCart() {
    console.log('Ir al carrito');
  }

  // Log out
  logout() {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}
