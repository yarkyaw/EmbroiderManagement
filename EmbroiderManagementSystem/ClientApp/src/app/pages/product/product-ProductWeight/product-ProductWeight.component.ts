import { HttpParams } from "@angular/common/http";
import { Component, OnInit, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { DxDataGridComponent } from "devextreme-angular";
import CustomStore from "devextreme/data/custom_store";
import { NzMessageService } from "ng-zorro-antd/message";
import { NzModalService } from "ng-zorro-antd/modal";
import { NzNotificationService } from "ng-zorro-antd/notification";
import { AuthService } from "../../../core/auth.service";
import { Utilities } from "../../../core/utilities";
import { ProductService } from "../../../service/product/product.service";
@Component({
  selector: "app-product-ProductWeight",
  templateUrl: "./product-ProductWeight.component.html",
  styleUrls: ["./product-ProductWeight.component.scss"]
})

export class ProductProductWeightComponent implements OnInit {
  @ViewChild('clientGrid') clientGrid: DxDataGridComponent;
  form: FormGroup;
  dataSource: any = {};
  categories = [];
  toogle = false;
  msgid: any;
  modalRef: any;
  result = '';
  demoValue: number = 0;
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
        return service.getPaginate(params, 'ProductWeight')
          .toPromise()
          .then((data: any) => {
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
      gram: [null, [Validators.required]],
      name: [null, [Validators.required]],
      localizeName: [null, [Validators.required]],
    });
  }

  gramChanged(val) {
    Promise.resolve().then(() => {
      if (val) {
        this.form.controls.localizeName.setValue(Utilities.gramToKyat(val));
      }
      else {
        this.form.controls.localizeName.setValue('');
      }
      this.form.controls.localizeName.updateValueAndValidity();
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
  submitForm() {
    if (this.form.valid) {
      let formValue = this.form.value;
      let obj = {
        id: formValue.id?formValue.id:0,
        gram: formValue.gram,
        name: formValue.name,
        localizeName: formValue.localizeName,
      };
      obj.id = obj.id ? obj.id : 0;

      this.service.save(obj, 'ProductWeight').subscribe(x => {
        this.form.reset();
        this.form = this.fb.group({
          id: 0,
          gram: [null, [Validators.required]],
          name: [null, [Validators.required]],
          localizeName: [null, [Validators.required]],
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
      gram: [model.gram, Validators.required],
      localizeName: [model.localizeName, [Validators.required]],
      name: [model.name, [Validators.required]],
    });
    if (!this.toogle) {
      this.showForm();
    }
  }

  cancel(e: Event) {
    e.preventDefault();
    this.showForm();
    this.form.reset();
  }
}

