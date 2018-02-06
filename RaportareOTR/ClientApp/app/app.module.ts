import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';

import { HomeComponent } from './components/home/home.component';
import { ListErrorsComponent } from "./components/error/error.component";

import { BrowserModule } from '@angular/platform-browser';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { HttpClientModule, HttpClient } from "@angular/common/http";

import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { ORIGIN_URL } from "./constansts/baseurl.constants";
import { LoginFormComponent } from "./account/login-form/login-form.component";
import { RegistrationFormComponent } from "./account/registration-form/registration-form.component";
import { ConfigService } from "./shared/utils/config.service";
import { CoolLocalStorage } from "angular2-cool-storage";
import { SecureFilesComponent } from "./components/securefile/securefiles.component";
import { SecureFileService } from "./components/securefile/SecureFileService";
import { AddUserComponent } from "./components/user/add.user.component";
import { ViewUserComponent } from "./components/user/view.user.component";
import { ServerBusyService } from "./services/serverBusy.service";
import { MessageService } from "./services/Authorization/message.service";
import { DatabaseUserService } from "./services/Authorization/database.user.service";
import { UserService } from "./services/Authorization/user.service";
import { CommonService } from "./services/common.service";
import { HelpUserComponent } from "./components/user/help.user.component";
import { HelpAddUserComponent } from "./components/user/help.new.user.component";
import { ListUsersComponent } from "./components/user/list.user.component";
import { HelpErrorComponent } from "./components/error/help.error.component";
import { EstimateService } from "./services/estimate.service";
import { NavbarComponent } from "./components/navbar/navbar.component";
import { SidebarComponent } from "./sidebar/sidebar.component";

export function HttpLoaderFactory(http: HttpClient, baseHref: any) {
    if (baseHref === null && typeof window !== 'undefined') {
        baseHref = window.location.origin;
    }
    return new TranslateHttpLoader(http, `${baseHref}/assets/i18n/`, '.json');
}

@NgModule({
    declarations: [
        AppComponent,
        LoginFormComponent,
        RegistrationFormComponent,
        NavbarComponent,
        HomeComponent,
        AddUserComponent,
        ViewUserComponent,
        ListUsersComponent,
        SecureFilesComponent,
        HelpUserComponent,
        AddUserComponent,
        HelpAddUserComponent,
        ViewUserComponent,
        ListErrorsComponent,
        HelpErrorComponent,
        SidebarComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },

            { path: 'login', component: LoginFormComponent },
            { path: 'register', component: RegistrationFormComponent }, 

            { path: 'add-user', component: AddUserComponent }, 
            { path: 'view-user', component: ViewUserComponent },
            { path: 'user', component: ListUsersComponent },
            { path: 'error', component: ListErrorsComponent },
            
            { path: 'secure', component: SecureFilesComponent }, 
            
            { path: '**', redirectTo: 'home' }
        ]),
        BrowserModule,
        HttpClientModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: HttpLoaderFactory,
                deps: [HttpClient, [ORIGIN_URL]]
            }
        })
    ],
    providers: [
        ConfigService,
        CoolLocalStorage,
        SecureFileService,
        UserService,
        ServerBusyService,
        MessageService,
        DatabaseUserService,
        CommonService,
        EstimateService
    ]
})

export class AppModuleShared {
}
