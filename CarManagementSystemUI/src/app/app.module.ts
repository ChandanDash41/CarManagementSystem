import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { CarModelListComponent } from './Car-Model/car-model-list/car-model-list.component';
import { CarModelFormComponent } from './Car-Model/car-model-form/car-model-form.component';
import { SalesReportListComponent } from './Sales-Report/sales-report-list/sales-report-list.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { apiInterceptor } from './Interceptors/api.interceptor';
import { LoginComponent } from './Auth/login/login.component';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { authInterceptor } from './Interceptors/auth.interceptor';
import { SalesDataFormComponent } from './Sales-Report/sales-data-form/sales-data-form.component';

@NgModule({
  declarations: [
    AppComponent,
    CarModelListComponent,
    CarModelFormComponent,
    SalesReportListComponent,
    LoginComponent,
    SalesDataFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    FormsModule
  ],
  providers: [
    provideClientHydration(),
    provideAnimationsAsync(),
    provideHttpClient(withInterceptors([apiInterceptor])),
    {
      provide: HTTP_INTERCEPTORS,
      useFactory: authInterceptor,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
