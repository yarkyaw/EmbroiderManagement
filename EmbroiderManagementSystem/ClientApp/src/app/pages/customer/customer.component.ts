import { Component, OnInit, ViewChild } from '@angular/core';
import { NzTableComponent, NzTableFilterValue, NzTableQueryParams, NzTableSortOrder } from 'ng-zorro-antd/table';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, startWith } from 'rxjs/operators';
import { CustommerService } from 'src/app/service/custommer.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {
  @ViewChild("customerTable")
  table:NzTableComponent;

  visible=false;
  modelChanged: Subject<string> = new Subject<string>();
  total = 1;
  customers: [] = [];
  loading = true;
  pageSize = 2;
  pageIndex = 1;
  searchValue = '';
  params:NzTableQueryParams;

  constructor(private service :CustommerService) { 
    
  }

  ngOnInit(): void {
    this.modelChanged
    .pipe(
      debounceTime(300),
      distinctUntilChanged(),
    ).subscribe(model => {
      this.pageIndex=1;
      this.loadDataFromServer(this.pageIndex, this.pageSize, null, null, [{key:'contains',value:[model]}]);
    } );
    this.loadDataFromServer(this.pageIndex, this.pageSize, null, null, []);
  }

  loadDataFromServer(
    pageIndex: number,
    pageSize: number,
    sortField: string | null,
    sortOrder: string | null,
    filter: Array<{ key: string; value: string[] }>
  ): void {
    this.loading = true;
    this.service.getCustomers(pageIndex, pageSize, sortField, sortOrder, filter).subscribe((data:any) => {
      this.loading = false;
      this.total = data.item2; // mock the total data here
      this.customers = data.item1;
    });
  }

  onQueryParamsChange(params: NzTableQueryParams): void {
    // if(this.params){
    //   params.filter=this.params.filter;
    //   this.params=params;
    // }
    // else{
    //   this.params=params;
    // }
    // const { pageSize, pageIndex, sort, filter } = params;
    // const currentSort = sort.find(item => item.value !== null);
    // const sortField = (currentSort && currentSort.key) || null;
    // const sortOrder = (currentSort && currentSort.value) || null;
  }

  nameChanged($event: any, first: { value: string; }){
    this.searchValue=first.value;
    this.modelChanged.next(first.value);
  }

  changePage(idx:number){
    if(this.searchValue){
      this.loadDataFromServer(idx, this.pageSize, null, null, [{key:'contains',value:[this.searchValue]}]);
    }
    else{
      this.loadDataFromServer(idx, this.pageSize, null, null, []);
    }
  }

  changePageSize(idx:number){
    this.pageIndex=1;
    this.pageSize=idx;
    if(this.searchValue){
      this.loadDataFromServer(this.pageIndex, this.pageSize, null, null, [{key:'contains',value:[this.searchValue]}]);
    }
    else{
      this.loadDataFromServer(this.pageIndex, this.pageSize, null, null, []);
    }
  }

}
