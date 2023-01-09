import { NgModule } from '@angular/core';

import { SharedModule } from '../../shared.module';
import { DashboardLayoutComponent } from './dashboard-layout.component';
import { FooterComponent } from './footer/footer.component';
import { SideBarComponent } from './side-bar/side-bar.component';
import { TopBarComponent } from './top-bar/top-bar.component';

@NgModule({
  declarations: [
    SideBarComponent,
    DashboardLayoutComponent,
    TopBarComponent,
    FooterComponent
  ],
  imports: [SharedModule],
  exports: [DashboardLayoutComponent, SideBarComponent],
})
export class DashboardLayoutModule { }
