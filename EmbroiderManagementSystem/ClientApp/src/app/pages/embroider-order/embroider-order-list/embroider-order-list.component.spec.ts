import { NO_ERRORS_SCHEMA } from "@angular/core";
import { EmbroiderOrderListComponent } from "./embroider-order-list.component";
import { ComponentFixture, TestBed } from "@angular/core/testing";

describe("EmbroiderOrderListComponent", () => {

  let fixture: ComponentFixture<EmbroiderOrderListComponent>;
  let component: EmbroiderOrderListComponent;
  beforeEach(() => {
    TestBed.configureTestingModule({
      schemas: [NO_ERRORS_SCHEMA],
      providers: [
      ],
      declarations: [EmbroiderOrderListComponent]
    });

    fixture = TestBed.createComponent(EmbroiderOrderListComponent);
    component = fixture.componentInstance;

  });

  it("should be able to create component instance", () => {
    expect(component).toBeDefined();
  });
  
});
