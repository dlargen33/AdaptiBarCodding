import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';

import { AlertComponent } from './alert.component';
import { AlertService } from './alert.service';

@NgModule({
    declarations: [
        AlertComponent
    ],
    exports: [
        AlertComponent
    ],
    imports: [
        BrowserAnimationsModule,
        CommonModule,
        ToastrModule.forRoot({
            positionClass: 'toast-top-center',
            timeOut: 7000,
            maxOpened: 3,
            autoDismiss: true,
            preventDuplicates: true
            // disableTimeOut: true,
            // closeButton: true,
            // tapToDismiss: false
        }),
    ],
    providers: [
        AlertService
    ]
})
export class AlertModule {
}
