import { NO_ERRORS_SCHEMA } from "@angular/core";
import { EmbroiderOrderFromComponent } from "./embroider-order-from.component";
import { ComponentFixture, TestBed } from "@angular/core/testing";

describe("EmbroiderOrderFromComponent", () => {

  let fixture: ComponentFixture<EmbroiderOrderFromComponent>;
  let component: EmbroiderOrderFromComponent;
  beforeEach(() => {
    TestBed.configureTestingModule({
      schemas: [NO_ERRORS_SCHEMA],
      providers: [
      ],
      declarations: [EmbroiderOrderFromComponent]
    });

    fixture = TestBed.createComponent(EmbroiderOrderFromComponent);
    component = fixture.componentInstance;

  });

  it("should be able to create component instance", () => {
    expect(component).toBeDefined();
  });
  
});
