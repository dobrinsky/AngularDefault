import { Component, OnInit} from '@angular/core';
import * as $ from 'jquery';
import { CommonService } from "../../services/common.service";
//declare var $: any;

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',

})

export class HomeComponent implements OnInit {

    contact: any = {}

    constructor(private commonService: CommonService) {
        

    }

    ngOnInit() {
    }

    submit() {
        //this.commonService.sendEmail(this.contact)
        //    .subscribe(
        //    () => {
        //    });
    }
}
