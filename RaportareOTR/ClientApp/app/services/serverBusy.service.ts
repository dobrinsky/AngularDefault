import { Injectable, isDevMode } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';

import { PlatformLocation } from '@angular/common';
import { ActivatedRoute, Router } from "@angular/router";

@Injectable()
export class ServerBusyService {

    showServerBusyIcon: boolean = false;

    public appName: string = "";

    a: string = "";

    constructor(private http: HttpClient,
        public platformLocation: PlatformLocation,
        private router: Router) {

        console.log((platformLocation as any));
        //let fullPath = (platformLocation as any).location.pathname as string;
        //let serverRoute = fullPath.substring(0, fullPath.indexOf(router.url))
        
        //let appNameData = serverRoute.split('/');
        
        //if (appNameData.length > 2) this.appName = "/" + appNameData[1];
        //else {
        //    let fullPathData = fullPath.split('/');

        //    if (fullPathData.length === 2)
        //        this.appName = fullPath;
        //    else
        //        this.appName = "/" + fullPathData[1];
        //}
        
    }

    toggleServerBusyIcon() {
        this.showServerBusyIcon = !this.showServerBusyIcon;
    }

    setToTrue() {
        this.showServerBusyIcon = true;
    }

    setToFalse() {
        this.showServerBusyIcon = false;
    }
}
