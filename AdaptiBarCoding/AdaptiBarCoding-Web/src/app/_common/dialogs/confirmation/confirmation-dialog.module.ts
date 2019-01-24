import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ConfirmationDialogComponent } from './confirmation-dialog.component';

@NgModule({
    declarations: [
        ConfirmationDialogComponent
    ],
    entryComponents: [
        ConfirmationDialogComponent
    ],
    exports: [
        ConfirmationDialogComponent
    ],
    imports: [
        CommonModule,
        FormsModule
    ],
})
export class ConfirmationDialogModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: ConfirmationDialogModule
        };
    }
}
