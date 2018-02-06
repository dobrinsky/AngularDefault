import { Injectable, Inject } from '@angular/core';
import { Http, Response, Headers, RequestOptions, ResponseContentType } from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';


@Injectable()
export class SecureFileService {

    constructor(private http: Http) {

    }

    printBill(billID: any) {
        return this.http.get('/api/getFile/' + billID,
            { responseType: ResponseContentType.Blob })
            .map((res) => {
                return new Blob([res.blob()], { type: 'application/pdf' })
            })
    }


}