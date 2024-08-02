import { Component, OnInit } from "@angular/core";
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators, ValidationErrors } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { NzMessageService } from "ng-zorro-antd/message";
import { NzModalService } from "ng-zorro-antd/modal";
import { NzNotificationService } from "ng-zorro-antd/notification";
import { BehaviorSubject, forkJoin } from "rxjs";
import { debounceTime, map, pairwise, startWith } from "rxjs/operators";
import { AuthService } from "src/app/core/auth.service";
import { EmbroiderOrderService } from "../../../service/embroider-order/embroider-order.service";
import { EmbroiderService } from "../../../service/embroider/embroider.service";
import { ProductService } from "../../../service/product/product.service";
declare var $: any;
@Component({
  selector: "app-embroider-order-from",
  templateUrl: "./embroider-order-from.component.html",
  styleUrls: ["./embroider-order-from.component.scss"]
})

export class EmbroiderOrderFromComponent implements OnInit {
  showLoading = false;
  dateFormat = 'dd/MM/yyyy';
  embroiders = new Array<any>();
  categories = new Array<any>();
  jewelList = new Array<any>();
  subCategories = new Array<any>();
  productWeights = new Array<any>();
  showJewelPanel = false;
  form: FormGroup;
  embroiderOrder: any;
  listOfSubCategoryId: number[] = [];
  goldGradeList = new Array<any>();
  searchChange$ = new BehaviorSubject('');
  searchCategoryList = new Array<any>();

  constructor(private router: Router, private route: ActivatedRoute, private fb: FormBuilder, private modal: NzModalService, private productService: ProductService, private embroiderService: EmbroiderService, private messageService: NzMessageService, private notificationService: NzNotificationService, private service: EmbroiderOrderService, private authService: AuthService) {

  }

  ngOnInit() {
    this.showLoading = true;
    this.form = this.fb.group({
      id: 0,
      paidGold: [null, [Validators.required]],
      paidJewel: [null, [this.jewelConditionallyRequiredValidator]],
      orderType: [null, [Validators.required]],
      categoryId: [null, [Validators.required]],
      orderDate: [new Date(), [Validators.required]],
      embroiderId: [null, [Validators.required]],
      productWeightId: [null, [Validators.required]],
      goldGradeId: [null, [Validators.required]],
      golds: this.fb.array([]),
      jewels: this.fb.array([]),
    });

    this.route.data.subscribe(data => {
      console.log(data);
      this.embroiderOrder = data.embroiderOrder;
      forkJoin([this.productService.getAll('Categories'), this.embroiderService.getAll(), this.service.getJson(), this.productService.getAll('ProductWeights')])
        .subscribe((results: any) => {
          this.categories = results[0];
          this.searchCategoryList = [...this.categories];
          this.embroiders = results[1];
          this.jewelList = results[2].jewels;
          this.goldGradeList = results[2].gold_grades;
          this.productWeights = results[3];
          this.showLoading = false;
        });
      if (!this.embroiderOrder) {
        this.generateNewForm();
      }
      else {
        this.generateUpdateForm();
      }
    });


    this.form.get('categoryId')
      .valueChanges
      .pipe(startWith(null as string), pairwise())
      .subscribe(([prev, next]: [any, any]) => {
        if (next && prev != next) {
          this.productService.getByParentId('subCategory', next)
            .subscribe(x => {
              this.subCategories = x;
              this.golds.controls.map((x: any) => {
                x.controls['subCategoryId'].setValue(null);
              })
            });
        }
        else {
          this.subCategories = [];
          this.golds.controls.map((x: any) => {
            x.controls['subCategoryId'].setValue(null);
          });
        }

      });

    this.form.get("orderType").valueChanges.subscribe(x => {
      if (x == 15) {
        if (this.jewels.length <= 0) {
          this.addJewel()
        }
        this.showJewelPanel = true;
      }
      else {
        (this.form.controls.jewels as FormArray).clear();
        this.form.get('paidJewel').setValue(null);
        this.showJewelPanel = false;
      }
    });

    $("#orderDate").attr("tabindex", -1).focus();
  }

  onSearchCategory(value: string): void {

    this.searchChange$
      .asObservable()
      .pipe(
        debounceTime(500),
        map(x => x)
      )
      .subscribe(x => {
        if (x) {
          this.searchCategoryList = this.categories.filter(x => x.name.includes(value));
        }
        else {
          this.searchCategoryList = [...this.categories];
        }
      });
  }
  generateNewForm() {
    this.addGold(null, true);
  }

  generateUpdateForm() {
    if (this.embroiderOrder.orderType == 15) {
      this.showJewelPanel = true;
    }
    this.productService.getByParentId('SubCategory', this.embroiderOrder.categoryId)
      .subscribe(x => {
        this.subCategories = x;
      });
    let goldDetails = this.embroiderOrder.orderDetails.filter(x => x.materialType == 5);
    let jewelDetails = this.embroiderOrder.orderDetails.filter(x => x.materialType == 10);
    this.form = this.fb.group({
      id: this.embroiderOrder.id,
      paidGold: [this.embroiderOrder.paidGold, [Validators.required]],
      goldGradeId: [this.embroiderOrder.goldGradeId, [Validators.required]],
      paidJewel: [this.embroiderOrder.paidJewel, [this.jewelConditionallyRequiredValidator]],
      orderType: [`${this.embroiderOrder.orderType}`, [Validators.required]],
      categoryId: [this.embroiderOrder.categoryId, [Validators.required]],
      orderDate: [this.embroiderOrder.orderDate, [Validators.required]],
      embroiderId: [this.embroiderOrder.embroiderId, [Validators.required]],
      productWeightId: [this.embroiderOrder.productWeightId, [Validators.required]],
      golds: this.fb.array([]),
      jewels: this.fb.array([]),
    });
    goldDetails.map((x, idx) => this.addGold(x, idx == 0));
    jewelDetails.map(x => this.addJewel(x));
  }

