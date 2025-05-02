import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { ProductDto } from '../models/product-dto.model';
import { GLOBAL } from '@core/config/app.config';
import { EndpointType } from '@core/enums/endpoint-type.enum';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private apiUrl: string = '';

  constructor(private http: HttpClient) {
    this.apiUrl = `${GLOBAL.apiBaseUrl}/${EndpointType.Products}`;
  }

  getAll(): Observable<ProductDto[]> {
    const url = `${this.apiUrl}/dto`;
    return this.http.get<ProductDto[]>(url);
  }

  searchByName(pattern:string): Observable<ProductDto[]> {
    const url = `${this.apiUrl}/search?search=${pattern}`;

    return this.http.get<ProductDto[]>(url);
  }

  getBiId(id: number): Observable<ProductDto> {
    const url = `${this.apiUrl}/dto/${id}`;
    return this.http.get<ProductDto>(url);
  }

  add(product: Product): Observable<Product> {
    const url = `${this.apiUrl}`;
    return this.http.post<Product>(url, product);
  }

  update(product: Product): Observable<Product> {
    const url = `${this.apiUrl}/${product.id}`;
    return this.http.put<Product>(url, product);
  }

  delete(id: number): Observable<Product> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<Product>(url);
  }
}
