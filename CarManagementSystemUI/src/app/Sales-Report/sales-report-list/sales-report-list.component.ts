import { Component } from '@angular/core';
import { ApiService } from '../../Services/api.service';
import { PreviousYearSales, SalesForMonth } from '../../Models/Car-Model.interface';

@Component({
  selector: 'app-sales-report-list',
  templateUrl: './sales-report-list.component.html',
  styleUrl: './sales-report-list.component.css'
})
export class SalesReportListComponent {
  salesForMonth: SalesForMonth[] = [];
  previousYearSales: PreviousYearSales[] = [];

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.loadCommissionReport();
  }

  loadCommissionReport(): void {
    this.apiService.getCommissionReport().subscribe(
      (data) => {
        this.processSalesData(data);
      },
      (error) => {
        console.error('Error loading commission report', error);
      }
    );
  }

  processSalesData(rawData: any[]) {
    let salesMap = new Map<string, SalesForMonth>();
    let previousYearSalesMap = new Map<string, number>();
  
    rawData.forEach(sale => {
      let key = `${sale.salesmanName}-${sale.carClass}`; // Ensure grouping by Salesman & Class
  
      // Process Monthly Sales Data
      if (!salesMap.has(key)) {
        salesMap.set(key, {
          salesman: sale.salesmanName,
          class: sale.carClass,
          audi: 0,
          jaguar: 0,
          landRover: 0,
          renault: 0
        });
      }
  
      let record = salesMap.get(key)!;
      switch (sale.brand) {
        case 'Audi':
          record.audi += sale.numberOfCarsSold;
          break;
        case 'Jaguar':
          record.jaguar += sale.numberOfCarsSold;
          break;
        case 'Land Rover':
          record.landRover += sale.numberOfCarsSold;
          break;
        case 'Renault':
          record.renault += sale.numberOfCarsSold;
          break;
      }
  
      // Process Previous Year Sales Data (Summing up Total Commission per Salesman)
      if (!previousYearSalesMap.has(sale.salesmanName)) {
        previousYearSalesMap.set(sale.salesmanName, 0);
      }
      previousYearSalesMap.set(
        sale.salesmanName,
        previousYearSalesMap.get(sale.salesmanName)! + sale.totalCommission
      );
    });
  
    this.salesForMonth = Array.from(salesMap.values())
      .sort((a, b) => a.salesman.localeCompare(b.salesman) || a.class.localeCompare(b.class)); // Sorting by Salesman and Class
  
    this.previousYearSales = Array.from(previousYearSalesMap.entries()).map(([salesman, total]) => ({
      salesman,
      lastYearTotalSale: total
    }));
  }
  

  deleteSalesData(id: number): void {
    this.apiService.deleteSalesData(id).subscribe(() => {
      this.loadCommissionReport();
    });
  }
}
