import { NgModule } from '@angular/core';


import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AntdModule } from 'src/app/modules/antd.module';
import { ProductRoutingModule } from './product-routing.module';
import { ProductCategoryComponent } from './product-category/product-category.component';
import { ProductGroupComponent } from './product-group/product-group.component';
import { ProductProductWeightComponent } from './product-ProductWeight/product-ProductWeight.component';
import { ProductSubcategoryComponent } from './product-subcategory/product-subcategory.component';
import { DxDataGridModule,DxButtonModule,DxDateBoxModule,DxFormModule } from "devextreme-angular";
import { ProductService } from '../../service/product/product.service';

@NgModule({
  imports: [ProductRoutingModule, AntdModule, FormsModule, ReactiveFormsModule, CommonModule,DxDataGridModule,DxButtonModule,DxDateBoxModule,DxFormModule],
  declarations: [
      ProductSubcategoryComponent,
      ProductProductWeightComponent,
      ProductGroupComponent,
      ProductCategoryComponent
    ],
  exports: [
    ProductSubcategoryComponent,
    ProductProductWeightComponent,
    ProductGroupComponent,
    ProductCategoryComponent
  ],
  providers:[ProductService]
})
export class ProductModule { }