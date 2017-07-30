import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { HttpModule, Http } from '@angular/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';

import { InjectionToken, Inject } from '@angular/core';

import { ORIGIN_URL } from './constants/baseurl.constants';
import { getOriginUrl } from "./app.module.client";

import { UserService, THIRDPARTYLIBPROVIDERS } from './shared/user.service';

export function createTranslateLoader(http: Http, baseHref) {
    // Temporary Azure hack
    if (baseHref === null && typeof window !== 'undefined') {
        baseHref = window.location.origin;
    }
    console.log("Hugabugagau: " + THIRDPARTYLIBPROVIDERS)
    // i18n files are in `wwwroot/assets/`
    return new TranslateHttpLoader(http, `${baseHref}/assets/i18n/`, '.json');
}

export const sharedConfig: NgModule = {
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent
    ],
    imports: [
        HttpModule,
        // i18n support
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: (createTranslateLoader),
                deps: [Http, [ORIGIN_URL]]
            }
        }),
        //THIRDPARTYLIBPROVIDERS, //<-- registered provider here
        //TranslateModule.forRoot({
        //    loader: {
        //        provide: TranslateLoader,
        //        useFactory: (createTranslateLoader),
        //        deps: [
        //            Http,
        //            new Inject(ORIGIN_URL), //remove this while using `InjectionToken`
        //            //ORGIN_URL //<-- this will be with `InjectionToken`
        //        ] //passed dependency name in `deps`
        //    }
        //}),
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ] ,
    providers: [
        UserService,
        TranslateModule,

    ]
};
