import { HttpParams, HttpHeaders } from '@angular/common/http';

export interface Api {
  endpoint: string;
  token: string;
  apiKey: string;
  path: string;
}

export interface HttpParameter {
  version: string;
  url: string;
  reponseType?: string;
  headers?: HttpHeaders;
  params?: HttpParams;
}

export class Login {
  userName: string;
  password: string;
}

export class ResponseLogin {
  token: string;
  loginUser: LoginUser;
}


export class ResponseSicil {
  id: number;
  ad: string;
  soyad: string;
  rol: string;
  kod: string;
  fotograf: string;
  domainName: string;
  firstFireLink: string;
  ip: string;
  sirketId: number | null;
  
}

export class Menu {
  id: number;
  name: string;
  icon: string;
  parentId: number | null;
  allRouterLink: string;
  isComponent: boolean;
  order: number;
  childs: Menu[] = [];
}

export class MenuIds {
  id: number;
  parentId: number | null;
}

export class User {
  id: number;
  ad: string;
  soyad: string;
  ipAddress: string;
  domainName: string;
  rol: string;
  sirket: any;
}

export class Setting {
  menuPosition: string = 'menu-side-left';
  menuStyle: string = 'menu-layout-full';
  menuColor: string = 'color-scheme-light';
  subMenuColor: string = 'sub-menu-color-light';
  fullScreen: string = 'full-screen';
  topBarColor: string = 'color-scheme-light';
  darkMode: string = ' ';
}

export interface Api {
  endpoint: string;
  token: string;
  apiKey: string;
  path: string;
  contentRootPath: string;
}

export interface HttpParameter {
  version: string;
  url: string;
  reponseType?: string;
  headers?: HttpHeaders;
  params?: HttpParams;
}

export class LoginUser {
  id: number;
  name: string;
  surname: string;
  image: string;
  firstFireLink: string;
  organizasyonId: number;
  workYear: number;
  ipAddress: string;
  hostName: string;
  sicilId: number | null;
  branch: string;
  organizationSchemaId: number;
}
