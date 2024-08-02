import { NgModule } from '@angular/core';


import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AntdModule } from 'src/app/modules/antd.module';
import { DxDataGridModule,DxButtonModule,DxDateBoxModule,DxFormModule } from "devextreme-angular";
import { EmbroiderOrderListComponent } from './embroider-order-list/embroider-order-list.component';
import { EmbroiderOrderRoutingModule } from './embroider-order-routing.module';
import { EmbroiderOrderFromComponent } from './embroider-order-from/embroider-order-from.component';
import { EmbroiderOrderService } from '../../service/embroider-order/embroider-order.service';
import { ProductService } from 'src/app/service/product/product.service';
import { EmbroiderService } from 'src/app/service/embroider/embroider.service';

@NgModule({
  imports: [EmbroiderOrderRoutingModule, AntdModule, FormsModule, ReactiveFormsModule, CommonModule,DxDataGridModule,DxButtonModule,DxDateBoxModule,DxFormModule],
  declarations: [
    EmbroiderOrderFromComponent,EmbroiderOrderListComponent
    ],
  exports: [
    EmbroiderOrderFromComponent,EmbroiderOrderListComponent
  ],
  providers:[EmbroiderOrderService,ProductService,EmbroiderService]
})
export class EmbroiderOrderModule { }