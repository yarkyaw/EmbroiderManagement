import { NgModule } from '@angular/core';


import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AntdModule } from 'src/app/modules/antd.module';
import { DxDataGridModule,DxButtonModule,DxDateBoxModule,DxFormModule } from "devextreme-angular";
import { EmbroiderOrderService } from '../../service/embroider-order/embroider-order.service';
import { ProductService } from 'src/app/service/product/product.service';
import { EmbroiderService } from 'src/app/service/embroider/embroider.service';
import { EmbroiderInvoiceService } from 'src/app/service/embroider-invoice/embroider-invoice.service';
import { EmbroiderInvoiceRoutingModule } from './embroider-invoice.routing.module';
import { EmbroiderInvoiceFromComponent } from './embroider-invoice-from/embroider-invoice-from.component';
import { EmbroiderInvoiceListComponent } from './embroider-invoice-list/embroider-invoice-list.component';

@NgModule({
  imports: [EmbroiderInvoiceRoutingModule, AntdModule, FormsModule, ReactiveFormsModule, CommonModule,DxDataGridModule,DxButtonModule,DxDateBoxModule,DxFormModule],
  declarations: [
    EmbroiderInvoiceFromComponent,EmbroiderInvoiceListComponent
    ],
  exports: [
    EmbroiderInvoiceFromComponent,EmbroiderInvoiceListComponent
  ],
  providers:[EmbroiderOrderService,ProductService,EmbroiderService,EmbroiderInvoiceService]
})
export class EmbroiderInvoiceModule { }