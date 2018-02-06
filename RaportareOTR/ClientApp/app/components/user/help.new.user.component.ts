import { Component } from '@angular/core';

@Component({
    selector: 'helnewpuser',
    templateUrl: './help.new.user.component.html'
})
export class HelpAddUserComponent {
    goBack() {
        window.history.back();
    }
}