  jewelConditionallyRequiredValidator(formControl: AbstractControl) {
    if (!formControl.parent) {
      return null;
    }

    if (formControl.parent.get('orderType').value && formControl.parent.get('orderType').value == 15) {
      return Validators.required(formControl);
    }
    return null;

  }

  updateJewelValidator(val) {
    Promise.resolve().then(() => {
      if (val && val == 15) {
        if (this.golds.controls.length > 1) {
          this.form.controls.golds = this.fb.array([this.golds.controls[0]]);
        }
      }
      this.form.controls.paidJewel.updateValueAndValidity();
    });

  }



  get golds() {
    return this.form.controls["golds"] as FormArray;
  }

  get jewels() {
    return this.form.controls["jewels"] as FormArray;
  }

  addGold(model?: any, isFirst: boolean = false) {
    const goldForm = this.fb.group({
      id: model ? model.id : 0,
      subCategoryId: [model ? model.subCategoryId : null, Validators.required],
      quantity: [model ? model.quantity : null, Validators.required],
      description: [model ? model.description : null],
      materialType: [model ? model.materialType : 5, Validators.required],
      ratio: [model ? model.ratio : 1, Validators.required]
    });

    this.golds.push(goldForm);
    if (!isFirst) {
      let id = this.golds.length - 1;
      setTimeout(() => {
        $(`#subCategoryId${id}`).children().first().attr("tabindex", -1).focus();
      }, 300);

    }
  }

  addJewel(model?: any) {
    const jewelForm = this.fb.group({
      id: model ? model.id : 0,
      subCategoryId: [model ? model.subCategoryId : 0, Validators.required],
      quantity: [model ? model.quantity : null, Validators.required],
      description: [model ? model.description : null, Validators.required],
      materialType: [model ? model.materialType : 10, Validators.required],
      ratio: [model ? model.ratio : null, Validators.required]
    });
    this.jewels.push(jewelForm);
  }

  deleteGold(index: any) {
    this.golds.removeAt(index);
  }

  deleteJewel(index: any) {
    this.jewels.removeAt(index);
  }

  cancel(e: any) {
    e.preventDefault();
    this.router.navigate([`/dashboard/embroiderorder/list`]);
  }

  submitForm() {
    if (this.form.valid) {
      this.showLoading = true;
      let formValue = this.form.value;
      let gList = formValue.golds as Array<any>;
      let jList = formValue.jewels as Array<any>;
      if (jList && jList.length && jList.length > 0) {
        jList.map(x => {
          x.subCategoryId = gList[0].subCategoryId;
        });
      }
      if (formValue.orderType != "15") {
        jList = [];
      }
      let details = [...gList, ...jList];
      let obj = {
        orderNo: 'temp',
        id: formValue.id ? formValue.id : 0,
        paidGold: formValue.paidGold,
        goldGradeId: formValue.goldGradeId,
        paidJewel: formValue.orderType == "15" ? formValue.paidJewel : 0,
        orderType: parseInt(formValue.orderType),
        categoryId: formValue.categoryId,
        orderDate: formValue.orderDate,
        embroiderId: formValue.embroiderId,
        productWeightId: formValue.productWeightId,
        OrderDetails: details
      };
      this.service.save(obj).subscribe(x => {
        this.showLoading = false;
        this.router.navigate([`/dashboard/embroiderorder/list`]);
      }, err => this.showLoading = false
      );

    }
    else {
      this.getFormValidationErrors();
      for (const i in this.form.controls) {
        if (this.form.controls[i] instanceof FormArray) {
          let fa = this.form.controls[i] as FormArray;
          for (const j in fa.controls) {
            let fg = fa.controls[j] as FormGroup;
            for (const k in fg.controls) {
              fg.controls[k].markAsDirty();
              fg.controls[k].updateValueAndValidity();
            }
          }
        }
        else {
          this.form.controls[i].markAsDirty();

          if (i == "categoryId") {
            if (!this.form.controls[i].valid) {
              this.form.controls[i].updateValueAndValidity();
            }
          }
          else {
            this.form.controls[i].updateValueAndValidity();
          }
        }

      }
    }
  }

  getFormValidationErrors() {
    Object.keys(this.form.controls).forEach(key => {

      const controlErrors: ValidationErrors = this.form.get(key).errors;
      if (controlErrors != null) {
        Object.keys(controlErrors).forEach(keyError => {
          console.log('Key control: ' + key + ', keyError: ' + keyError + ', err value: ', controlErrors[keyError]);
        });
      }
    });
  }
}
