import { Component, OnInit } from '@angular/core';
import { ErrorService } from "../../services/error.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'error',
  templateUrl: './error.component.html',
  providers: [
      ErrorService,
  ]
})
export class ErrorComponent implements OnInit {

    errors: any;


    constructor(private errorService: ErrorService, private route: ActivatedRoute,
        private router: Router) {

    }


    ngOnInit() {
        this.populateError();
    }

    populateError() {
        this.errorService.getErrors().subscribe(x => {
            this.errors = x
            for (let e of this.errors) {
                e.line = e.line.slice(2);
                e.file = e.file.slice(2);
                e.method = e.method.slice(2);
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
}
