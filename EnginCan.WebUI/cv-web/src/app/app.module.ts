import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AppRoutes } from './app.routing';
import { RouterModule } from '@angular/router';
import { LOCALE_ID } from '@angular/core';
import localeTr from '@angular/common/locales/tr';
import { APP_BASE_HREF, registerLocaleData } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import {ToastrModule} from "ngx-toastr";

registerLocaleData(localeTr, 'tr-TR');

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(AppRoutes, {
      onSameUrlNavigation: 'reload',
      scrollPositionRestoration: 'enabled',
    }),
    ReactiveFormsModule,
    ToastrModule.forRoot({
      positionClass:"toast-bottom-right"
    })
  ],
  providers: [
    { provide: APP_BASE_HREF, useValue: '/' },
    {
      provide: LOCALE_ID,
      useValue: 'tr-TR',
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
