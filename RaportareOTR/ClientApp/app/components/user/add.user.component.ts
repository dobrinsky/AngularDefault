import { Component, OnInit } from '@angular/core';
import { UserService } from "../../services/Authorization/user.service";
import { ActivatedRoute, Router } from "@angular/router";
import { ServerBusyService } from "../../services/serverBusy.service";
declare var $: any;
declare var swal: any;

@Component({
    selector: 'add-user',
    templateUrl: './add.user.component.html'
})

export class AddUserComponent implements OnInit  {

    user: any = {};

    userclasses: any;

    public filteredList: any = [];
    public elementRef: any;
    public query: any = {};

    usernameErrors: string[];
    userclassErrors: string[];
    passwordErrors: string[];
    passwordConfirmationErrors: string[];
    emailErrors: string[];

    constructor(public userService: UserService,
        public serverBusyService: ServerBusyService,
        private route: ActivatedRoute,
        private router: Router) {

        this.initialize();

    }
    
    ngOnInit() {
        $.getScript(this.serverBusyService.appName + '/plugins/sweetalert/jquery.sweet-alert.custom.js');
        $.getScript(this.serverBusyService.appName + '/plugins/sweetalert/sweetalert.min.js');
        
        this.userService.getUserClasses().subscribe((x: any) => {
            this.userclasses = x.items;
            console.log(x);
        });
        
    }
    
    select(item: any) {
        this.query.denumire = item;
        
        this.filteredList = [];
    }

    goBack() {
        window.history.back();
    }
    
    submit() {
        
        this.initialize();
        
        this.userService.create(this.user)
            .subscribe(x => { }
            ,
            (err: any) => {
                if (err.status === 400) {
                    let validationErrorDictionary = err.error;
                    console.log(validationErrorDictionary);
                    
                    for (var fieldName in validationErrorDictionary) {
                        if (validationErrorDictionary.hasOwnProperty(fieldName)) {
                            if (fieldName == "username")
                                this.usernameErrors.push(validationErrorDictionary[fieldName]);
                            else if (fieldName == "userclass")
                                this.userclassErrors.push(validationErrorDictionary[fieldName]);
                            else if (fieldName == "password")
                                this.passwordErrors.push(validationErrorDictionary[fieldName]);
                            else if (fieldName == "passwordConfirmation")
                                this.passwordConfirmationErrors.push(validationErrorDictionary[fieldName]);
                            else if (fieldName == "email")
                                this.emailErrors.push(validationErrorDictionary[fieldName]);
                        }
                    }
                }
                else
                    swal("Error", "Something goes wrong", "error");
            },
            () => {
                swal("Succes!", "Tipul de ambalare a fost sters.", "success");
            }
        );
    }

    initialize() {
        this.usernameErrors = [];
        this.userclassErrors = [];
        this.passwordErrors = [];
        this.passwordConfirmationErrors = [];
        this.emailErrors = [];
    }
}
