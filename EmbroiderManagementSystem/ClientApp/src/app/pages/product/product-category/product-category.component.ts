import { Component, OnInit, AfterViewInit, TemplateRef, ViewChild, Input } from '@angular/core';
import { Utilities } from '../../../core/utilities';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { AuthService } from 'src/app/core/auth.service';
import { ProductService } from '../../../service/product/product.service';
import { FormBuilder, FormControl, Validators, FormGroup, AsyncValidatorFn, AbstractControl } from '@angular/forms';
import { HttpParams } from '@angular/common/http';
import CustomStore from 'devextreme/data/custom_store';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { DxDataGridComponent } from 'devextreme-angular';

@Component({
  selector: "app-product-category",
  templateUrl: "./product-category.component.html",
  styleUrls: ["./product-category.component.scss"]
})

export class ProductCategoryComponent implements OnInit {

  @ViewChild('clientGrid') clientGrid: DxDataGridComponent;
  form: FormGroup;
  dataSource: any = {};
  groups = [];
  toogle = false;
  msgid: any;
  modalRef: any;
  result = '';
  demoValue: number = 0;
  showLoading = false;

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
        return service.getPaginate(params, 'category')
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
    this.service.getAll('Groups').subscribe(x => this.groups = x);
    this.form = this.fb.group({
      id: 0,
      categoryCode: [null, [Validators.required], [this.isCodeDuplicate()]],
      name: [null, [Validators.required], [this.isNameDuplicate()]],
      groupId: [null, [Validators.required]],
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

  addCategory() {
    if (this.form.valid) {
      this.showLoading = !this.showLoading;
      let formValue = this.form.value;
      let obj = {
        id: formValue.id ? formValue.id : 0,
        name: formValue.name,
        groupId: formValue.groupId,
        categoryCode: formValue.categoryCode,
      };

      this.service.save(obj, 'Category').subscribe(x => {
        this.form.reset();
        this.form = this.fb.group({
          id: 0,
          categoryCode: [null, [Validators.required], [this.isCodeDuplicate()]],
          name: [null, [Validators.required], [this.isNameDuplicate()]],
          groupId: [null, [Validators.required]],
        });
        this.showForm();
        this.showLoading = !this.showLoading;
        this.clientGrid.instance.refresh();
      }, err => this.showLoading = !this.showLoading);
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
      categoryCode: [model.categoryCode, [Validators.required], [this.isCodeDuplicate(model.categoryCode)]],
      name: [model.name, [Validators.required], [this.isNameDuplicate(model.name)]],
      groupId: [model.groupId, [Validators.required]],
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
    //       this.createErrorMessage('error', `Delete Error.An error occured whilst deleting the category.\r\nError: "${Utilities.getHttpResponseMessages(error)}"`)
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

      return this.service.hasDuplicateName('Category', c.value, val ? val : '')
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

      return this.service.hasDuplicateCode('Category', c.value, val ? val : '')
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

