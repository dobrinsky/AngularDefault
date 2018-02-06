import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { UserService } from "../../services/Authorization/user.service";
import { ServerBusyService } from "../../services/serverBusy.service";
declare var $: any;
declare var swal: any;

@Component({
    selector: 'view-user',
    templateUrl: './view.user.component.html',
  
})
export class ViewUserComponent implements OnInit {

    username: string;
    user: any = {};
    puncteDeLucru: any;
    
    constructor(public userService: UserService,
        public serverBusyService: ServerBusyService,
        private route: ActivatedRoute,
        private router: Router) {

        route.params.subscribe(p => {
            if (p.username != null)
                this.username = p['username'];
        });

        this.user.user = {};
    }

    ngOnInit() {
        $.getScript(this.serverBusyService.appName + '/plugins/sweetalert/jquery.sweet-alert.custom.js');
        $.getScript(this.serverBusyService.appName + '/plugins/sweetalert/sweetalert.min.js');
        
        this.userService.getUser(this.username).subscribe(x => {
            this.user = x;
        });
        
        this.populatePuncteDeLucru();

    }

    populatePuncteDeLucru() {
        this.userService.getPuncteDeLucru(this.username).subscribe(x => {
            this.puncteDeLucru = x;
        });
    }
    
    delete(id: number) {

        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this imaginary file!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel plx!",
            closeOnConfirm: false,
            closeOnCancel: false
        }, (isConfirm: boolean) => {
            if (isConfirm) {

                this.userService.deletePuncteDeLucru(id)
                    .subscribe(
                    () => {
                        this.populatePuncteDeLucru();
                    });

                swal("Deleted!", "Your imaginary file has been deleted.", "success");
            } else {
                swal("Cancelled", "Your imaginary file is safe :)", "error");
            }
        });
    }

    alege(id: number) {
        this.userService.choosePuncteDeLucru(this.username, id)
            .subscribe(() => {

                swal("Succes!", "Tipul de ambalare a fost sters.", "success");
            },
            err => {
                swal("Error", "Something goes wrong", "error");
            });
    }
    
}
