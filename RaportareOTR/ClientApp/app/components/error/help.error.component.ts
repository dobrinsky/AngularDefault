import { Component, OnInit } from '@angular/core';

import { ActivatedRoute, Router } from "@angular/router";


@Component({
    selector: 'list-error-help',
    templateUrl: './help.error.component.html',
  
})
export class HelpErrorComponent implements OnInit {

    constructor( private router: Router) { }

    goBack() {
        window.history.back();
    }
    ngOnInit() {
  
    }

    
}
