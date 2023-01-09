import { LoginUser } from './../../../core/dto/general.dto';
import { AuthService } from 'src/app/shared/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Menu, MenuIds } from 'src/app/shared/core/dto/general.dto';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'in-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss']
})
export class SideBarComponent implements OnInit {
  environment = environment;
  menus: Menu[] = [];
  visibleMenus: Menu[] = [];
  menuIds: MenuIds[] = [];
  selectedMenuId: number[] = [];
  loginUser:LoginUser = new LoginUser();
  logoImg: string;
  constructor(private authService:AuthService) { }

  ngOnInit() {
    environment.isMiniNavbar = false;
    this.authService.getLoginUser().subscribe((res)=>{
      this.loginUser = res.data;    
    });

    this.authService.getAllPage().subscribe((res)=>{     
      this.preCreateMenu(res, res.find((a) => a.parentId == null).id).then(
        (created) => {
          this.menus = created;
          this.visibleMenus = [...created];
        }
      );   
    })
  }

  preCreateMenu(menus: Menu[], parentId: number) {
    const newMenu = [];
    menus
      .filter((a) => a.parentId == parentId)
      .forEach((menu) => {
        this.preCreateMenu(menus, menu.id).then((childs) => {
          this.menuIds.push(<MenuIds>{
            id: menu.id,
            parentId: menu.parentId,
          });

          newMenu.push(<Menu>{
            id: menu.id,
            name: menu.name,
            icon: menu.icon,
            parentId: menu.parentId,
            allRouterLink: menu.allRouterLink,
            isComponent: menu.isComponent,
            order: menu.order,
            childs: childs,
          });
        });
      });

    return Promise.all(newMenu).then(() => newMenu);
  }

  selectMenu(menuId: number) {
    if (this.selectedMenuId.indexOf(menuId) < 0) {
      const parentId = this.menuIds.find((a) => a.id == menuId).parentId;
      if (this.selectedMenuId.indexOf(parentId) < 0) {
        this.selectedMenuId = [menuId];
      } else {
        this.selectedMenuId.push(menuId);
        this.menuIds
          .filter((a) => a.id != menuId && a.parentId == parentId)
          .forEach((val) => {
            const index = this.selectedMenuId.indexOf(val.id);
            if (index > -1) {
              this.selectedMenuId.splice(index, 1);
            }
          });
      }
    } else {
      const index = this.selectedMenuId.indexOf(menuId);
      if (index > -1) {
        this.selectedMenuId.splice(index, 1);
      }
    }
  }

  checkMiniNavbar() {
    localStorage.setItem(
      'mini-navbar',
      environment.isMiniNavbar != undefined
        ? String(!environment.isMiniNavbar)
        : String(true)
    );

    environment.isMiniNavbar =
      environment.isMiniNavbar != undefined ? !environment.isMiniNavbar : true;
  }

  openMobileMenu() {
    environment.isMiniNavbar = !environment.isMiniNavbar;
    environment.isOpenMobileNavbar = !environment.isOpenMobileNavbar;
  }
  
  search(event) {
    if (event.target.value && event.target.value.length > 2) {
      this.visibleMenus = this.filterData(
        JSON.parse(JSON.stringify(this.menus)),
        event.target.value.toLocaleLowerCase(),
        false
      );
    } else {
      this.visibleMenus = [...this.menus];
    }
  }
  filterData(data, text, isContains) {
    return data.filter(
      function (o) {
        if (o.childs) {
          o.childs = this.filterData(
            [...o.childs],
            text,
            o.name.toLocaleLowerCase().includes(text)
          );
        }
        return (
          isContains ||
          o.name.toLocaleLowerCase().includes(text) ||
          o.childs.length > 0
        );
      }.bind(this)
    );
  }


  trackByFn(index: number, menu): number {
    return menu.id;
  }
}
