import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmbroiderInvoiceResolver } from 'src/app/service/resolver/embroider-invoice/embroider-invoice.resolver';
import { EmbroiderOrderResolver } from 'src/app/service/resolver/embroider-order/embroider-order.resolver';
import { EmbroiderInvoiceFromComponent } from './embroider-invoice-from/embroider-invoice-from.component';
import { EmbroiderInvoiceListComponent } from './embroider-invoice-list/embroider-invoice-list.component';


const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/dashboard/embroiderinvoice/list',
  },
  { path: 'list', component: EmbroiderInvoiceListComponent, data: { title: 'Embroder Invoices', breadcrumb: 'Embroider Invoices' } },
  {
    path: 'embroiderinvoice', component: EmbroiderInvoiceFromComponent,
    data: { title: 'Embroder Invoice', breadcrumb: 'Embroider Invoice' },
    resolve: { embroiderInvoice: EmbroiderInvoiceResolver }
  },
  {
    path: 'newinvoice/:id', component: EmbroiderInvoiceFromComponent,
    data: { title: 'Embroder Invoice', breadcrumb: 'Embroder Invoice' },
    resolve: { embroiderOrder: EmbroiderOrderResolver }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [EmbroiderOrderResolver]
})
export class EmbroiderInvoiceRoutingModule { }