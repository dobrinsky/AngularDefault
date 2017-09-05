import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppModuleShared } from './app.module';
import { AppComponent } from './components/app/app.component';
import { ORIGIN_URL } from "./constansts/baseurl.constants";

export function getOriginUrl() {
    return window.location.origin;
}

@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        BrowserModule,
        AppModuleShared
    ],
    providers: [
        {
            // We need this for our Http calls since they'll be using an ORIGIN_URL provided in main.server
            // (Also remember the Server requires Absolute URLs)
            provide: ORIGIN_URL,
            useFactory: (getOriginUrl)
        },
        {
            provide:
            'BASE_URL', useFactory: getBaseUrl
        }
    ]
})
export class AppModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
