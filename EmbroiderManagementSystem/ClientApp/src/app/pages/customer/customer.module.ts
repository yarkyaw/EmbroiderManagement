import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AntdModule } from 'src/app/modules/antd.module';
import { CustomerComponent } from './customer.component';
import { CustomerRoutingModule } from './customer.routing.module';

@NgModule({
  imports: [CustomerRoutingModule, AntdModule, FormsModule, ReactiveFormsModule, CommonModule],
  declarations: [CustomerComponent],
  exports: [CustomerComponent]
})
export class CustomerModule { }