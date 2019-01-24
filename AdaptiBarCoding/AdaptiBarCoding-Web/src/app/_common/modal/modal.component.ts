import { Component, ElementRef, Input, OnInit, OnDestroy } from '@angular/core';

import * as $ from 'jquery';

import { ModalService } from './modal.service';

@Component({
    selector: 'modal',
    templateUrl: './modal.component.html'
})
export class ModalComponent implements OnDestroy, OnInit {
    private element: JQuery;

    @Input() id: string;

    constructor(private modalService: ModalService, private el: ElementRef) {
        this.element = $(el.nativeElement);
    }

    ngOnInit(): void {
        const modal = this;

        // Ensure id attribute exists.
        if (!this.id) {
            console.error('modal must have an id');
            return;
        }

        // Move element to bottom of page (just before </body>) so it can be displayed above everything else.
        this.element.appendTo('body');

        // Close modal on background click.
        // this.element.on('click', function (e: any) {
        //     const target = $(e.target);
        //     if (!target.closest('.modal-body').length) {
        //         modal.close();
        //     }
        // });

        // Add self (this modal instance) to the modal service so it's accessible from controllers.
        this.modalService.add(this);
    }

    // Remove self from modal service when directive is destroyed.
    ngOnDestroy(): void {
        this.modalService.remove(this.id);
        this.element.remove();
    }

    close(): void {
        this.element.hide();
        $('body').removeClass('modal-open');
    }

    open(): void {
        this.element.show();
        $('body').addClass('modal-open');
    }
}
