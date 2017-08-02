import 'reflect-metadata';
import 'zone.js';
import 'rxjs/add/operator/first';
import { enableProdMode, ApplicationRef, NgZone, ValueProvider } from '@angular/core';
import { platformDynamicServer, PlatformState, INITIAL_CONFIG } from '@angular/platform-server';
import { createServerRenderer, RenderResult, BootFuncParams } from 'aspnet-prerendering';
import { AppModule } from './app/app.module.server';

import { ORIGIN_URL } from './app/constants/baseurl.constants';
import { REQUEST } from './app/shared/request';

import { ngAspnetCoreEngine, IEngineOptions, createTransferScript, IRequestParams } from './temporary-aspnetcore-engine';

enableProdMode();

//export default createServerRenderer((params: BootFuncParams) => {

//    const providers = [
//        { provide: INITIAL_CONFIG, useValue: { document: '<app></app>', url: params.url } },
//        { provide: ORIGIN_URL, useValue: params.origin },
//        { provide: REQUEST, useValue: params.data.request }
//    ];

//    return platformDynamicServer(providers).bootstrapModule(AppModule).then(moduleRef => {
//        const appRef = moduleRef.injector.get(ApplicationRef);
//        const state = moduleRef.injector.get(PlatformState);
//        const zone = moduleRef.injector.get(NgZone);

//        return new Promise<RenderResult>((resolve, reject) => {
//            zone.onError.subscribe(errorInfo => reject(errorInfo));
//            appRef.isStable.first(isStable => isStable).subscribe(() => {
//                // Because 'onStable' fires before 'onError', we have to delay slightly before
//                // completing the request in case there's an error to report
//                setImmediate(() => {
//                    resolve({
//                        html: state.renderToString()
//                    });
//                    moduleRef.destroy();
//                });
//            });
//        });
//    });
//});

export default createServerRenderer((params: BootFuncParams) => {

    // Platform-server provider configuration
    const setupOptions: IEngineOptions = {
        appSelector: '<app></app>',
        ngModule: AppModule,
        request: params,
        providers: [
            // Optional - Any other Server providers you want to pass (remember you'll have to provide them for the Browser as well)
        ]
    };

    return ngAspnetCoreEngine(setupOptions).then(response => {
        // Apply your transferData to response.globals
        response.globals.transferData = createTransferScript({
            someData: 'Transfer this to the client on the window.TRANSFER_CACHE {} object',
            //fromDotnet: params.data.thisCameFromDotNET // example of data coming from dotnet, in HomeController
        });

        return ({
            html: response.html,
            globals: response.globals
        });
    });
});