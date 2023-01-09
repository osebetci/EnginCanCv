import { InputMaskModule } from 'primeng/inputmask';
import { HelpersService } from './services/helpers.service';
import { DocsService } from './services/docs.service';
import { InImageCropperComponent } from './tools/in-image-cropper/in-image-cropper.component';
import { ConfirmationDialogComponent } from './tools/confirmation-dialog/confirmation-dialog.component';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { InLoadingPopupComponent } from './tools/in-loading-popup/in-loading-popup.component';
import { ConfirmationDialogService } from './services/confirmation-dialog.service';
import { NgxOrgChartModule } from 'ngx-org-chart';
import {CalendarModule} from 'primeng/calendar';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        RouterModule,
        ReactiveFormsModule,
        NgxOrgChartModule,
        InputMaskModule,
        CalendarModule
    ],
    declarations: [
        InLoadingPopupComponent,
        ConfirmationDialogComponent,
        InImageCropperComponent
    ],
    providers: [ ConfirmationDialogService, DocsService,HelpersService ],
    exports: [
        CommonModule,
        FormsModule,
        RouterModule,
        ReactiveFormsModule,
        NgxOrgChartModule,
        InLoadingPopupComponent,
        InImageCropperComponent,
        InputMaskModule,
        CalendarModule
    ],
    entryComponents: [ ConfirmationDialogComponent ],

})
export class SharedModule { }
