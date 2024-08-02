import { NO_ERRORS_SCHEMA } from "@angular/core";
import { ProductSubcategoryComponent } from "./product-subcategory.component";
import { ComponentFixture, TestBed } from "@angular/core/testing";

describe("ProductSubcategoryComponent", () => {

  let fixture: ComponentFixture<ProductSubcategoryComponent>;
  let component: ProductSubcategoryComponent;
  beforeEach(() => {
    TestBed.configureTestingModule({
      schemas: [NO_ERRORS_SCHEMA],
      providers: [
      ],
      declarations: [ProductSubcategoryComponent]
    });

    fixture = TestBed.createComponent(ProductSubcategoryComponent);
    component = fixture.componentInstance;

  });

  it("should be able to create component instance", () => {
    expect(component).toBeDefined();
  });
  
});
