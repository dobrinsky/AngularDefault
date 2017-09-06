import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { UserComponent } from "./components/user/user.component";
import { ErrorComponent } from "./components/error/error.component";

import { BrowserModule } from '@angular/platform-browser';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { HttpClientModule, HttpClient } from "@angular/common/http";

import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { ORIGIN_URL } from "./constansts/baseurl.constants";
import { LoginFormComponent } from "./account/login-form/login-form.component";
import { RegistrationFormComponent } from "./account/registration-form/registration-form.component";
import { UserService } from "./services/user.service";
import { ConfigService } from "./shared/utils/config.service";
import { LocalStorage } from "./shared/utils/local-storage";

export function HttpLoaderFactory(http: HttpClient, baseHref: any) {
    if (baseHref === null && typeof window !== 'undefined') {
        baseHref = window.location.origin;
    }
    return new TranslateHttpLoader(http, `${baseHref}/assets/i18n/`, '.json');
}

@NgModule({
    declarations: [
        AppComponent,
        LoginFormComponent,
        RegistrationFormComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        UserComponent,
        ErrorComponent,
        
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },

            { path: 'login', component: LoginFormComponent },
            { path: 'register', component: RegistrationFormComponent },

            { path: 'user', component: UserComponent },
            { path: 'error', component: ErrorComponent },

            { path: '**', redirectTo: 'home' }
        ]),
        BrowserModule,
        HttpClientModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: HttpLoaderFactory,
                deps: [HttpClient, [ORIGIN_URL]]
            }
        })
    ],
    providers: [
        UserService,
        {
            provide: LocalStorage,
            useValue: { getItem() { } }
        },
        ConfigService
    ]
})

export class AppModuleShared {
}
