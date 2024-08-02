import { NO_ERRORS_SCHEMA } from "@angular/core";
import { EmbroiderComponent } from "./embroider.component";
import { ComponentFixture, TestBed } from "@angular/core/testing";

describe("EmbroiderComponent", () => {

  let fixture: ComponentFixture<EmbroiderComponent>;
  let component: EmbroiderComponent;
  beforeEach(() => {
    TestBed.configureTestingModule({
      schemas: [NO_ERRORS_SCHEMA],
      providers: [
      ],
      declarations: [EmbroiderComponent]
    });

    fixture = TestBed.createComponent(EmbroiderComponent);
    component = fixture.componentInstance;

  });

  it("should be able to create component instance", () => {
    expect(component).toBeDefined();
  });
  
});
