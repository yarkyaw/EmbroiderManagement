import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { catchError } from "rxjs/operators";
import { AuthService } from "src/app/core/auth.service";
import { ConfigurationService } from "src/app/core/configuration.service";
import { EndpointBase } from "src/app/core/endpoint-base.service";

/**
 * @description
 * @class
 */
@Injectable()
export class EmbroiderOrderService extends EndpointBase {
  get embroiderOrderBaseUrl() { return this.configurations.baseUrl + '/api/embroiderOrder/'; }
  constructor(http: HttpClient, private configurations: ConfigurationService, authService: AuthService) {
    super(http, authService);
  }

  save(model: any): Observable<any> {
    let url = this.embroiderOrderBaseUrl + 'save';
    return this.http.post<any>(url, JSON.stringify(model), this.requestHeaders)
      .pipe(
        catchError(error => {
          return this.handleError(error, () => this.save(model));
        })
      );
  }

  getJson():Observable<any>{
    return this.http.get("assets/common.json").pipe(
      catchError(error => {
        return this.handleError(error, () => this.getJson());
      })
    );
  }

  getPaginate(params: HttpParams): Observable<any> {
    let url = this.embroiderOrderBaseUrl + 'getPagination';
    let obj = Object.assign(this.requestHeaders, {params:params});
    return this.http.get<[]>(url, obj)
      .pipe(
        catchError(error => {
          return this.handleError(error, () => this.getPaginate(params));
        })
      );
  }

  getById(id:number): Observable<any> {
    let url = this.embroiderOrderBaseUrl + 'getById';
    let params=new HttpParams()
    .append('id',`${id}`);
    
    let obj = Object.assign(this.requestHeaders, {params:params});
    return this.http.get<[]>(url, obj)
      .pipe(
        catchError(error => {
          return this.handleError(error, () => this.getById(id));
        })
      );
  }

}
