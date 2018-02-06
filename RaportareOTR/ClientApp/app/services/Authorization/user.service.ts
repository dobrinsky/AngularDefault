import { Injectable, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';

import { UserRegistration } from '../../shared/models/user.registration.interface';
import { ConfigService } from '../../shared/utils/config.service';

import { BaseService } from "../base.service";

import { Observable } from 'rxjs/Rx';
import { BehaviorSubject } from 'rxjs/Rx';

import { CoolLocalStorage } from 'angular2-cool-storage';

import { Router, ActivatedRoute } from '@angular/router';
import { PlatformLocation } from '@angular/common';

declare var $: any;

//import * as _ from 'lodash';

// Add the RxJS Observable operators we need in this app.
import '../../rxjs-operators';
import { ORIGIN_URL } from "../../constansts/baseurl.constants";
import { ServerBusyService } from "../serverBusy.service";

@Injectable()
export class UserService extends BaseService implements OnInit {

    baseUrl: string = '';

    // Observable navItem source
    private _authNavStatusSource = new BehaviorSubject<boolean>(false);
    // Observable navItem stream
    authNavStatus$ = this._authNavStatusSource.asObservable();

    localStorage: CoolLocalStorage;

    username: string = "";
    email: string = "";
    role: string = "";

    public appName: string = "";

    public loggedIn = false;

    constructor(private http: HttpClient,
        private configService: ConfigService,
        private router: Router,
        public serverBusyService: ServerBusyService,
        localStorage: CoolLocalStorage) {
        super();
        this.loggedIn = !!localStorage.getItem('auth_token');
      
        // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
        // header component resulting in authed user nav links disappearing despite the fact user is still logged in
        this._authNavStatusSource.next(this.loggedIn);
        this.baseUrl = `${ ORIGIN_URL }/api`;
        this.localStorage = localStorage;
        
    }

    register(email: string, password: string, firstName: string, lastName: string, location: string): Observable<UserRegistration> {

        let body = JSON.stringify({ email, password, firstName, lastName,location });
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

        return this.http.post(this.baseUrl + "/accounts", body, { headers: headers })
            .map(res => true)
            .catch(this.handleError);
    }  

    ngOnInit() {

        if (this.loggedIn) {
            
            this.getLogeddInUserInfo();
        }

    }

    login(userName: any, password: any) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
   
    let user: any = {};

    user.userName = userName;
    user.password = password;

    return this.http.post(this.serverBusyService.appName + '/api/login', user).map((res: any) => {
        
        localStorage.setItem('auth_token', res.auth_token);
        
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
        this.email = res.email;
        this.username = res.username;
        this.role = res.role;
        
        return true;
    }).catch(this.handleError);
    }

    logout() {
    
        localStorage.removeItem('auth_token');

        let user: any = {};

        user.userName = "aaa";
        user.password = "aaa";

        return this.http.post(this.serverBusyService.appName + '/logout', user).map((res: any) => {
            
            this._authNavStatusSource.next(false);

            this.router.navigate([this.serverBusyService.appName + '/home']);
        
            this.loggedIn = false;

        });
    }

    isLoggedIn() {
    return this.loggedIn;
    }  

    getUser(username: string) {
        return this.http.get(this.serverBusyService.appName + '/api/user/' + username);
    }

    getUsers() {
        return this.http.get(this.serverBusyService.appName + '/api/users');
    }

    deleteUser(id: number) {
        return this.http.delete(this.serverBusyService.appName + '/api/user/' + id);
    }

    create(user: any) {
        return this.http.post(this.serverBusyService.appName + '/api/register', user);
    }

    getUserClasses() {
        return this.http.get(this.serverBusyService.appName + '/api/GetUserClasses');
    }

    getPuncteDeLucru(username: string) {
        return this.http.get(this.serverBusyService.appName + '/api/GetUserPuncteDeLucru/' + username);
    }

    createPunctDeLucruUser(user: any) {
        return this.http.post(this.serverBusyService.appName + '/api/AddPuncteDeLucruUser', user);
    }

    deletePuncteDeLucru(punctDeLucruId: number) {
        return this.http.delete(this.serverBusyService.appName + '/api/DeletePuncteDeLucruUser/' + punctDeLucruId);
    }

    choosePuncteDeLucru(username: string, punctDeLucruId: number) {
        return this.http.post(this.serverBusyService.appName + '/api/ChoosePuncteDeLucruUser/' + username + '/' + punctDeLucruId, username);
    }

    getLogeddInUserInfo() {
        return this.http.get(this.serverBusyService.appName + '/api/GetLogeddInUserInfo').map((res: any) => {
            
            this.email = res.email;
            this.username = res.username;
            this.role = res.role;
        },
        (err: any) => {
            
            if (err.status === 400)
                this.logout();
        });
    }
}

