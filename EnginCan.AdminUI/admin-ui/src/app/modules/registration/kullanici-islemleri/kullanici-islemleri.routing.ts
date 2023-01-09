import { YeniKullaniciComponent } from './yeni-kullanici/yeni-kullanici.component';
import { Routes } from '@angular/router';
import { AuthGuardService } from 'src/app/shared/services/auth-guard.service';
import { TumKullanicilarComponent } from './tum-kullanicilar/tum-kullanicilar.component';

export const KullaniciIslemleriRoutes: Routes = [
  {
    path: 'tum-kullanicilar',
    canActivate: [AuthGuardService],
    data: { pageId: 5 },
    component: TumKullanicilarComponent,
  },
  {
    path: 'yeni-kullanici',
    canActivate: [AuthGuardService],
    data: { pageId: 6 },
    component: YeniKullaniciComponent,
  },
  {
    path: 'yeni-kullanici/:id',
    canActivate: [AuthGuardService],
    data: { pageId: 6 },
    component: YeniKullaniciComponent,
  },
];
