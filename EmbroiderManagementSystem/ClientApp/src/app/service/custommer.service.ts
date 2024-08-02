import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../core/auth.service';
import { ConfigurationService } from '../core/configuration.service';
import { EndpointBase } from '../core/endpoint-base.service';

@Injectable({
  providedIn: 'root'
})
export class CustommerService extends EndpointBase {

  get customerUrl() { return this.configurations.baseUrl + '/api/customer/GetAllCustomerWithFilter'; }

  constructor(http: HttpClient, private configurations: ConfigurationService, authService: AuthService) {
    super(http, authService);
  }

  getCustomers(
    pageIndex: number,
    pageSize: number,
    sortField: string | null,
    sortOrder: string | null,
    filters: Array<{ key: string; value: string[] }>
  ): Observable<{ results: [] }> {
    console.log(filters)
    // let params = new HttpParams()
    //   .append('page', `${pageIndex}`)
    //   .append('results', `${pageSize}`)
    //   .append('sortField', `${sortField}`)
    //   .append('sortOrder', `${sortOrder}`);
    // // filters.forEach(filter => {
    // //   filter.value.forEach(value => {
    // //     params = params.append(filter.key, value);
    // //   });
    // // });

    let Filter: any;
    if (filters && filters.length > 0) {
      pageIndex = 1;
      Filter = {
        Logic: "or",
        filters: [
          {
            Field: "name",
            Operator: filters[0].key,
            Value: filters[0].value[0],
            Logic: "or",
          },
          {
            Field: "email",
            Operator: filters[0].key,
            Value: filters[0].value[0],
            Logic: "or",
          }
        ]
      }
    }
    else {
      Filter = null;
    }

    let obj = {
      Offset: (pageIndex - 1) * pageSize,
      Limit: pageSize,
      Sort: [{
        Field: sortField ? sortField : 'name',
        Dir: sortOrder ? sortOrder.replace('end', '') : 'desc'
      }],
      Filter: Filter
    };
    return this.http.post<any>(this.customerUrl, JSON.stringify(obj), this.requestHeaders);
  }
}
