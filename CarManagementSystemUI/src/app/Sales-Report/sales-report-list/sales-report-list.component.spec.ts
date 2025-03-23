import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SalesReportListComponent } from './sales-report-list.component';

describe('SalesReportListComponent', () => {
  let component: SalesReportListComponent;
  let fixture: ComponentFixture<SalesReportListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SalesReportListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SalesReportListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
