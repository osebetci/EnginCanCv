import { Routes } from '@angular/router';
import { AuthGuardService, IsLoggedIn } from './shared/services/auth-guard.service';

export const AppRoutes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./layouts/main/main.module').then((m) => m.MainModule),
  },

];
