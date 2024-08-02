import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmbroiderComponent } from './embroider.component';


const routes: Routes = [
    {
      path: '',
      pathMatch: 'full',
      redirectTo: '/dashboard/embroider/list',
    },
    { path: 'list', component: EmbroiderComponent, data: { title: 'Embroder', breadcrumb: 'Embroider' } },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmbroiderRoutingModule { }