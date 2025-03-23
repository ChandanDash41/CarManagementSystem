import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../Services/api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sales-data-form',
  templateUrl: './sales-data-form.component.html',
  styleUrl: './sales-data-form.component.css'
})
export class SalesDataFormComponent implements OnInit{
  salesmanId!: number;
  brandId!: number;
  classId!: number;
  numberOfCarsSold!: number;
  modelPrice!: number;
  
  salesmen: any[] = [];
  brands: any[] = [];
  classes: any[] = [];

  constructor(private apiService: ApiService, private router: Router) {}

  ngOnInit(): void {
    
  }

  loadDatas(){
    this.apiService.getSalesmen().subscribe({
      next: (data) => {
        this.salesmen = data;
        console.log('Salesmen:', this.salesmen);
      },
      error: (error) => {
        console.error('Error loading salesmen:', error);
      }
    });

    this.apiService.getBrands().subscribe({
      next: (data) => {
        this.brands = data;
        console.log('Brands:', this.brands);
      },
      error: (error) => {
        console.error('Error loading brands:', error);
      }
    });

    this.apiService.getClasses().subscribe({
      next: (data) => {
        this.classes = data;
        console.log('Classes:', this.classes);
      },
      error: (error) => {
        console.error('Error loading classes:', error);
      }
    });

  }

  createSalesData(): void {
    const newSalesData = {
      salesmanId: this.salesmanId,
      brandId: this.brandId,
      classId: this.classId,
      numberOfCarsSold: this.numberOfCarsSold,
      modelPrice: this.modelPrice
    };

    this.apiService.createSalesData(newSalesData).subscribe({
      next: (data) => {
        console.log('Sales data created successfully', data);
        this.router.navigate(['/sales-data']);
      },
      error: (error) => {
        console.error('Error creating sales data', error);
      }
    });
  }
}
