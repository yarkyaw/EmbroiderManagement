import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, timer } from "rxjs";
import { catchError, switchMap } from "rxjs/operators";
import { AuthService } from "../../core/auth.service";
import { ConfigurationService } from "../../core/configuration.service";
import { EndpointBase } from "../../core/endpoint-base.service";


@Injectable()
export class ProductService extends EndpointBase {
  get productBaseUrl() { return this.configurations.baseUrl + '/api/product/'; }
  constructor(http: HttpClient, private configurations: ConfigurationService, authService: AuthService) {
    super(http, authService);
  }

  saveGroup(model: any): Observable<any> {
    let url = this.productBaseUrl + 'saveGroup';
    return this.http.post<any>(url, JSON.stringify(model), this.requestHeaders)
      .pipe(
        catchError(error => {
          return this.handleError(error, () => this.saveGroup(model));
        })
      );
  }

  saveCategory(model: any): Observable<any> {
    let url = this.productBaseUrl + 'saveCategory';
    return this.http.post<any>(url, JSON.stringify(model), this.requestHeaders)
      .pipe(
        catchError(error => {
          return this.handleError(error, () => this.saveGroup(model));
        })
      );
  }

  save(model: any, objName: string): Observable<any> {
    let url = this.productBaseUrl + 'save' + objName;
    return this.http.post<any>(url, JSON.stringify(model), this.requestHeaders)
      .pipe(
        catchError(error => {
          return this.handleError(error, () => this.save(model, objName));
        })
      );
  }

  hasDuplicateName(objName: string, newValue: string, oldVal?: string): Observable<boolean> {

    let url = this.productBaseUrl + 'has' + objName + 'DuplicateName';
    let params = {
      params: {
        'oldVal': oldVal && oldVal == "null" ? null : oldVal,
        'newVal': newValue
      }
    };
    console.log(params);
    let obj = Object.assign(this.requestHeaders, params);
    return timer(1000)
      .pipe(
        switchMap(() => {
          // Check if username is available
          return this.http.get<boolean>(url, obj)
            .pipe(
              catchError(error => {
                return this.handleError(error, () => this.hasDuplicateName(oldVal, newValue));
              })
            )
        })
      );
  }

  hasDuplicateCode(objName: string, newValue: string, oldVal?: string): Observable<boolean> {
    let url = this.productBaseUrl + 'has' + objName + 'DuplicateCode';
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
  getPaginate(params: HttpParams, objName: string): Observable<any> {
    let url = this.productBaseUrl + 'get' + objName + 'Pagination';
    let obj = Object.assign(this.requestHeaders, {params:params});
    return this.http.get<[]>(url, obj)
      .pipe(
        catchError(error => {
          return this.handleError(error, () => this.getPaginate(params, objName));
        })
      );
  }

  getAll(objName: string): Observable<any> {
    let url = this.productBaseUrl + 'getAll' + objName;
    return this.http.get<[]>(url, this.requestHeaders)
      .pipe(
        catchError(error => {
          return this.handleError(error, () => this.getAll(objName));
        })
      );
  }

  getById(objName: string,id:number): Observable<any> {
    let url = this.productBaseUrl + 'getById' + objName;
    let params=new HttpParams()
    .append('id',`${id}`);
    
    let obj = Object.assign(this.requestHeaders, {params:params});
    return this.http.get<[]>(url, obj)
      .pipe(
        catchError(error => {
          return this.handleError(error, () => this.getById(objName, id));
        })
      );
  }

  getByParentId(objName:string,id:number): Observable<any> {
    let url = this.productBaseUrl + 'getByParentId'+objName;
    let params=new HttpParams()
    .set('id', `${id}`);
    
    let obj = Object.assign(this.requestHeaders, {params:params});
    console.log(obj);
    return this.http.get<[]>(url, obj)
      .pipe(
        catchError(error => {
          return this.handleError(error, () => this.getByParentId(objName,id));
        })
      );
  }
}
