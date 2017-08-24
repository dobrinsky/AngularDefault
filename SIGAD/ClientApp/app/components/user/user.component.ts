import { Component, OnInit } from '@angular/core';
import { UserService } from "../../services/user.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  providers: [
      UserService,
  ]
})
export class UserComponent implements OnInit {

    users: any;


    constructor(private userService: UserService, private route: ActivatedRoute,
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
