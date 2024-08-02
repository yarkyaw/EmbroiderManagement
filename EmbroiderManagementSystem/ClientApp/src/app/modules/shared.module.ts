import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { en_US, NZ_I18N } from 'ng-zorro-antd/i18n';


@NgModule({
    declarations: [],
    imports: [
        CommonModule,
        HttpClientModule,
        ],
    providers: [{ provide: NZ_I18N, useValue: en_US }]
})
export class SharedModule { }
