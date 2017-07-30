import { Component, Inject } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Http } from '@angular/http';
import { ORIGIN_URL } from '../../constants/baseurl.constants';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    user = {
        name: 'Arthur',
        age: 42
    };

    constructor(private translate: TranslateService) {
        translate.setDefaultLang('ro');
    }

    switchLanguage(language: string) {
        this.translate.use(language);
    }
}
