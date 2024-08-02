import { EmbroiderOrderService } from "./embroider-order.service";
import { TestBed } from "@angular/core/testing";

describe("EmbroiderOrderService", () => {

  let service: EmbroiderOrderService;
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        EmbroiderOrderService
      ]
    });
    service = TestBed.get(EmbroiderOrderService);

  });

  it("should be able to create service instance", () => {
    expect(service).toBeDefined();
  });

});
