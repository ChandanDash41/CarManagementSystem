import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../../Services/api.service';

@Component({
  selector: 'app-car-model-form',
  templateUrl: './car-model-form.component.html',
  styleUrl: './car-model-form.component.css'
})
export class CarModelFormComponent {
  carModelForm!: FormGroup;
  brands: any[] = [];
  classes: any[] = [];
  selectedFiles: File[] = [];
  imageError: string = '';

  constructor(private fb: FormBuilder, private apiService: ApiService) {
    this.loadDatas();
  }

  ngOnInit(): void {
    this.carModelForm = this.fb.group({
      brand: ['', Validators.required],
      class: ['', Validators.required],
      modelName: ['', Validators.required],
      modelCode: ['', [Validators.required, Validators.pattern(/^[a-zA-Z0-9]{10}$/)]],
      description: ['', Validators.required],
      features: ['', Validators.required],
      price: ['', Validators.required],
      dateOfManufacturing: ['', Validators.required],
      isActive: [true],
      sortOrder: ['', Validators.required],
      images: [null]
    });
  }

  loadDatas(){
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

  onFileSelected(event: any): void {
    const files = event.target.files;
    this.imageError = '';
    this.selectedFiles = [];
    for (let file of files) {
      if (file.size > 5 * 1024 * 1024) {
        this.imageError = 'Each file must be under 5MB!';
        return;
      }
      this.selectedFiles.push(file);
    }

    this.carModelForm.patchValue({ images: this.selectedFiles });
    this.carModelForm.get('images')?.updateValueAndValidity();
  }
  
  onSubmit(): void {
    if (this.carModelForm.invalid) {
      alert('Please fill all required fields.');
      return;
    }
  
    const formData = new FormData();
  
    formData.append('brand', this.carModelForm.get('brand')?.value || '');
    formData.append('class', this.carModelForm.get('class')?.value || '');
    formData.append('modelName', this.carModelForm.get('modelName')?.value || '');
    formData.append('modelCode', this.carModelForm.get('modelCode')?.value || '');
    formData.append('description', this.carModelForm.get('description')?.value || '');
    formData.append('features', this.carModelForm.get('features')?.value || '');
    formData.append('price', this.carModelForm.get('price')?.value || '');
    formData.append('dateOfManufacturing', this.carModelForm.get('dateOfManufacturing')?.value || '');
    formData.append('isActive', this.carModelForm.get('isActive')?.value ? 'true' : 'false');
    formData.append('sortOrder', this.carModelForm.get('sortOrder')?.value || '');
  
    if (this.selectedFiles.length > 0) {
      this.selectedFiles.forEach((file, index) => {
        formData.append(`images`, file);
      });
    } else {
      alert('Please upload at least one image.');
      return;
    }

    this.apiService.addCarModel(formData).subscribe(
      (response) => {
        alert('Car Model Added Successfully!');
      },
      (error) => {
        console.error('Error adding car model:', error);
      }
    );
  }  

}
