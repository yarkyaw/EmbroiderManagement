import { NgModule } from '@angular/core';


import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AntdModule } from 'src/app/modules/antd.module';
import { EmbroiderRoutingModule } from './embroider-routing.module';
import { DxDataGridModule,DxButtonModule,DxDateBoxModule,DxFormModule } from "devextreme-angular";
import { EmbroiderComponent } from './embroider.component';
import { EmbroiderService } from 'src/app/service/embroider/embroider.service';

@NgModule({
  imports: [EmbroiderRoutingModule, AntdModule, FormsModule, ReactiveFormsModule, CommonModule,DxDataGridModule,DxButtonModule,DxDateBoxModule,DxFormModule],
  declarations: [
     EmbroiderComponent
    ],
  exports: [
    EmbroiderComponent
  ],
  providers:[EmbroiderService]
})
export class EmbroiderModule { }