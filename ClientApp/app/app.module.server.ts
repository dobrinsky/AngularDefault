import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { BrowserModule } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { sharedConfig } from './app.module';
import { ServerTransferStateModule } from './modules/transfer-state/server-transfer-state.module';
import { TransferState } from "./modules/transfer-state/transfer-state";

@NgModule({
    bootstrap: sharedConfig.bootstrap,
    declarations: sharedConfig.declarations,
    imports: [
        ServerModule,
        ...sharedConfig.imports
    ]
})

//@NgModule({
//    bootstrap: sharedConfig.bootstrap,
//    imports: [
//        BrowserModule.withServerTransition({
//            appId: 'my-app-id' // make sure this matches with your Browser NgModule
//        }),
//        ServerModule,
//        NoopAnimationsModule,

//        ServerTransferStateModule,

//        // Our Common AppModule
//        AppModule
//    ]
//})

export class AppModule {
    constructor(private transferState: TransferState) { }

    // Gotcha (needs to be an arrow function)
    ngOnBootstrap = () => {
        this.transferState.inject();
    }
}
