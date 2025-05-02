import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from '../models/category.model';
import { GLOBAL } from '@core/config/app.config';
import { EndpointType } from '@core/enums/endpoint-type.enum';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private apiUrl: string = '';

  constructor(private http: HttpClient) {
    this.apiUrl = `${GLOBAL.apiBaseUrl}/${EndpointType.Categories}`;
  }

  getAll(): Observable<Category[]> {
    const url = `${this.apiUrl}`;
    return this.http.get<Category[]>(url);
  }

  getBiId(id: number): Observable<Category> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Category>(url);
  }

  add(category: Category): Observable<Category> {
    const url = `${this.apiUrl}`;
    return this.http.post<Category>(url, category);
  }

  update(category: Category): Observable<Category> {
    const url = `${this.apiUrl}/${category.id}`;
    return this.http.put<Category>(url, category);
  }

  delete(id: number): Observable<Category> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<Category>(url);
  }
}
