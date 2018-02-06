import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'; 
import 'rxjs/add/operator/map';

@Injectable()
export class CommonService {

    constructor(private http: HttpClient) { }

    sendEmail(emailRes: any) {
        return this.http.post('/api/sendEmail', emailRes);
    }
    
}
