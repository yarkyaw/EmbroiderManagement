import { Directive, ElementRef, OnInit } from '@angular/core';


@Directive({
  selector: '[appAutofocus]'
})
export class AutofocusDirective implements OnInit {
  constructor(public elementRef: ElementRef) {
    console.log(this.elementRef);
   }

  ngOnInit() {
    
    setTimeout(() => this.elementRef.nativeElement.focus(), 500);
  }
}