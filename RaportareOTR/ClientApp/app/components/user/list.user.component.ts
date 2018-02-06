import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { DatabaseUserService } from "../../services/Authorization/database.user.service";
import { ServerBusyService } from "../../services/serverBusy.service";
declare var $: any;
declare var swal: any;

@Component({
  selector: 'user',
  templateUrl: './list.user.component.html',
  providers: [
      DatabaseUserService,
  ]
})
export class ListUsersComponent implements OnInit {

    users: any;


    constructor(private userService: DatabaseUserService,
        public serverBusyService: ServerBusyService,
        private route: ActivatedRoute,
        private router: Router) {

    }


    ngOnInit() {
        $.getScript(this.serverBusyService.appName + '/plugins/sweetalert/jquery.sweet-alert.custom.js');
        $.getScript(this.serverBusyService.appName + '/plugins/sweetalert/sweetalert.min.js');

        this.populateUser();
    }

    populateUser() {
        this.userService.getUsers().subscribe(x => { this.users = x; });
        
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

                this.userService.deleteUser(id)
                    .subscribe(
                    () => {
                        this.populateUser();
                    });

                swal("Deleted!", "Your imaginary file has been deleted.", "success");
            } else {
                swal("Cancelled", "Your imaginary file is safe :)", "error");
            }
        });
    }
}
