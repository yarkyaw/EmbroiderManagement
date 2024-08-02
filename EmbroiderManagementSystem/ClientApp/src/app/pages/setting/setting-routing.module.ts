import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserInfoComponent } from './user-info/user-info.component';
import { UsersManagementComponent } from './users-management/user-management.component';



const routes: Routes = [
    {
      path: '',
      pathMatch: 'full',
      redirectTo: '/dashboard/setting/usermanagement',
    },
    { path: 'usermanagement', component: UsersManagementComponent, data: { title: 'Manage User', breadcrumb: 'ManageUser' } },
   
    { path: 'userinfo/:id', component: UserInfoComponent, data: { title: 'User Info', breadcrumb: 'UserInfo' } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SettingRoutingModule { }