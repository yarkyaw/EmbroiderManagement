import { HttpParams } from "@angular/common/http";
import { Component, OnInit, ViewChild } from "@angular/core";
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormGroup, Validators } from "@angular/forms";
import { DxDataGridComponent } from "devextreme-angular";
import CustomStore from "devextreme/data/custom_store";
import { NzMessageService } from "ng-zorro-antd/message";
import { NzModalService } from "ng-zorro-antd/modal";
import { NzNotificationService } from "ng-zorro-antd/notification";
import { BehaviorSubject, Observable, of } from "rxjs";
import { debounceTime, map, switchMap } from "rxjs/operators";
import { AuthService } from "../../../core/auth.service";
import { Utilities } from "../../../core/utilities";
import { ProductService } from "../../../service/product/product.service";

@Component({
  selector: "app-product-subcategory",
  templateUrl: "./product-subcategory.component.html",
  styleUrls: ["./product-subcategory.component.scss"]
})

export class ProductSubcategoryComponent implements OnInit {

  @ViewChild('clientGrid') clientGrid: DxDataGridComponent;
  form: FormGroup;
  dataSource: any = {};
  categories = [];
  toogle = false;
  msgid: any;
  modalRef: any;
  result = '';
  demoValue: number = 0;
  searchList = [];

  searchChange$ = new BehaviorSubject('');

  constructor(private fb: FormBuilder, private modal: NzModalService, private service: ProductService, private messageService: NzMessageService, private notificationService: NzNotificationService, private authService: AuthService) {

    function isNotEmpty(value: any): boolean {
      return value !== undefined && value !== null && value !== "";
    }

    this.dataSource = new CustomStore({
      key: "id",
      load: function (loadOptions: any) {
        let params: HttpParams = new HttpParams();
        [
          "skip",
          "take",
          "requireTotalCount",
          "requireGroupCount",
          "sort",
          "filter",
          "totalSummary",
          "group",
          "groupSummary"
        ].forEach(function (i) {
          if (i in loadOptions && isNotEmpty(loadOptions[i]))
            params = params.set(i, JSON.stringify(loadOptions[i]));
        });
        return service.getPaginate(params, 'SubCategory')
          .toPromise()
          .then((data: any) => {
            console.log(data.data);
            return {
              data: data.data,
              totalCount: data.totalCount,
              summary: data.summary,
              groupCount: data.groupCount
            };
          })
          .catch(error => { throw 'Data Loading Error' });
      }
    });
  }



  dataChanged(newObj) {
    if (newObj) {
      this.result = Utilities.gramToKyat(newObj);
    }
    else {
      this.result = '';
    }
  }
  ngOnInit() {

    this.service.getAll('Categories').subscribe(x => this.categories = x);
    this.form = this.fb.group({
      id: 0,
      categoryId: [null, [Validators.required]],
      subCategoryCode: [null, [Validators.required], [this.isCodeDuplicate()]],
      name: [null, [Validators.required], [this.isNameDuplicate()]],
    });

  }

  onSearch(value: string): void {
    this.searchChange$
      .asObservable()
      .pipe(
        debounceTime(500),
        map(x => x)
      )
      .subscribe(x => {
        if (x) {
          this.searchList = this.categories.filter(x => x.name.includes(value));
        }
        else {
          this.searchList = [...this.categories];
        }
      });
  }

  onToolbarPreparing(e) {
    e.toolbarOptions.items.unshift(
      {
        location: 'after',
        widget: 'dxButton',
        options: {
          icon: 'add',
          onClick: this.showForm.bind(this)
        }
      });
  }

  showForm() {
    this.toogle = !this.toogle;
  }

  addSubCategory() {
    if (this.form.valid) {
      let formValue = this.form.value;
      let obj = {
        id: formValue.id ? formValue.id : 0,
        categoryId: formValue.categoryId,
        name: formValue.name,
        subCategoryCode: formValue.subCategoryCode,
      };

      this.service.save(obj, 'SubCategory').subscribe(x => {
        this.form.reset();
        this.form = this.fb.group({
          id: 0,
          subCategoryCode: [null, [Validators.required], [this.isCodeDuplicate()]],
          name: [null, [Validators.required], [this.isNameDuplicate()]],
          categoryId: [null, Validators.required],
        });
        this.showForm();
        this.clientGrid.instance.refresh();
      });
    }
    else {
      for (const i in this.form.controls) {
        this.form.controls[i].markAsDirty();
        this.form.controls[i].updateValueAndValidity();
      }
    }
  }

  edit(model: any) {
    this.form = this.fb.group({
      id: model.id,
      categoryId: [model.categoryId, Validators.required],
      subCategoryCode: [model.subCategoryCode, [Validators.required], [this.isCodeDuplicate(model.subCategoryCode)]],
      name: [model.name, [Validators.required], [this.isNameDuplicate(model.name)]],
    });
    if (!this.toogle) {
      this.showForm();
    }
  }

  cancel() {
    this.showForm();
    this.form.reset();
  }

  delete(model: any) {
    this.showConfirm(model);
  }

  deleteCategoryHelper(model) {
    this.createMessage('Deleting...');
    // this.service.deleteCategory(model)
    //   .subscribe(results => {
    //     this.messageService.remove(this.msgid);
    //   },
    //     error => {
    //       this.messageService.remove(this.msgid);
    //       this.createErrorMessage('error', `Delete Error.An error occured whilst deleting the sub category.\r\nError: "${Utilities.getHttpResponseMessages(error)}"`)
    //     });
  }

  createMessage(msg: string): void {
    this.msgid = this.messageService.loading(msg, { nzDuration: 0 }).messageId;

  }

  createErrorMessage(type, msg: string): void {
    this.msgid = this.messageService.create(type, msg, { nzDuration: 500 }).messageId;;
  }


  showConfirm(row): void {
    this.modalRef = this.modal.confirm({
      nzTitle: `Do you Want to delete these ${row.userName}?`,
      nzContent: '',
      nzOnOk: () => {
        this.deleteCategoryHelper(row);
      }
    });
  }

  isNameDuplicate(val: string = null): AsyncValidatorFn {
    return (c: AbstractControl): Observable<{ [key: string]: any } | null> => {
      if (!c.value || c.value.trim().length == 0) {
        return of(null);
      }

      return this.service.hasDuplicateName('SubCategory', c.value, val ? val : '')
        .pipe(
          map(x => {
            if (x == true) {
              return { duplicate: true };
            }
            else {
              return null;
            }
          })
        );
    }
  }

  isCodeDuplicate(val: string = null): AsyncValidatorFn {
    return (c: AbstractControl): Observable<{ [key: string]: any } | null> => {
      if (!c.value || c.value.trim().length == 0) {
        return of(null);
      }

      return this.service.hasDuplicateCode('SubCategory', c.value, val ? val : '')
        .pipe(
          map(x => {
            if (x == true) {
              return { duplicate: true };
            }
            else {
              return null;
            }
          })
        );
    }
  }
}

