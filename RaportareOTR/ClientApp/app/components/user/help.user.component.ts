import { Component } from '@angular/core';

@Component({
    selector: 'helpuser',
    templateUrl: './help.user.component.html'
})
export class HelpUserComponent {
    goBack() {
        window.history.back();
    }
}
