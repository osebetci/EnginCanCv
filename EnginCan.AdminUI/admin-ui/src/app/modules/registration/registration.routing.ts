import { Routes } from '@angular/router';
import { AuthGuardService } from 'src/app/shared/services/auth-guard.service';

export const RegistrationRoutes: Routes = [
    {
      path: '',
      children: [
        {
          path: 'kullanici-islemleri',
          loadChildren: () =>
            import('./kullanici-islemleri/kullanici-islemleri.module').then(
              (m) => m.KullaniciIslemleriModule
            ),
        },

        
      ],
    },
  ];
  