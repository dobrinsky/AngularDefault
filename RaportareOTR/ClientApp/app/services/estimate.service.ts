import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { ServerBusyService } from "./serverBusy.service";

@Injectable()
export class EstimateService {

    constructor(
        public serverBusyService: ServerBusyService,
        private http: HttpClient) { }
    /*
    getErrors() {
        return this.http.get(this.serverBusyService.appName + '/api/error');
    }

    deleteError(id: number) {
        return this.http.delete(this.serverBusyService.appName + '/api/error/' + id);
    }

    deleteAllErrors() {
        return this.http.delete(this.serverBusyService.appName + '/api/errorAll');
    }
    */
}
