import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/shared/services/auth.service';


@Component({
  selector: 'in-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  // encapsulation: ViewEncapsulation.None 
})
export class LoginComponent implements OnInit {
  busy: Promise<any>;
  loginForm: FormGroup;
  backLink: string;
  isForgetPw: boolean = false;
  year = new Date().getFullYear();
  env = environment;
  errorMessage: string;
  successMessage: string;
  title: string;
  logoUrl: string;
  errorMessageVisible = false;
  succesMessageVisible = false;
  get email(): any { return this.loginForm.get('email') }
  get password(): any { return this.loginForm.get('password') }

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {
  }

  ngOnInit() {
    this.backLink = this.route.snapshot.queryParams['from'];
    this.loginForm = this.fb.group({
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
    environment.isRequesting = false;
  }

  loginEnter(e) {
    if (e.keyCode === 13) {
      this.login();
    }
  }

  login() {
    this.errorMessage = null;
    this.successMessage = null;
    if (this.loginForm.valid) {
      environment.isRequesting = true;
      this.authService.login(this.loginForm.value).toPromise().then((res:any) => {     
        if (res.data.token) {
          if (this.backLink) {
            this.router.navigate([this.backLink]);
          } else {
            this.router.navigate([res.data.loginUser?.firstFireLink]);
          }
          environment.isRequesting = false;
        } else if (!res.success) {
          this.errorMessage = res.messages;
          environment.isRequesting = false;
          this.errorMessageVisible = true;     
        }
      })
    }
  }
}
