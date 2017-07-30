import { Component } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {

    title: string = 'Angular 4.0 Universal & ASP.NET Core advanced starter-kit';

    // Use "constructor"s only for dependency injection
    constructor(public translate: TranslateService) { }

    public setLanguage(lang) {
        this.translate.use(lang);
    }
}
