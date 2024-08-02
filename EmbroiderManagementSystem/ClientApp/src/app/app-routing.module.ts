import { Injectable, NgModule } from '@angular/core';
import { Routes, RouterModule, UrlSerializer, DefaultUrlSerializer, UrlTree } from '@angular/router';
import { AuthGuard } from './core/auth-guard.service';
import { AuthService } from './core/auth.service';
import { Utilities } from './core/utilities';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { LoginComponent } from './pages/login/login.component';

@Injectable()
export class LowerCaseUrlSerializer extends DefaultUrlSerializer {
  parse(url: string): UrlTree {
    const possibleSeparators = /[?;#]/;
    const indexOfSeparator = url.search(possibleSeparators);
    let processedUrl: string;

    if (indexOfSeparator > -1) {
      const separator = url.charAt(indexOfSeparator);
      const urlParts = Utilities.splitInTwo(url, separator);
      urlParts.firstPart = urlParts.firstPart.toLowerCase();

      processedUrl = urlParts.firstPart + separator + urlParts.secondPart;
    } else {
      processedUrl = url.toLowerCase();
    }

    return super.parse(processedUrl);
  }
}

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/dashboard' },
  {
    path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard], data: { title: 'Home', breadcrumb: 'Home' },
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: '/dashboard/welcome',
        canActivate: [AuthGuard]
      },
      {
        path: 'welcome',
        loadChildren: () =>
          import('./pages/welcome/welcome.module').then(m => m.WelcomeModule),
        canActivate: [AuthGuard],
        data: { title: 'Welcome', breadcrumb: 'Welcome' }
      },
      {
        path: 'setting',
        loadChildren: () =>
          import('./pages/setting/setting.module').then(m => m.SettingModule),
        canActivate: [AuthGuard],
        data: { title: 'Settings', breadcrumb: 'setting' }
      },
      {
        path: 'customer',
        loadChildren: () =>
          import('./pages/customer/customer.module').then(m => m.CustomerModule),
        canActivate: [AuthGuard],
        data: { title: 'Customer', breadcrumb: 'Customer' }
      },
      {
        path: 'product',
        loadChildren: () =>
          import('./pages/product/product.module').then(m => m.ProductModule),
        canActivate: [AuthGuard],
        data: { title: 'Product', breadcrumb: 'Product' }
      },
      {
        path: 'embroider',
        loadChildren: () =>
          import('./pages/embroider/embroider.module').then(m => m.EmbroiderModule),
        canActivate: [AuthGuard],
        data: { title: 'Embroider', breadcrumb: 'Embroider' }
      },
      {
        path: 'embroiderorder',
        loadChildren: () =>
          import('./pages/embroider-order/embroider-order.module').then(m => m.EmbroiderOrderModule),
        canActivate: [AuthGuard],
        data: { title: 'Embroider Order', breadcrumb: 'Embroider Order' }
      },
      {
        path: 'embroiderinvoice',
        loadChildren: () =>
          import('./pages/embroider-invoice/embroider-invoice.module').then(m => m.EmbroiderInvoiceModule),
        canActivate: [AuthGuard],
        data: { title: 'Embroider Invoice', breadcrumb: 'Embroider Invoice' }
      },

    ]
  },

  { path: 'login', component: LoginComponent, data: { title: 'Login' } },
  { path: '**', redirectTo: '/dashboard', data: { title: 'Page Not Found' } }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  providers: [
    AuthService,
    AuthGuard,
    { provide: UrlSerializer, useClass: LowerCaseUrlSerializer }],
  exports: [RouterModule]
})
export class AppRoutingModule { }
