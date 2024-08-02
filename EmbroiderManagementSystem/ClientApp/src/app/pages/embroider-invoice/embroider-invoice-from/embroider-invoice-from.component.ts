import { Component, OnInit } from "@angular/core";
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators, ValidationErrors, ValidatorFn } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { NzMessageService } from "ng-zorro-antd/message";
import { NzModalService } from "ng-zorro-antd/modal";
import { NzNotificationService } from "ng-zorro-antd/notification";
import { forkJoin } from "rxjs";
import { pairwise, startWith } from "rxjs/operators";
import { AuthService } from "src/app/core/auth.service";
import { EmbroiderInvoiceService } from "src/app/service/embroider-invoice/embroider-invoice.service";
import { EmbroiderOrderService } from "src/app/service/embroider-order/embroider-order.service";
import { EmbroiderService } from "src/app/service/embroider/embroider.service";
import { ProductService } from "src/app/service/product/product.service";

declare var $: any;

@Component({
  selector: "app-embroider-invoice-from",
  templateUrl: "./embroider-invoice-from.component.html",
  styleUrls: ["./embroider-invoice-from.component.scss"]
})

export class EmbroiderInvoiceFromComponent implements OnInit {
  showLoading = false;
  dateFormat = 'dd/MM/yyyy';
  embroiders = [];
  categories = [];
  jewelList = [];
  subCategories = [];
  productWeights = [];
  embroiderOrders = [];
  showJewelPanel = false;
  form: FormGroup;
  embroiderOrder: any;
  embroiderInvoice: any;
  model: any;

  formatMinus = (value: number) => value < 0 ? `${(value)}` : `${value}`;
  parserMinus = (value: string) => value.replace('(', '').replace(')', '');

