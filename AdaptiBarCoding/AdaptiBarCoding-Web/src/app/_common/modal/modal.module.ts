import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ModalComponent } from './modal.component';
import { ModalService } from './modal.service';

@NgModule({
    declarations: [
        ModalComponent
    ],
    exports: [
        ModalComponent
    ],
    imports: [
        CommonModule,
        FormsModule
    ],
    providers: [
        ModalService
    ]
})
export class ModalModule {
}
