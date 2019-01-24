import { Injectable } from '@angular/core';

import * as _ from 'underscore';

@Injectable()
export class ModalService {
    private modals: any[] = [];

    add(modal: any): void {
        // Add modal to array of active modals.
        this.modals.push(modal);
    }

    close(id: string): void {
        // Close modal specified by id.
        const modal = _.find(this.modals, { id: id });
        modal.close();
    }

    open(id: string): void {
        // Open modal specified by id.
        const modal = _.findWhere(this.modals, { id: id });
        modal.open();
    }

    remove(id: string): void {
        // Remove modal from array of active modals.
        const modalToRemove = _.findWhere(this.modals, { id: id });
        this.modals = _.without(this.modals, modalToRemove);
    }
}
