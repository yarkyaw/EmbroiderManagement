import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmbroiderOrderResolver } from 'src/app/service/resolver/embroider-order/embroider-order.resolver';
import { EmbroiderOrderFromComponent } from './embroider-order-from/embroider-order-from.component';
import { EmbroiderOrderListComponent } from './embroider-order-list/embroider-order-list.component';


const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/dashboard/embroiderorder/list',
  },
  { path: 'list', component: EmbroiderOrderListComponent, data: { title: 'Embroder', breadcrumb: 'Embroider' } },
  { path: 'embroiderorder', component: EmbroiderOrderFromComponent, data: { title: 'Embroder', breadcrumb: 'Embroider' } },
  {
    path: 'embroiderorder/:id', component: EmbroiderOrderFromComponent,
    data: { title: 'Embroder', breadcrumb: 'Embroider' },
    resolve: { embroiderOrder: EmbroiderOrderResolver }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [EmbroiderOrderResolver]
})
export class EmbroiderOrderRoutingModule { }