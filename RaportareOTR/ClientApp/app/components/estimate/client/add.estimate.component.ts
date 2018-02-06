import { Component, OnInit } from '@angular/core';
import { EstimateService } from "../../../services/estimate.service";
import { ActivatedRoute, Router } from "@angular/router";
import { ServerBusyService } from "../../../services/serverBusy.service";
declare var $: any;

@Component({
  selector: 'add-estimate',
  templateUrl: './add.estimate.component.html'
})
export class AddEstimateComponent implements OnInit {
    
    constructor(private estimateService: EstimateService,
        public serverBusyService: ServerBusyService,
        private route: ActivatedRoute,
        private router: Router) {

    }

    ngOnInit() {
        $.getScript(this.serverBusyService.appName + '/plugins/sweetalert/jquery.sweet-alert.custom.js');
        $.getScript(this.serverBusyService.appName + '/plugins/sweetalert/sweetalert.min.js');
        
        //this.populateError();
    }
    /*
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
    */
}
