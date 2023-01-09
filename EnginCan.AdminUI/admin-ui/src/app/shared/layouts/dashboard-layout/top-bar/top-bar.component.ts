import { AuthService } from 'src/app/shared/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { LoginUser } from 'src/app/shared/core/dto/general.dto';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'in-top-bar',
  templateUrl: './top-bar.component.html',
  styleUrls: ['./top-bar.component.scss']
})
export class TopBarComponent implements OnInit {
  environment = environment;
  loginUser = new LoginUser();
  breadCrumb: string[] = [];
  passwordPopupVisible = false;
  bildirimlerPopupVisible = false;
  newNotificationCount;
  constructor(public authService:AuthService) { }

  ngOnInit() {
    this.authService.getLoginUser().subscribe((res)=>{
      this.loginUser = res.data;
    });
  }

  reloadNotificationDataSource(){
  }

  allReadNotification(){

  }

  openMobileMenu() {
    environment.isMiniNavbar = !environment.isMiniNavbar;
    environment.isOpenMobileNavbar = !environment.isOpenMobileNavbar;
  }


}
