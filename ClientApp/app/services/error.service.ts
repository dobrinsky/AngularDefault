import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class ErrorService {

    constructor(private http: Http) { }

    getErrors() {
        return this.http.get('/api/error').map(res => res.json());
    }

    deleteError(id) {
        return this.http.delete('/api/error/' + id).map(res => res.json());
    }
}
