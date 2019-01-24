import { Injectable } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { ErrorMessage } from '../../_models/error-message';

@Injectable()
export class AlertService {
    private shouldDisplayAfterNavigationChange = false;
    private subject = new Subject<any>();

    constructor(private readonly router: Router) {
        // Clear the alert message on route change.
        router.events.subscribe(event => {
            if (event instanceof NavigationStart) {
                if (this.shouldDisplayAfterNavigationChange) {
                    // Only keep for a single location change.
                    this.shouldDisplayAfterNavigationChange = false;
                } else {
                    // Clear the alert.
                    this.subject.next();
                }
            }
        });
    }

    clear(): void {
        this.shouldDisplayAfterNavigationChange = false;
        this.subject.next();
    }

    getMessage(): Observable<any> {
        return this.subject.asObservable();
    }

    error(error: any, keepAfterNavigationChange = false): void {
        if (error.status === 998) {
            this.router.navigateByUrl('/maintenance-mode');
        } else {
            this.shouldDisplayAfterNavigationChange = keepAfterNavigationChange;
            this.subject.next({type: 'error', text: this.setErrorText(error)});
        }
    }

    errorString(error: string, keepAfterNavigationChange = false): void {
        this.shouldDisplayAfterNavigationChange = keepAfterNavigationChange;
        this.subject.next({type: 'error', text: error});
    }

    success(message: any, keepAfterNavigationChange = false): void {
        this.shouldDisplayAfterNavigationChange = keepAfterNavigationChange;
        this.subject.next({type: 'success', text: message});
    }

    warning(message: any, keepAfterNavigationChange = false): void {
        this.shouldDisplayAfterNavigationChange = keepAfterNavigationChange;
        this.subject.next({type: 'warning', text: message});
    }

    private setErrorText(error: any): string {
        let message = 'An error occurred';

        if (error != null) {
            if (error.status != null) {
                if (error.status === 401) {
                    return null;
                }
            }
            if (error.error != null) {
                if (error.error.message != null) {
                    message = error.error.message.replace('["', '').replace('"]', '');
                }
            } else {
                if (error.message != null) {
                    message = error.message.replace('["', '').replace('"]', '');
                } else {
                    if (typeof(error) === 'string') {
                        message = error;
                    }
                }
            }
        }

        return message;
    }
}
