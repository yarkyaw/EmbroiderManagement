import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CustomerComponent } from './customer.component';

const routes: Routes = [
    {
      path: '',
      pathMatch: 'full',
      redirectTo: '/lsit',
    },
    { path: 'list', component: CustomerComponent, data: { title: 'Customers', breadcrumb: 'Manage Customers' } },
    
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }