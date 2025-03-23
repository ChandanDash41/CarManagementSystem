import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CarModel } from '../Models/Car-Model.interface';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'https://localhost:7111/api'; // Replace with actual backend URL

  constructor(private http: HttpClient) {}

  getAllCarModels(): Observable<CarModel[]> {
    return this.http.get<CarModel[]>(`${this.apiUrl}/CarModel/GetAll`);
  }

  searchCarModels(search: string, orderBy: string): Observable<CarModel[]> {
    return this.http.get<CarModel[]>(`${this.apiUrl}/CarModel/GetCarModels?search=${search}&orderBy=${orderBy}`);
  }

  addCarModel(model: FormData): Observable<any> {
    return this.http.post(`${this.apiUrl}/CarModel/AddCarModel`, model, {
      reportProgress: true,
      observe: 'response'
    });
  }

  deleteCarModel(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/CarModel/DeleteCarModel/${id}`);
  }

  getCommissionReport(): Observable<any> {
    return this.http.get(`${this.apiUrl}/SalesmanCommission/commissionReport`);
  }

  createSalesData(data: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, data);
  }

  updateSalesData(id: number, data: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, data);
  }

  deleteSalesData(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }

  getSalesmen(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/salesdata/salesmen`);
  }

  getBrands(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/salesdata/brands`);
  }

  getClasses(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/salesdata/classes`);
  }
}
