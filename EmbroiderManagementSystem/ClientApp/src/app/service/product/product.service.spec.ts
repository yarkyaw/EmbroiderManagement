import { ProductService } from "./product.service";
import { TestBed } from "@angular/core/testing";

describe("ProductService", () => {

  let service: ProductService;
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        ProductService
      ]
    });
    service = TestBed.get(ProductService);

  });

  it("should be able to create service instance", () => {
    expect(service).toBeDefined();
  });

});
