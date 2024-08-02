import { NO_ERRORS_SCHEMA } from "@angular/core";
import { EmbroiderInvoiceListComponent } from "./embroider-invoice-list.component";
import { ComponentFixture, TestBed } from "@angular/core/testing";

describe("EmbroiderInvoiceListComponent", () => {

  let fixture: ComponentFixture<EmbroiderInvoiceListComponent>;
  let component: EmbroiderInvoiceListComponent;
  beforeEach(() => {
    TestBed.configureTestingModule({
      schemas: [NO_ERRORS_SCHEMA],
      providers: [
      ],
      declarations: [EmbroiderInvoiceListComponent]
    });

    fixture = TestBed.createComponent(EmbroiderInvoiceListComponent);
    component = fixture.componentInstance;

  });

  it("should be able to create component instance", () => {
    expect(component).toBeDefined();
  });
  
});
