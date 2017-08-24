import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class ErrorService {

    constructor(private http: Http) { }

    getErrors() {
        return this.http.get('/api/test').map(res => res.json());
    }

    deleteError(id: number) {
        return this.http.delete('/api/test/' + id).map(res => res.json());
    }
}
