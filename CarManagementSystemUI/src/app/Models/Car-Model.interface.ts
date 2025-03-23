export interface CarModel {
    id?: number;
    brand: string;
    class: string;
    modelName: string;
    modelCode: string;
    description: string;
    features: string;
    price: number;
    dateOfManufacturing: string;
    isActive: boolean;
    sortOrder: number;
    images?: string[];
  }

export interface PreviousYearSales {
  salesman: string;
  lastYearTotalSale: number; 
}

export interface SalesForMonth {
  salesman: string;
  class: string;
  audi: number;
  jaguar: number;
  landRover: number;
  renault: number;
}