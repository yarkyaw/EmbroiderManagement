import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, timer } from "rxjs";
import { catchError, switchMap } from "rxjs/operators";
import { AuthService } from "src/app/core/auth.service";
import { ConfigurationService } from "src/app/core/configuration.service";
import { EndpointBase } from "src/app/core/endpoint-base.service";

/**
 * @description
 * @class
 */
@Injectable()
export class EmbroiderService extends EndpointBase {
  get embroiderBaseUrl() { return this.configurations.baseUrl + '/api/embroider/'; }
  constructor(http: HttpClient, private configurations: ConfigurationService, authService: AuthService) {
    super(http, authService);
  }

  getAll(): Observable<any> {
    let url = this.embroiderBaseUrl + 'getAll';
    return this.http.get<[]>(url, this.requestHeaders)
      .pipe(
        catchError(error => {
          return this.handleError(error, () => this.getAll());
        })
      );
  }

  getPaginate(params: HttpParams): Observable<any> {
    let url = this.embroiderBaseUrl + 'getPagination';
    let obj = Object.assign(this.requestHeaders, {params:params});
    return this.http.get<[]>(url, obj)
      .pipe(
        catchError(error => {
          return this.handleError(error, () => this.getPaginate(params));
        })
      );
  }

  save(model: any): Observable<any> {
    let url = this.embroiderBaseUrl + 'save';
    return this.http.post<any>(url, JSON.stringify(model), this.requestHeaders)
      .pipe(
        catchError(error => {
          return this.handleError(error, () => this.save(model));
        })
      );
  }

  delete(model: any): Observable<any> {
    let url = this.embroiderBaseUrl + 'delete';
    return this.http.post<any>(url, JSON.stringify(model), this.requestHeaders)
      .pipe(
        catchError(error => {
          return this.handleError(error, () => this.save(model));
        })
      );
  }

  hasDuplicateCode(newValue: string, oldVal?: string): Observable<boolean> {
    let url = this.embroiderBaseUrl + 'hasDuplicateCode';
    let params = {
      params: {
        'oldVal': oldVal && oldVal == "null" ? null : oldVal,
        'newVal': newValue
      }
    };
    let obj = Object.assign(this.requestHeaders, params);
    return timer(1000)
      .pipe(
        switchMap(() => {
          // Check if username is available
          return this.http.get<boolean>(url, obj)
            .pipe(
              catchError(error => {
                return this.handleError(error, () => this.hasDuplicateCode(oldVal, newValue));
              })
            )
        })
      );
  }

}
