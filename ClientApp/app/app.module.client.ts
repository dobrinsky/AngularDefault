import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { sharedConfig } from './app.module';


import { Injectable, Inject } from '@angular/core';
import { ORIGIN_URL } from './constants/baseurl.constants';

export function getOriginUrl() {
    return window.location.origin;
}


@NgModule({
    bootstrap: sharedConfig.bootstrap,
    declarations: sharedConfig.declarations,
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        ...sharedConfig.imports
    ],
    providers: [
        { provide: ORIGIN_URL, useFactory: (getOriginUrl) }
    ]
})
export class AppModule {
}
