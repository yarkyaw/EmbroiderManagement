import { HttpParams } from "@angular/common/http";
import { Component, OnInit, ViewChild } from "@angular/core";
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormGroup, Validators } from "@angular/forms";
import { DxDataGridComponent } from "devextreme-angular";
import CustomStore from "devextreme/data/custom_store";
import { NzMessageService } from "ng-zorro-antd/message";
import { NzModalService } from "ng-zorro-antd/modal";
import { NzNotificationService } from "ng-zorro-antd/notification";
import { Observable, of } from "rxjs";
import { map } from "rxjs/operators";
import { AuthService } from "src/app/core/auth.service";
import { Utilities } from "src/app/core/utilities";
import { EmbroiderService } from "../../service/embroider/embroider.service";

@Component({
  selector: "app-embroider",
  templateUrl: "./embroider.component.html",
  styleUrls: ["./embroider.component.scss"]
})

export class EmbroiderComponent implements OnInit {
  @ViewChild('clientGrid') clientGrid: DxDataGridComponent;
  form: FormGroup;
  dataSource: any = {};
  categories = [];
  toogle = false;
  msgid: any;
  modalRef: any;
  result = '';
  invoiceCount=0;
  constructor(private fb: FormBuilder, private modal: NzModalService, private service: EmbroiderService, private messageService: NzMessageService, private notificationService: NzNotificationService, private authService: AuthService) {

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
        return service.getPaginate(params)
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

  ngOnInit() {
    this.form = this.fb.group({
      id: 0,
      openingBalance: [0, [Validators.required]],
      embroiderCode: [null, [Validators.required], [this.isCodeDuplicate()]],
      name: [null, [Validators.required]],
      phone: [null, [Validators.required]],
      address: [null, [Validators.required]],
      balance:[0,[Validators.required]]
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

  addEmbroider() {
    if (this.form.valid) {
      let formValue = this.form.value;
      let obj = {
        id: formValue.id?formValue.id:0,
        openingBalance: formValue.openingBalance,
        embroiderCode: formValue.embroiderCode,
        name: formValue.name,
        phone: formValue.phone,
        address: formValue.address,
      };
      console.log(obj);

      this.service.save(obj).subscribe(x => {
        this.form.reset();
        this.form = this.fb.group({
          id: 0,
          openingBalance: [0, [Validators.required]],
          balance: [0, [Validators.required]],
          embroiderCode: [null, [Validators.required], [this.isCodeDuplicate()]],
          name: [null, [Validators.required]],
          phone: [null, [Validators.required]],
          address: [null, [Validators.required]],
        });
        this.showForm();
        this.invoiceCount=0;
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
    console.log(model);
    this.invoiceCount=model.invoiceCount;
    this.form = this.fb.group({
      id: model.id,
      openingBalance: [model.openingBalance, [Validators.required]],
      balance: [model.balance, [Validators.required]],
      embroiderCode: [model.embroiderCode, [Validators.required], [this.isCodeDuplicate(model.embroiderCode)]],
      name: [model.name, [Validators.required]],
      phone: [model.phone, [Validators.required]],
      address: [model.address, [Validators.required]],
    });
    if (!this.toogle) {
      this.showForm();
    }
  }

  cancel() {
    this.showForm();
    this.form.reset();
    this.invoiceCount=0;
  }

  delete(model: any) {
    this.showConfirm(model);
  }

  deleteCategoryHelper(model) {
    this.createMessage('Deleting...');
    this.service.delete(model)
      .subscribe(results => {
        this.messageService.remove(this.msgid);
      },
        error => {
          this.messageService.remove(this.msgid);
          this.createErrorMessage('error', `Delete Error.An error occured whilst deleting the sub category.\r\nError: "${Utilities.getHttpResponseMessages(error)}"`)
        });
  }

  createMessage(msg: string): void {
    this.msgid = this.messageService.loading(msg, { nzDuration: 0 }).messageId;

  }

  createErrorMessage(type, msg: string): void {
    this.msgid = this.messageService.create(type, msg, { nzDuration: 500 }).messageId;;
  }


  showConfirm(row): void {
    this.modalRef = this.modal.confirm({
      nzTitle: `Do you Want to delete these ${row.name}?`,
      nzContent: '',
      nzOnOk: () => {
        this.deleteCategoryHelper(row);
      }
    });
  }


  isCodeDuplicate(val: string = null): AsyncValidatorFn {
    return (c: AbstractControl): Observable<{ [key: string]: any } | null> => {
      if (!c.value || c.value.trim().length == 0) {
        return of(null);
      }

      return this.service.hasDuplicateCode(val ? val : '', c.value)
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

