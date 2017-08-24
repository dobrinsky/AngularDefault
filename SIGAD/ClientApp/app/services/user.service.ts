import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import 'rxjs/add/operator/map';

@Injectable()
export class UserService {

    constructor(private http: Http) { }

    getUser() {
        return this.http.get('/api/user').map(res => res.json());
    }

    deleteUser(id: number) {
        return this.http.delete('/api/user/' + id).map(res => res.json());
    }

}
