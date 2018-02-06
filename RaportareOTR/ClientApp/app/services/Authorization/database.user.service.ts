import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { ServerBusyService } from "../serverBusy.service";

@Injectable()
export class DatabaseUserService {

    constructor(
        public serverBusyService: ServerBusyService,
        private http: HttpClient) { }

    getUsers() {
        return this.http.get(this.serverBusyService.appName + '/api/users');
    }

    deleteUser(id: number) {
        return this.http.delete(this.serverBusyService.appName + '/api/DeleteUser/' + id);
    }

}
