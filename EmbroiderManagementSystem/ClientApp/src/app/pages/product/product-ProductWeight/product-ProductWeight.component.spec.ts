import { NO_ERRORS_SCHEMA } from "@angular/core";
import { ProductProductWeightComponent } from "./product-ProductWeight.component";
import { ComponentFixture, TestBed } from "@angular/core/testing";

describe("ProductProductWeightComponent", () => {

  let fixture: ComponentFixture<ProductProductWeightComponent>;
  let component: ProductProductWeightComponent;
  beforeEach(() => {
    TestBed.configureTestingModule({
      schemas: [NO_ERRORS_SCHEMA],
      providers: [
      ],
      declarations: [ProductProductWeightComponent]
    });

    fixture = TestBed.createComponent(ProductProductWeightComponent);
    component = fixture.componentInstance;

  });

  it("should be able to create component instance", () => {
    expect(component).toBeDefined();
  });
  
});
