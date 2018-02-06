import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { SecureFileService } from './SecureFileService';
import { Observable } from 'rxjs/Observable';
//import { saveAs } from "file-saver";

@Component({
    selector: 'securefiles',
    templateUrl: 'securefiles.component.html',
    providers: [SecureFileService]
})

export class SecureFilesComponent implements OnInit {

    fileName=['test1','test2'];

   
    constructor(private secureFileService: SecureFileService) {

    }
    ngOnInit() {

    }

    download() {
        this.secureFileService.printBill(5).subscribe((res:any) => {
            //saveAs(res, "InvoiceNo" + 5 + ".pdf");
            let fileURL = URL.createObjectURL(res);
            window.open(fileURL);
        })
    }


}
