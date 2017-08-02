import { Component, OnInit, OnDestroy, Inject, ViewEncapsulation, RendererFactory2, PLATFORM_ID } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute, PRIMARY_OUTLET } from '@angular/router';
import { Meta, Title, DOCUMENT, MetaDefinition } from '@angular/platform-browser';
import { Subscription } from 'rxjs/Subscription';
import { isPlatformServer } from '@angular/common';

import { TranslateService } from '@ngx-translate/core';
import { REQUEST } from '../../shared/request';

import { Http } from '@angular/http';
import { ORIGIN_URL } from '../../constants/baseurl.constants';
import { getOriginUrl, getRequest } from "../../app.module.client";

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],

})
export class AppComponent implements OnInit, OnDestroy {
    user = {
        name: 'Arthur',
        age: 42
    };

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private title: Title,
        private meta: Meta,
        public translate: TranslateService,
        @Inject(REQUEST) private request
    ) {
        // this language will be used as a fallback when a translation isn't found in the current language
        translate.setDefaultLang('en');

        // the lang to use, if the lang isn't available, it will use the current loader to get them
        translate.use('en');

        console.log(`What's our REQUEST Object look like?`);
        console.log(`The Request object only really exists on the Server, but on the Browser we can at least see Cookies`);
        console.log(this.request);
    }

    ngOnInit() {
        // Change "Title" on every navigationEnd event
        // Titles come from the data.title property on all Routes (see app.routes.ts)
    }

    ngOnDestroy() {
        // Subscription clean-up
    }

    //constructor(private translate: TranslateService) {
    //    translate.setDefaultLang('ro');
    //}

    switchLanguage(language: string) {
        debugger;
        this.translate.use(language);
    }
}
