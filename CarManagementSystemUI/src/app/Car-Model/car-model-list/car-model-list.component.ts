import { Component } from '@angular/core';
import { ApiService } from '../../Services/api.service';
import { CarModel } from '../../Models/Car-Model.interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-car-model-list',
  templateUrl: './car-model-list.component.html',
  styleUrl: './car-model-list.component.css'
})
export class CarModelListComponent {
  carModels: CarModel[] = [];
  search: string = '';
  orderBy: string = 'DateOfManufacturing';

  constructor(private apiService: ApiService, private router: Router) {}

  ngOnInit(): void {
    this.getCarModels();
  }

  addNewCarModel() {
    this.router.navigate(['/carModelForm']);
  }
  getCarModels(): void {
    this.apiService.getAllCarModels()
      .subscribe((data: any) => {
        this.carModels = data;
      });
  }

  searchCarModels(): void {
    this.apiService.searchCarModels(this.search, this.orderBy).subscribe(data => {
      this.carModels = data;
    });
  }

  deleteCarModel(id: number): void {
    if (confirm('Are you sure you want to delete this model?')) {
      this.apiService.deleteCarModel(id).subscribe(
        () => {
          alert('Car Model deleted successfully!');
          this.getCarModels();
        },
        (error) => {
          console.error('Error deleting car model:', error);
          alert('Failed to delete the car model. Please try again.');
        }
      );
    }
  }
}
