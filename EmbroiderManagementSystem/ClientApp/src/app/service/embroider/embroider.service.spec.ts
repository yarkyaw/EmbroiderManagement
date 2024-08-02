import { EmbroiderService } from "./embroider.service";
import { TestBed } from "@angular/core/testing";

describe("EmbroiderService", () => {

  let service: EmbroiderService;
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        EmbroiderService
      ]
    });
    service = TestBed.get(EmbroiderService);

  });

  it("should be able to create service instance", () => {
    expect(service).toBeDefined();
  });

});
