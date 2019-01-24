import { Component, OnInit } from '@angular/core';

import { ToastrService } from 'ngx-toastr';

import { AlertService } from './alert.service';

@Component({
    selector: 'app-alert',
    styleUrls: ['./alert.component.scss'],
    templateUrl: './alert.component.html'
})
export class AlertComponent implements OnInit {
    constructor(
        private toastrService: ToastrService,
        private readonly alertService: AlertService) {
    }

    ngOnInit(): void {
        this.alertService.getMessage().subscribe(message => {
            if (message && message.text) {
                this.showMessage(message);
            }
        });
    }

    showMessage(message: any) {
        switch(message.type) {
            case "success":
                this.toastrService.success(message.text);
                break;
            case "error":
                this.toastrService.error(message.text);
                break;
            case "warning":
                this.toastrService.warning(message.text);
                break;
            case "info":
                this.toastrService.info(message.text);
                break;
            default:
                this.toastrService.error(message.text);
                break;
        }
    }
}
