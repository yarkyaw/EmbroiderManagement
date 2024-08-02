import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductCategoryComponent } from './product-category/product-category.component';
import { ProductGroupComponent } from './product-group/product-group.component';
import { ProductProductWeightComponent } from './product-ProductWeight/product-ProductWeight.component';
import { ProductSubcategoryComponent } from './product-subcategory/product-subcategory.component';



const routes: Routes = [
    {
      path: '',
      pathMatch: 'full',
      redirectTo: '/dashboard/product/group',
    },
    { path: 'group', component: ProductGroupComponent, data: { title: 'Group', breadcrumb: 'Group' } },
   
    { path: 'categroy', component: ProductCategoryComponent, data: { title: 'Category', breadcrumb: 'Category' } },
    { path: 'subcategroy', component: ProductSubcategoryComponent, data: { title: 'Sub Category', breadcrumb: 'Sub Category' } },
    { path: 'productweight', component: ProductProductWeightComponent, data: { title: 'Product Weight', breadcrumb: 'Product Weight' } },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRoutingModule { }