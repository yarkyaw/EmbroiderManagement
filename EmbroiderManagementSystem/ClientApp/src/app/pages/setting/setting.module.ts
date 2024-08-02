import { NgModule } from '@angular/core';

import { SettingRoutingModule } from './setting-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UsersManagementComponent } from './users-management/user-management.component';
import { UserInfoComponent } from './user-info/user-info.component';
import { AntdModule } from 'src/app/modules/antd.module';

@NgModule({
  imports: [SettingRoutingModule, AntdModule, FormsModule, ReactiveFormsModule, CommonModule],
  declarations: [UsersManagementComponent,UserInfoComponent],
  exports: [UsersManagementComponent]
})
export class SettingModule { }