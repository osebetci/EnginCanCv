import { Routes } from '@angular/router';
import { AuthGuardService, IsLoggedIn } from './shared/services/auth-guard.service';
import { LoginComponent } from './layouts/login/login.component';

export const AppRoutes: Routes = [
  {
    path: 'yonetim',
    loadChildren: () =>
      import('./layouts/main/main.module').then((m) => m.MainModule),
  },
  {
    path: 'giris',
    component: LoginComponent,
    resolve: [IsLoggedIn],
  },
  {
    path: '**',
    pathMatch: 'full',
    redirectTo: 'giris',
  },
];
