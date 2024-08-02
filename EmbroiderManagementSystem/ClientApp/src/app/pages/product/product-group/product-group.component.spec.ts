import { NO_ERRORS_SCHEMA } from "@angular/core";
import { ProductGroupComponent } from "./product-group.component";
import { ComponentFixture, TestBed } from "@angular/core/testing";

describe("ProductGroupComponent", () => {

  let fixture: ComponentFixture<ProductGroupComponent>;
  let component: ProductGroupComponent;
  beforeEach(() => {
    TestBed.configureTestingModule({
      schemas: [NO_ERRORS_SCHEMA],
      providers: [
      ],
      declarations: [ProductGroupComponent]
    });

    fixture = TestBed.createComponent(ProductGroupComponent);
    component = fixture.componentInstance;

  });

  it("should be able to create component instance", () => {
    expect(component).toBeDefined();
  });
  
});
