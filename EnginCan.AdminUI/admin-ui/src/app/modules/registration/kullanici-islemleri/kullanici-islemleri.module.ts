import { ToastrModule } from 'ngx-toastr';
import { SharedModule } from './../../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DashboardLayoutModule } from 'src/app/shared/layouts/dashboard-layout/dashboard-layout.module';
import { TumKullanicilarComponent } from './tum-kullanicilar/tum-kullanicilar.component';
import { YeniKullaniciComponent } from './yeni-kullanici/yeni-kullanici.component';
import { KullaniciIslemleriRoutes } from './kullanici-islemleri.routing';

@NgModule({
  declarations: [TumKullanicilarComponent,YeniKullaniciComponent],
  imports: [
    CommonModule,SharedModule,
    RouterModule.forChild(KullaniciIslemleriRoutes),
    DashboardLayoutModule
  ]
})
export class KullaniciIslemleriModule { }

