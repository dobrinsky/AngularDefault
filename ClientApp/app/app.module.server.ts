import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { sharedConfig } from './app.module';
import { TransferState } from "./modules/transfer-state/transfer-state";

@NgModule({
    bootstrap: sharedConfig.bootstrap,
    declarations: sharedConfig.declarations,
    imports: [
        ServerModule,
        ...sharedConfig.imports
    ]
})
export class AppModule {
  //  constructor(private transferState: TransferState) { }

  //// Gotcha (needs to be an arrow function)
  //ngOnBootstrap = () => {
  //  this.transferState.inject();
  //}
}
