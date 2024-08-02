import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { EmbroiderInvoiceService } from '../../embroider-invoice/embroider-invoice.service';

@Injectable()
export class EmbroiderInvoiceResolver implements Resolve<Observable<any>> {
    constructor(private service: EmbroiderInvoiceService) { }
    resolve(route: ActivatedRouteSnapshot): Observable<any> {
       return this.service.getById(route.params.id);
    }
}