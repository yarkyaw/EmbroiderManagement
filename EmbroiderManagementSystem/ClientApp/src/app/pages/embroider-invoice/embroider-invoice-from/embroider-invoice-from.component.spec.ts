import { NO_ERRORS_SCHEMA } from "@angular/core";
import { EmbroiderInvoiceFromComponent } from "./embroider-invoice-from.component";
import { ComponentFixture, TestBed } from "@angular/core/testing";

describe("EmbroiderInvoiceFromComponent", () => {

  let fixture: ComponentFixture<EmbroiderInvoiceFromComponent>;
  let component: EmbroiderInvoiceFromComponent;
  beforeEach(() => {
    TestBed.configureTestingModule({
      schemas: [NO_ERRORS_SCHEMA],
      providers: [
      ],
      declarations: [EmbroiderInvoiceFromComponent]
    });

    fixture = TestBed.createComponent(EmbroiderInvoiceFromComponent);
    component = fixture.componentInstance;

  });

  it("should be able to create component instance", () => {
    expect(component).toBeDefined();
  });
  
});
