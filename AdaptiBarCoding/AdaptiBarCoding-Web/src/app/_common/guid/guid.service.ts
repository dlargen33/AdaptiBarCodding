import { Injectable } from '@angular/core';

import { Guid } from './guid';

@Injectable()
export class GuidService {
    generate(): string {
        return Guid.Guid();
    }
}
