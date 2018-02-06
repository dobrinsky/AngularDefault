import { Component, OnInit } from '@angular/core';
import { ErrorService } from "../../services/error.service";
import { ActivatedRoute, Router } from "@angular/router";
import { ServerBusyService } from "../../services/serverBusy.service";
declare var $: any;

@Component({
  selector: 'list-error',
  templateUrl: './error.component.html',
  providers: [
      ErrorService,
  ]
})
export class ListErrorsComponent implements OnInit {

    errors: any;
    
    constructor(private errorService: ErrorService,
        public serverBusyService: ServerBusyService,
        private route: ActivatedRoute,
        private router: Router) {

    }

    ngOnInit() {
        $.getScript(this.serverBusyService.appName + '/plugins/sweetalert/jquery.sweet-alert.custom.js');
        $.getScript(this.serverBusyService.appName + '/plugins/sweetalert/sweetalert.min.js');
        
        this.populateError();
    }

    populateError() {
        this.errorService.getErrors().subscribe(x => {
            this.errors = x
            for (let e of this.errors) {
                e.line = e.line;
                e.file = e.file;
                e.method = e.method;
            }

        });
    }

    delete(id: number) {
        this.errorService.deleteError(id)
            .subscribe(
            () => {
                this.populateError();
            });
    }

    deleteAll() {
        this.errorService.deleteAllErrors()
            .subscribe(
            () => {
                this.populateError();
            });
    }
}
