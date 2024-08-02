
import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { AccountService } from '../../../core/account.service';
import { Utilities } from '../../../core/utilities';
import { User } from '../../../models/user.model';
import { UserEdit } from '../../../models/user-edit.model';
import { Role } from '../../../models/role.model';
import { Permission } from '../../../models/permission.model';
import { FormControl, FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {

  public isEditMode = false;
  public isNewUser = false;
  public isSaving = false;

  public isEditingSelf = false;
  public showValidationErrors = false;
  public uniqueId: string = Utilities.uniqueId();
  public user: User = new User();
  public userEdit: UserEdit;
  public allRoles: Role[] = [];

  public formResetToggle = true;

  public changesSavedCallback: () => void;
  public changesFailedCallback: () => void;
  public changesCancelledCallback: () => void;
  public userId:string='';
  @Input()
  isGeneralEditor = false;

  @Input()
  isShowPassword = false;
  isChangePassword = false;
  validateForm: FormGroup;
  @Input()
  isShowOnModal=false;
  msgid = "";
  constructor(private fb: FormBuilder, private accountService: AccountService, private messageService: NzMessageService, private notificationService: NzNotificationService,private activatedRoute:ActivatedRoute) {
  }

  confirmValidator = (control: FormControl): { [s: string]: boolean } => {
    if (!control.value) {
      return { error: true, required: true };
    } else if (control.value !== this.validateForm.controls.newPassword.value) {
      return { confirm: true, error: true };
    }
    return {};
  };

  validateConfirmPassword(): void {
    setTimeout(() => this.validateForm.controls.confirm.updateValueAndValidity());
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.userId = params['id'];
      });
    this.validateForm = this.fb.group({
      jobTitle: null,
      userName: [null, [Validators.required]],
      email: [null, [Validators.email, Validators.required]],
      roles: [[], [Validators.required]],
      fullName: null,
      phoneNumber: null

    });
    if (!this.isGeneralEditor) {
      this.loadCurrentUserData();
    }
    if (this.isShowPassword) {
      this.changePassword();
    }
  }



  private loadCurrentUserData() {
    if (this.canViewAllRoles) {
      this.accountService.getUserAndRoles().subscribe(results => this.onCurrentUserDataLoadSuccessful(results[0], results[1]), error => this.onCurrentUserDataLoadFailed(error));
    } else {
      this.accountService.getUser().subscribe(user => this.onCurrentUserDataLoadSuccessful(user, user.roles.map(x => new Role(x))), error => this.onCurrentUserDataLoadFailed(error));
    }
  }


  private onCurrentUserDataLoadSuccessful(user: User, roles: Role[]) {
    this.user = user;
    this.allRoles = roles;
    if(this.userId){
      this.accountService.getUser(this.userId)
      .subscribe(user=>this.editUser(user,this.allRoles))
      }
  }

  private onCurrentUserDataLoadFailed(error: any) {
    this.createErrorMessage('error', `Load Error Unable to retrieve user data from the server.\r\nErrors: ${Utilities.getHttpResponseMessages(error)}`);

    this.user = new User();
  }



  getRoleByName(name: string) {
    return this.allRoles.find((r) => r.name === name);
  }



  showErrorAlert(caption: string, message: string) {
    this.createErrorMessage('error', `${caption} ${message}`);
  }


  deletePasswordFromUser(user: UserEdit | User) {
    const userEdit = user as UserEdit;

    delete userEdit.currentPassword;
    delete userEdit.newPassword;
    delete userEdit.confirmPassword;
  }


  edit() {
    if (!this.isGeneralEditor) {
      this.isEditingSelf = true;
      this.userEdit = new UserEdit();

      Object.assign(this.userEdit, this.user);

    } else {
      if (!this.userEdit) {
        this.userEdit = new UserEdit();
      }
      this.isEditingSelf = this.accountService.currentUser ? this.userEdit.id === this.accountService.currentUser.id : false;
    }

    this.isEditMode = true;
    this.showValidationErrors = true;
    this.isChangePassword = false;
  }


  save() {
    if (this.validateForm.valid) {
      this.isSaving = true;
      this.createMessage('Saving changes...');

      this.userEdit = { ...this.userEdit, ...this.validateForm.value };
      if (this.isNewUser) {
        this.accountService.newUser(this.userEdit).subscribe(user => this.saveSuccessHelper(user), error => this.saveFailedHelper(error));
      } else {
        this.accountService.updateUser(this.userEdit).subscribe(response => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
      }
    }
  }


  private saveSuccessHelper(user?: User) {
    this.testIsRoleUserCountChanged(this.user, this.userEdit);

    if (user) {
      Object.assign(this.userEdit, user);
    }

    this.isSaving = false;
    this.messageService.remove(this.msgid);
    this.isChangePassword = false;
    this.showValidationErrors = false;

    this.deletePasswordFromUser(this.userEdit);
    Object.assign(this.user, this.userEdit);
    this.userEdit = new UserEdit();

    if (this.isGeneralEditor) {
      if (this.isNewUser) {
        this.createErrorMessage('success', `User \"${this.user.userName}\" was created successfully`);

      } else if (!this.isEditingSelf) {
        this.createErrorMessage('success', `Changes to user \"${this.user.userName}\" was saved successfully`);

      }
    }

    if (this.isEditingSelf) {
      this.createErrorMessage('success', 'Changes to your User Profile was saved successfully');
      this.refreshLoggedInUser();
    }

    this.isEditMode = false;


    if (this.changesSavedCallback) {
      this.changesSavedCallback();
    }
  }


  private saveFailedHelper(error: any) {
    this.isSaving = false;
    this.messageService.remove(this.msgid);
    this.createErrorMessage('error', `${JSON.stringify(error)}`);
    if (this.changesFailedCallback) {
      this.changesFailedCallback();
    }
  }



  private testIsRoleUserCountChanged(currentUser: User, editedUser: User) {

    const rolesAdded = this.isNewUser ? editedUser.roles : editedUser.roles.filter(role => currentUser.roles.indexOf(role) === -1);
    const rolesRemoved = this.isNewUser ? [] : currentUser.roles.filter(role => editedUser.roles.indexOf(role) === -1);

    const modifiedRoles = rolesAdded.concat(rolesRemoved);

    if (modifiedRoles.length) {
      setTimeout(() => this.accountService.onRolesUserCountChanged(modifiedRoles));
    }
  }



  cancel() {
    // if (this.isGeneralEditor) {
    //   this.userEdit = this.user = new UserEdit();
    // } else {
    //   this.userEdit = new UserEdit();
    // }

    // this.showValidationErrors = false;
    // this.resetForm();

    // // this.alertService.showMessage('Cancelled', 'Operation cancelled by user', MessageSeverity.default);
    // // this.alertService.resetStickyMessage();

    // if (!this.isGeneralEditor) {
    //   this.isEditMode = false;
    // }

    // if (this.changesCancelledCallback) {
    //   this.changesCancelledCallback();
    // }
  }


  close() {
    // this.userEdit = this.user = new UserEdit();
    // this.showValidationErrors = false;
    // this.resetForm();
    // this.isEditMode = false;

    // if (this.changesSavedCallback) {
    //   this.changesSavedCallback();
    // }
  }



  private refreshLoggedInUser() {
    this.accountService.refreshLoggedInUser()
      .subscribe(user => {
        this.loadCurrentUserData();
      },
        error => {
          this.createErrorMessage('error', 'Refresh failed An error occured whilst refreshing logged in user information from the server');

        });
  }


  changePassword() {
    if (!this.isNewUser && this.isEditingSelf) {
      this.validateForm.addControl('currentPassword', new FormControl('', Validators.required));
    }
    this.validateForm.addControl('newPassword', new FormControl('', [Validators.required]));
    this.validateForm.addControl('confirmPassword', new FormControl('', [Validators.required, this.confirmValidator]));
    this.isChangePassword = true;
  }


  unlockUser() {
    this.isSaving = true;
    // this.alertService.startLoadingMessage('Unblocking user...');


    this.accountService.unblockUser(this.userEdit.id)
      .subscribe(() => {
        this.isSaving = false;
        this.userEdit.isLockedOut = false;
        // this.alertService.stopLoadingMessage();
        // this.alertService.showMessage('Success', 'User has been successfully unblocked', MessageSeverity.success);
      },
        error => {
          this.isSaving = false;
          // this.alertService.stopLoadingMessage();
          // this.alertService.showStickyMessage('Unblock Error', 'The below errors occured whilst unblocking the user:', MessageSeverity.error, error);
          // this.alertService.showStickyMessage(error, null, MessageSeverity.error);
        });
  }


  resetForm(replace = false) {
    // this.isChangePassword = false;

    // if (!replace) {
    //   this.form.reset();
    // } else {
    //   this.formResetToggle = false;

    //   setTimeout(() => {
    //     this.formResetToggle = true;
    //   });
    // }
  }


  newUser(allRoles: Role[]) {

    this.isGeneralEditor = true;
    this.isNewUser = true;

    this.allRoles = [...allRoles];
    this.user = this.userEdit = new UserEdit();
    this.userEdit.isEnabled = true;
    this.edit();
    this.changePassword();
    return this.userEdit;
  }

  editUser(user: User, allRoles: Role[]) {
    if (user) {
      this.isGeneralEditor = true;
      this.isNewUser = false;

      this.setRoles(user, allRoles);
      this.user = new User();
      this.userEdit = new UserEdit();
      Object.assign(this.user, user);
      Object.assign(this.userEdit, user);
      this.validateForm.controls['userName'].setValue(user.userName);
      this.validateForm.controls['email'].setValue(user.email);
      this.validateForm.controls['jobTitle'].setValue(user.jobTitle);
      this.validateForm.controls['roles'].setValue(user.roles);
      this.validateForm.controls['fullName'].setValue(user.fullName);
      this.validateForm.controls['phoneNumber'].setValue(user.phoneNumber);
      this.edit();

      return this.userEdit;
    } else {
      return this.newUser(allRoles);
    }
  }


  displayUser(user: User, allRoles?: Role[]) {

    this.user = new User();
    Object.assign(this.user, user);
    this.deletePasswordFromUser(this.user);
    this.setRoles(user, allRoles);

    this.isEditMode = false;
  }



  private setRoles(user: User, allRoles?: Role[]) {

    this.allRoles = allRoles ? [...allRoles] : [];

    if (user.roles) {
      for (const ur of user.roles) {
        if (!this.allRoles.some(r => r.name === ur)) {
          this.allRoles.unshift(new Role(ur));
        }
      }
    }
  }



  get canViewAllRoles() {
    return this.accountService.userHasPermission(Permission.viewRolesPermission);
  }

  get canAssignRoles() {
    return this.accountService.userHasPermission(Permission.assignRolesPermission);
  }

  createMessage(msg: string): void {
    this.msgid = this.messageService.loading(msg, { nzDuration: 0 }).messageId;

  }

  createErrorMessage(type, msg: string): void {
    this.msgid = this.messageService.create(type, msg, { nzDuration: 2000 }).messageId;;
  }
}
