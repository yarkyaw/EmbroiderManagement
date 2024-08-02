import { NO_ERRORS_SCHEMA } from "@angular/core";
import { ProductCategoryComponent } from "./product-category.component";
import { ComponentFixture, TestBed } from "@angular/core/testing";

describe("ProductCategoryComponent", () => {

  let fixture: ComponentFixture<ProductCategoryComponent>;
  let component: ProductCategoryComponent;
  beforeEach(() => {
    TestBed.configureTestingModule({
      schemas: [NO_ERRORS_SCHEMA],
      providers: [
      ],
      declarations: [ProductCategoryComponent]
    });

    fixture = TestBed.createComponent(ProductCategoryComponent);
    component = fixture.componentInstance;

  });

  it("should be able to create component instance", () => {
    expect(component).toBeDefined();
  });
  
});
