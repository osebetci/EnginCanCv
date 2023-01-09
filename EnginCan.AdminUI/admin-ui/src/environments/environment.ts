// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

import { Api } from "src/app/shared/core/dto/general.dto";

export const environment = {
  production: false,
  isLoading: false,
  isRfIdConnect: true,
  isRequesting: false,
  loginPath: '/giris',
  isOpenMobileNavbar: false,
  isSueno: false,
  isMiniNavbar: true,
  canTransferTurnsPurchase: true,
  api: <Api>{
    endpoint: 'http://localhost:22197/api',
    token: `${name}-dashboard`,
    contentRootPath: 'http://localhost:22197/api/docs/',
  },
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