  conditionalTotalValidatorForDiposal(idx: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.parent) {
        return {};
      }
      else {
        let root = control.parent.parent.parent;
        let invDetails = root.controls['invoiceDetails'] as FormArray;
        let invFg = invDetails.controls[idx] as FormGroup;
        let dipDetails = root.controls['diposalDetails'] as FormArray;
        let dipFg = dipDetails.controls[idx] as FormGroup;
        setTimeout(() => {
          dipFg.controls['actualQuantity'].updateValueAndValidity();
        }, 300);

        if (invFg.controls['quantity'].value && invFg.controls['actualQuantity'].value) {
          if (invFg.controls['quantity'].value != (invFg.controls['actualQuantity'].value + control.value)) {
            return { "quantityNotMatch": true };
          }
          else {
            return {};
          }
        }
        else {
          return {};
        }
      }
    };
  }

  conditionalTotalValidatorForInv(idx: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.parent) {
        return {};
      }
      else {
        let root = control.parent.parent.parent;
        let invDetails = root.controls['invoiceDetails'] as FormArray;
        let invFg = invDetails.controls[idx] as FormGroup;
        let dipDetails = root.controls['diposalDetails'] as FormArray;
        let dipFg = dipDetails.controls[idx] as FormGroup;
        setTimeout(() => {
          invFg.controls['actualQuantity'].updateValueAndValidity();
        }, 300);
        if (invFg.controls['quantity'].valid && dipFg.controls['actualQuantity'].value) {
          if (invFg.controls['quantity'].value != (dipFg.controls['actualQuantity'].value + control.value)) {
            return { "quantityNotMatch": true };
          }
          else {
            return {}
          }
        }
        else {
          return {};
        }
      }
    };
  }



  constructor(private router: Router, private route: ActivatedRoute, private fb: FormBuilder, private modal: NzModalService, private productService: ProductService, private service: EmbroiderInvoiceService, private embroiderService: EmbroiderService, private messageService: NzMessageService, private notificationService: NzNotificationService, private orderService: EmbroiderOrderService, private authService: AuthService) {

  }

  ngOnInit() {
    this.showLoading = true;
    this.form = this.fb.group({
      id: 0,
      paidGold: [null, [Validators.required]],
      paidJewel: [null, [Validators.required]],
      orderType: [null, [Validators.required]],
      invoiceDate: [new Date(), [Validators.required]],
      orderDate: [null, [Validators.required]],
      embroiderId: [null, [Validators.required]],
      categoryId: [null, [Validators.required]],
      productWeightId: [null, [Validators.required]],
      orderId: [null, [Validators.required]],
      diposalGold: [0, [Validators.required]],
      serviceFee: [null, [Validators.required]],
      serviceFeePerItem: [null, [Validators.required]],
      receivedGold: [null, [Validators.required]],
      excessOrLack: [0, [Validators.required]],
      invoiceDetails: this.fb.array([]),
      diposalDetails: this.fb.array([]),
    });


    this.route.data.subscribe(data => {
      this.embroiderOrder = data.embroiderOrder;
      this.embroiderInvoice = data.embroiderInvoice;
      if (this.embroiderInvoice) {
        this.form.get('id').setValue(this.embroiderInvoice.id);
        this.form.get('invoiceDate').setValue(this.embroiderInvoice.invoiceDate);
        this.form.get('diposalGold').setValue(this.embroiderInvoice.diposalGold);
        this.form.get('serviceFee').setValue(this.embroiderInvoice.serviceFee);
        this.form.get('serviceFeePerItem').setValue(this.embroiderInvoice.serviceFeePerItem);
        this.form.get('receivedGold').setValue(this.embroiderInvoice.receivedGold);
        this.form.get('excessOrLack').setValue(this.embroiderInvoice.excessOrLack);
        this.embroiderOrder=this.embroiderInvoice.order;
      }
      
        this.form.get('orderDate').setValue(this.embroiderOrder.orderDate);
        this.form.get('orderType').setValue(this.embroiderOrder.orderType);
        this.form.get('paidGold').setValue(this.embroiderOrder.paidGold);
        this.form.get('orderId').setValue(this.embroiderOrder.id);
        this.form.get('categoryId').setValue(this.embroiderOrder.categoryId);
        this.form.get('paidJewel').setValue(this.embroiderOrder.paidJewel);
        this.form.get('embroiderId').setValue(this.embroiderOrder.embroiderId);
        this.form.get('productWeightId').setValue(this.embroiderOrder.productWeightId);
      
      forkJoin([this.productService.getAll('Categories'), this.embroiderService.getAll(), this.productService.getAll('ProductWeights'), this.productService.getByParentId('SubCategory',this.embroiderOrder.categoryId)])
        .subscribe(results => {
          this.categories=results[0];
          this.embroiders = results[1];
          this.productWeights = results[2];
          this.subCategories = results[3];
          this.showLoading = false;
        });
      
      
      if (!this.embroiderInvoice) {
        let details = this.embroiderOrder.orderDetails.filter(x => x.materialType == 5);
        details.map((x, idx) => {
          this.addDetail(x, idx);
          this.addDiposalDetail(x, idx);
        });
      }
      else {
        let detailsInv = this.embroiderInvoice.invoiceDetails.filter(x => x.detailType == 5);
        let detailsDip = this.embroiderInvoice.invoiceDetails.filter(x => x.detailType == 10);

        detailsInv.map((x) => {
          this.addDetailInv(x);
        });
        detailsDip.map((x) => {
          this.addDiposalDetailInv(x);
        });
      }


      for (let k in this.invoiceDetails.controls) {
        let fg = this.invoiceDetails.controls[k] as FormGroup;
        for (let j in fg.controls) {
          if (j == 'actualQuantity') {
            (fg.controls[j] as FormControl).clearValidators();
            (fg.controls[j] as FormControl).setValidators([Validators.required, this.conditionalTotalValidatorForInv(parseInt(k))]);
            (fg.controls[j] as FormControl).updateValueAndValidity();
          }
        }
      }

      for (let k in this.diposalDetails.controls) {
        let fg = this.diposalDetails.controls[k] as FormGroup;
        for (let j in fg.controls) {
          if (j == 'actualQuantity') {
            (fg.controls[j] as FormControl).clearValidators();
            (fg.controls[j] as FormControl).setValidators([Validators.required, this.conditionalTotalValidatorForDiposal(parseInt(k))]);
            (fg.controls[j] as FormControl).updateValueAndValidity();
          }
        }
      }
    });
  }

  get invoiceDetails() {
    return this.form.controls["invoiceDetails"] as FormArray;
  }

  get diposalDetails() {
    return this.form.controls["diposalDetails"] as FormArray;
  }

  addDetail(model: any, idx) {
    const detailForm = this.fb.group({
      id: 0,
      subCategoryId: [model.subCategoryId, Validators.required],
      quantity: [model.quantity, Validators.required],
      actualQuantity: [null, [Validators.required]],
      detailType: [5, [Validators.required]],
    });
    this.invoiceDetails.push(detailForm);
  }

  addDetailInv(model: any) {
    const detailForm = this.fb.group({
      id: model.id,
      subCategoryId: [model.subCategoryId, Validators.required],
      quantity: [model.quantity, Validators.required],
      actualQuantity: [model.actualQuantity, [Validators.required]],
      detailType: [model.detailType, [Validators.required]],
    });
    this.invoiceDetails.push(detailForm);
  }

  addDiposalDetail(model: any, idx) {
    const diposalForm = this.fb.group({
      id: 0,
      subCategoryId: [model.subCategoryId, Validators.required],
      quantity: [model.quantity, Validators.required],
      actualQuantity: [0, [Validators.required]],
      detailType: [10, [Validators.required]],
    });
    this.diposalDetails.push(diposalForm);
  }

  addDiposalDetailInv(model: any) {
    const diposalForm = this.fb.group({
      id: model.id,
      subCategoryId: [model.subCategoryId, Validators.required],
      quantity: [model.quantity, Validators.required],
      actualQuantity: [model.actualQuantity, [Validators.required]],
      detailType: [model.detailType, [Validators.required]],
    });
    this.diposalDetails.push(diposalForm);
  }

  submitForm() {
    if (this.form.valid) {
      this.showLoading = true;
      let fValue = this.form.value;
      let details = fValue.invoiceDetails;
      let diposalDetails = fValue.diposalDetails;
      let invoiceDetails = [...details, ...diposalDetails];
      let obj = {
        id: fValue.id ? fValue.id : 0,
        paidGold: fValue.paidGold,
        paidJewel: fValue.paidJewel,
        orderType: fValue.orderType,
        categoryId:fValue.categoryId,
        invoiceDate: fValue.invoiceDate,
        orderDate: fValue.orderDate,
        embroiderId: fValue.embroiderId,
        productWeightId: fValue.productWeightId,
        orderId: fValue.orderId,
        diposalGold: fValue.diposalGold,
        serviceFee: fValue.serviceFee,
        serviceFeePerItem: fValue.serviceFeePerItem,
        receivedGold: fValue.receivedGold,
        excessOrLack: fValue.excessOrLack,
        invoiceDetails: invoiceDetails
      };
      console.log(obj);
      this.service.save(obj).subscribe(x => {
        this.showLoading = false;
        this.router.navigate([`/dashboard/embroiderinvoice/list`]);
      }, err => this.showLoading = false
      );
    }
    else {
      this.getFormValidationErrors();
      this.runValidation();
    }
  }

  runValidation() {
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

  cancel(e: Event) {
    e.preventDefault();
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

  updateAllValidator(): void {
    /** wait for refresh value */
    Promise.resolve().then(() => this.runValidation());
  }

  calculateServiceFee() {
    Promise.resolve().then(() => {
      if (this.invoiceDetails.controls.map((x: FormGroup) => x.controls['actualQuantity'].value).filter(x => x == null).length == 0 && this.form.controls.serviceFeePerItem.valid) {
        let qty = this.invoiceDetails.controls.map((x: FormGroup) => x.controls['actualQuantity'].value).reduce((x, y) => x + y);

        this.form.controls['serviceFee'].setValue(this.roundToTwo(this.form.controls.serviceFeePerItem.value * qty));
      }
      else {
        this.form.controls['serviceFee'].setValue(null);
      }
    })

  }

  calculateExcessOrLack() {
    Promise.resolve().then(() => {
      if (this.form.controls.diposalGold.valid && this.form.controls.serviceFee.valid && this.form.controls.receivedGold.valid ) {
        console.log('enter');
        let temp = this.form.controls.diposalGold.value + this.form.controls.serviceFee.value + this.form.controls.receivedGold.value ;  
        this.form.controls.excessOrLack.setValue(this.roundToTwo(this.form.controls.paidGold.value - temp) * -1);
      }
      else {
        console.log('no enter');
        this.form.controls.excessOrLack.setValue(null);
      }
    });
  };


  roundToTwo(num) {
    return Math.round(num * 100) / 100.
  }
}
