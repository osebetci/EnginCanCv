import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'in-loading-popup',
    templateUrl: './in-loading-popup.component.html',
    styleUrls: ['./in-loading-popup.component.scss']
})

export class InLoadingPopupComponent {
    environment = environment;
}
