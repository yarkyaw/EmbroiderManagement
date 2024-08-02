import { EmbroiderInvoiceService } from "./embroider-invoice.service";
import { TestBed } from "@angular/core/testing";

describe("EmbroiderInvoiceService", () => {

  let service: EmbroiderInvoiceService;
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        EmbroiderInvoiceService
      ]
    });
    service = TestBed.get(EmbroiderInvoiceService);

  });

  it("should be able to create service instance", () => {
    expect(service).toBeDefined();
  });

});
