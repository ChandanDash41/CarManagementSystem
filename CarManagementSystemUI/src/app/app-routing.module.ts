import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarModelListComponent } from './Car-Model/car-model-list/car-model-list.component';
import { SalesReportListComponent } from './Sales-Report/sales-report-list/sales-report-list.component';
import { authGuard } from './Guards/auth.guard';
import { LoginComponent } from './Auth/login/login.component';
import { CarModelFormComponent } from './Car-Model/car-model-form/car-model-form.component';
import { SalesDataFormComponent } from './Sales-Report/sales-data-form/sales-data-form.component';

const routes: Routes = [
  { path: 'carModel', component: CarModelListComponent, canActivate: [authGuard] },
  { path: 'carModelForm', component: CarModelFormComponent, canActivate: [authGuard] },
  { path: 'salesReport', component: SalesReportListComponent, canActivate: [authGuard] },
  { path: 'salesDataForm', component: SalesDataFormComponent, canActivate: [authGuard] },
  { path: 'Login', component: LoginComponent },
  { path: '', redirectTo: '/Login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
