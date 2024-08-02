import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IconsProviderModule } from './icons-provider.module';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { en_US } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { LoginComponent } from './pages/login/login.component';
import { AntdModule } from './modules/antd.module';
import { SharedModule } from './modules/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OAuthModule, OAuthService, UrlHelperService } from 'angular-oauth2-oidc';
import { AccountEndpoint } from './core/account-endpoint.service';
import { AccountService } from './core/account.service';
import { AppTitleService } from './core/app-title.service';
import { ConfigurationService } from './core/configuration.service';
import { LocalStoreManager } from './core/local-store-manager.service';
import { OidcHelperService } from './core/oidc-helper.service';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AutofocusDirective } from './core/autofocus.directive';

registerLocaleData(en);

@NgModule({
  declarations: [
    AppComponent, LoginComponent, DashboardComponent,AutofocusDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    IconsProviderModule,
    HttpClientModule,
    BrowserAnimationsModule,
    SharedModule,
    AntdModule,
    FormsModule,
    ReactiveFormsModule,
    OAuthModule.forRoot(),
  ],
  providers: [
    { provide: NZ_I18N, useValue: en_US },
    ConfigurationService,
    AppTitleService,
    AccountService,
    AccountEndpoint,
    LocalStoreManager,
    OidcHelperService,
    OAuthService,
    UrlHelperService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
