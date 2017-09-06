import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { DatabaseUserService } from "../../services/database.user.service";

@Component({
  selector: 'user',
  templateUrl: './user.component.html',
  providers: [
      DatabaseUserService,
  ]
})
export class UserComponent implements OnInit {

    users: any;


    constructor(private userService: DatabaseUserService, private route: ActivatedRoute,
        private router: Router) {

    }


    ngOnInit() {
        this.populateUser();
    }

    populateUser() {
        this.userService.getUser().subscribe(x => this.users = x);
    }

    delete(id: number) {
        this.userService.deleteUser(id)
            .subscribe(
            () => {
                this.populateUser();
            });
    }
}
