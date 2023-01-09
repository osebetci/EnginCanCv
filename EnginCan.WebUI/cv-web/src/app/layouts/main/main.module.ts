import { FooterComponent } from './footer/footer.component';
import { ContactComponent } from './contact/contact.component';
import { QualificationComponent } from './qualification/qualification.component';
import { AboutComponent } from './about/about.component';

import { NavbarComponent } from './navbar/navbar.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MainRoutes } from './main.routing';
import { MainComponent } from './main.component';
import { HeaderComponent } from './header/header.component';
import { SkillComponent } from './skill/skill.component';
@NgModule({
  declarations: [MainComponent,MainComponent,ContactComponent,FooterComponent,SkillComponent,NavbarComponent,HeaderComponent,AboutComponent,QualificationComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(MainRoutes),
  ]
})
export class MainModule { }

