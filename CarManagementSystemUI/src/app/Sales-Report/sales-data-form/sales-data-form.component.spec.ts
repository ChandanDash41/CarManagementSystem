import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SalesDataFormComponent } from './sales-data-form.component';

describe('SalesDataFormComponent', () => {
  let component: SalesDataFormComponent;
  let fixture: ComponentFixture<SalesDataFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SalesDataFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SalesDataFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
