
import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { AuthService } from '../../core/auth.service';
import { ConfigurationService } from '../../core/configuration.service';
import { Utilities } from '../../core/utilities';
import { UserLogin } from '../../models/user-login.model';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit, OnDestroy {

  loginForm: FormGroup;
  userLogin = new UserLogin();
  isLoading = false;
  islogging = false;
  loginStatusSubscription: any;
  id: string = '';

  @Input()
  isModal = false;

  notiIds=[];


  constructor(private fb: FormBuilder, private authService: AuthService, private configurations: ConfigurationService, private messageService: NzMessageService, private notificationService: NzNotificationService) {

  }

  ngOnInit() {
    this.createAdminNotification();
    this.createUserNotification();

    this.loginForm = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
      rememberMe: [false]
    });
    this.userLogin.rememberMe = this.authService.rememberMe;

    if (this.getShouldRedirect()) {
      this.authService.redirectLoginUser();
    } else {
      this.loginStatusSubscription = this.authService.getLoginStatusEvent().subscribe(isLoggedIn => {
        if (this.getShouldRedirect()) {
          this.authService.redirectLoginUser();
        }
      });
    }
  }


  ngOnDestroy() {
    if (this.loginStatusSubscription) {
      this.loginStatusSubscription.unsubscribe();
    }
  }


  getShouldRedirect() {
    return !this.isModal && this.authService.isLoggedIn && !this.authService.isSessionExpired;
  }


  showErrorAlert(caption: string, message: string) {

  }

  submit() {
    if(this.loginForm.valid){
      this.islogging = true;
    let data = {
      userName: this.loginForm.controls.userName.value,
      password: this.loginForm.controls.password.value,
      rememberMe: this.loginForm.controls.password.value,
    };
    this.userLogin.userName = data.userName;
    this.userLogin.password = data.password;
    this.userLogin.rememberMe = data.rememberMe;
    this.isLoading = true;
    this.createMessage(`Attemping Login!`);
    this.authService.loginWithPassword(this.userLogin.userName, this.userLogin.password, this.userLogin.rememberMe)
      .subscribe(
        user => {
          this.messageService.remove(this.id);
          this.createMessage(`Welcome ${user.userName}!`);
          setTimeout(() => {
            this.notiIds.forEach(x=>this.notificationService.remove(x));
            this.isLoading = false;
            this.messageService.remove(this.id);
          }, 1500);

        },
        error => {
          console.log(error);
          this.islogging = false;
          this.messageService.remove(this.id);
          if (Utilities.checkNoNetwork(error)) {
            this.createErrorMessage('error', Utilities.noNetworkMessageDetail);
            this.offerAlternateHost();
          } else {
            const errorMessage = Utilities.getHttpResponseMessage(error);

            if (errorMessage) {
              this.createErrorMessage('error', errorMessage);
            } else {
              this.createErrorMessage('error', 'An error occured whilst logging in, please try again later.\nError: ');

            }
          }
          setTimeout(() => {
            this.isLoading = false;
            this.messageService.remove(this.id);
          }, 1000);
        });
    }
    else{
      for (const key in this.loginForm.controls) {
        this.loginForm.controls[key].markAsDirty();
        this.loginForm.controls[key].updateValueAndValidity();
      }
    }
    
  }

  offerAlternateHost() {

    if (Utilities.checkIsLocalHost(location.origin) && Utilities.checkIsLocalHost(this.configurations.baseUrl)) {
      // this.alertService.showDialog('Dear Developer!\nIt appears your backend Web API service is not running...\n' +
      //   'Would you want to temporarily switch to the online Demo API below?(Or specify another)',
      //   DialogType.prompt,
      //   (value: string) => {
      //     this.configurations.baseUrl = value;
      //     this.configurations.tokenUrl = value;
      //     this.alertService.showStickyMessage('API Changed!', 'The target Web API has been changed to: ' + value, MessageSeverity.warn);
      //   },
      //   null,
      //   null,
      //   null,
      //   this.configurations.fallbackBaseUrl);
    }
  }


  mapLoginErrorMessage(error: string) {

    if (error === 'invalid_username_or_password') {
      return 'Invalid username or password';
    }

    if (error === 'invalid_grant') {
      return 'This account has been disabled';
    }

    return error;
  }

  createMessage(msg: string): void {
    const id = this.messageService.loading(msg, { nzDuration: 0 }).messageId;

  }

  createErrorMessage(type, msg: string): void {
    const id = this.messageService.create(type, msg).messageId;
  }

  createAdminNotification(): void {
    this.notiIds.push(this.notificationService.blank(
      'Admin Account Info',
      'Login Name - admin <br/> Passowrd - Admin@123',
      { nzDuration: 0 }
    ).messageId);
  }
  createUserNotification(): void {
    this.notiIds.push(this.notificationService.blank(
      'User Account Info',
      'Login Name - user <br/> Passowrd - User@123',
      { nzDuration: 0 }
    ).messageId);
  }
}
