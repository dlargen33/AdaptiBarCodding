import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'confirmation-dialog',
    styleUrls: ['./confirmation-dialog.component.scss'],
    templateUrl: './confirmation-dialog.component.html'
})
export class ConfirmationDialogComponent {
    @Input('message') message = 'Confirm delete?';

    @Output('confirm') confirm = new EventEmitter();
    @Output('cancel') cancel = new EventEmitter();

    isShowing = false;

    sendCancel(): void {
        this.cancel.emit();
        this.isShowing = false;
    }

    sendConfirm(): void {
        this.confirm.emit();
        this.isShowing = false;
    }
}
