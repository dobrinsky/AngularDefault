import { NgModule } from '@angular/core';
import { CommonModule, APP_BASE_HREF } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { HttpModule, Http } from '@angular/http';
import { TranslateModule, TranslateLoader, TranslateService } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';

import { UsersComponent } from './users/users.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { NgxBootstrapComponent } from './ngx-bootstrap-demo/ngx-bootstrap.component';
import { Ng2BootstrapModule } from 'ngx-bootstrap';

import { InjectionToken, Inject } from '@angular/core';

import { ORIGIN_URL } from './constants/baseurl.constants';
import { getOriginUrl } from "./app.module.client";

import { ConnectionResolver } from './shared/route.resolver';
import { LinkService } from './shared/link.service';

import { UserService, THIRDPARTYLIBPROVIDERS } from './shared/user.service';
import { BrowserModule } from '@angular/platform-browser';

export function createTranslateLoader(http: Http, baseHref) {
    // Temporary Azure hack
    if (baseHref === null && typeof window !== 'undefined') {
        baseHref = window.location.origin;
    }
    
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
        UsersComponent,
        UserDetailComponent,
        HomeComponent,
        NgxBootstrapComponent
    ],
    imports: [
        BrowserModule,
        CommonModule,
        HttpModule,
        FormsModule,
        Ng2BootstrapModule.forRoot(),
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
        //            //new Inject(ORIGIN_URL), //remove this while using `InjectionToken`
        //            ORIGIN_URL //<-- this will be with `InjectionToken`
        //        ] //passed dependency name in `deps`
        //    }
        //}),
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            {
                path: 'users', component: UsersComponent,
                data: {
                    title: 'Users REST example',
                    meta: [{ name: 'description', content: 'This is User REST API example page Description!' }],
                    links: [
                        { rel: 'canonical', href: 'http://blogs.example.com/chat/something' },
                        { rel: 'alternate', hreflang: 'es', href: 'http://es.example.com/users' }
                    ]
                }
            },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ] ,
    providers: [
        LinkService,
        UserService,
        TranslateModule,
        ConnectionResolver,
        TranslateService
    ]
};

export class AppModule {
}