import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve } from '@angular/router';

import { Observable, of } from 'rxjs';
import { delay } from 'rxjs/operators';
import { EmbroiderOrderService } from '../../embroider-order/embroider-order.service';

@Injectable()
export class EmbroiderOrderResolver implements Resolve<Observable<any>> {
    constructor(private service: EmbroiderOrderService) { }
    resolve(route: ActivatedRouteSnapshot): Observable<any> {
       return this.service.getById(route.params.id);
    }
}