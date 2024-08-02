import { Component, OnInit, AfterViewInit, TemplateRef, ViewChild, Input } from '@angular/core';
import { AccountService } from '../../../core/account.service';
import { Utilities } from '../../../core/utilities';
import { User } from '../../../models/user.model';
import { Role } from '../../../models/role.model';
import { Permission } from '../../../models/permission.model';
import { UserEdit } from '../../../models/user-edit.model';
import { UserInfoComponent } from '../user-info/user-info.component';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { AuthService } from 'src/app/core/auth.service';

@Component({
  selector: 'app-users-management',
  templateUrl: './user-management.component.html',
})
export class UsersManagementComponent implements OnInit, AfterViewInit {
  columns: any[] = [];
  rows: User[] = [];
  rowsCache: User[] = [];
  editedUser: UserEdit;
  sourceUser: UserEdit;
  editingUserName: { name: string };
  loadingIndicator: boolean;
  loading = true;

  allRoles: Role[] = [];

  modalRef: NzModalRef;
  msgid = '';
  
  constructor(private modal: NzModalService, private accountService: AccountService, private messageService: NzMessageService, private notificationService: NzNotificationService,private authService: AuthService) {
  }


  ngOnInit() {
    // if (this.canManageUsers) {
    //   //this.columns.push({ name: '', width: 160, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false });
    // }
    this.loadData();
  }


  ngAfterViewInit() {

  }


  addNewUserToList() {
    if (this.sourceUser) {
      Object.assign(this.sourceUser, this.editedUser);
      let sourceIndex = this.rowsCache.indexOf(this.sourceUser, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rowsCache, sourceIndex, 0);
      }

      sourceIndex = this.rows.indexOf(this.sourceUser, 0);
      if (sourceIndex > -1) {
        Utilities.moveArrayItem(this.rows, sourceIndex, 0);
      }

      this.editedUser = null;
      this.sourceUser = null;
    } else {
      const user = new User();
      Object.assign(user, this.editedUser);
      this.editedUser = null;
      let maxIndex = 0;
      for (const u of this.rowsCache) {
        if ((u as any).index > maxIndex) {
          maxIndex = (u as any).index;
        }
      }

      (user as any).index = maxIndex + 1;

      this.rowsCache.splice(0, 0, user);
      this.rows.splice(0, 0, user);
      this.rows = [...this.rows];
    }
  }


  loadData() {
    // this.createMessage('Loading...!');
    // this.loadingIndicator = true;

    if (this.canViewRoles) {
      this.accountService.getUsersAndRoles().subscribe(results => this.onDataLoadSuccessful(results[0], results[1]), error => this.onDataLoadFailed(error));
    } else {
      this.accountService.getUsers().subscribe(users => this.onDataLoadSuccessful(users, this.accountService.currentUser.roles.map(x => new Role(x))), error => this.onDataLoadFailed(error));
    }
  }


  onDataLoadSuccessful(users: User[], roles: Role[]) {
    // this.messageService.remove(this.msgid);
    // this.loadingIndicator = false;
    this.loading=false;

    users.forEach((user, index) => {
      (user as any).index = index + 1;
    });

    this.rowsCache = [...users];
    this.rows = users;

    this.allRoles = roles;
  }


  onDataLoadFailed(error: any) {
    // this.messageService.remove(this.msgid);
    // this.loadingIndicator = false;
    this.loading=false;
    console.log(error);
    this.createErrorMessage('error', `Unable to retrieve users from the server.\r\nErrors: "${Utilities.getHttpResponseMessages(error)}"`);
  }


  onSearchChanged(value: string) {
    this.rows = this.rowsCache.filter(r => Utilities.searchArray(value, false, r.userName, r.fullName, r.email, r.phoneNumber, r.jobTitle, r.roles));
  }


  newUser() {
    let that = this;
    this.editingUserName = null;
    this.sourceUser = null;
    this.modalRef = this.modal.create({
      nzTitle: 'New User',
      nzContent: UserInfoComponent,
      nzFooter: [
        {
          label: 'Cancel',
          shape: 'round',
          onClick: () => this.modalRef.destroy()
        },
        {
          label: 'Save',
          type: 'primary',
          shape: 'round',
          loading: false,
          disabled: false,
          onClick: function (componentInstance) {
            this.loading = true;
            this.disabled = true;
            for (const key in componentInstance.validateForm.controls) {
              componentInstance.validateForm.controls[key].markAsDirty();
              componentInstance.validateForm.controls[key].updateValueAndValidity();
            }

            for (const key in componentInstance.validateForm.controls) {
              componentInstance.validateForm.controls[key].markAsDirty();
              componentInstance.validateForm.controls[key].updateValueAndValidity();
            }

            if (componentInstance.validateForm.valid) {
              componentInstance.save();
              componentInstance.changesSavedCallback = () => {
                this.loading = true;
                this.disabled = true;
                that.editedUser = componentInstance.user as any;
                that.addNewUserToList();
                that.modalRef.destroy();
              };

              componentInstance.changesCancelledCallback = () => {
                that.editedUser = null;
                that.sourceUser = null;
                that.modalRef.destroy();
              };
              componentInstance.changesFailedCallback = () => {
                this.loading = false;
                this.disabled = false;
              };
            }
            else {
              setTimeout(() => {
                this.loading = false;
                this.disabled = false;
              }, 500);

            }
          }

        }
      ],
      nzWidth: '600px',
      nzComponentParams: {
        isGeneralEditor: false,
        isShowPassword: true,
        isShowOnModal:true
      }
    });
    this.modalRef.afterOpen.subscribe(() => {
      let info: UserInfoComponent = this.modalRef.getContentComponent();
      info.editUser(this.sourceUser, this.allRoles);
    });
  }

  editUser(row: UserEdit) {
    let that = this;
    this.editingUserName = { name: row.userName };
    this.sourceUser = row;
    this.modalRef = this.modal.create({
      nzTitle: 'Edit User',
      nzStyle: { top: '20px' },
      nzContent: UserInfoComponent,
      nzFooter: [
        {
          label: 'Cancel',
          shape: 'round',
          onClick: () => this.modalRef.destroy()
        },
        {
          label: 'Update',
          type: 'primary',
          shape: 'round',
          loading: false,
          disabled: false,
          onClick: function (componentInstance) {
            this.loading = true;
            this.disabled = true;
            for (const key in componentInstance.validateForm.controls) {
              componentInstance.validateForm.controls[key].markAsDirty();
              componentInstance.validateForm.controls[key].updateValueAndValidity();
            }

            for (const key in componentInstance.validateForm.controls) {
              componentInstance.validateForm.controls[key].markAsDirty();
              componentInstance.validateForm.controls[key].updateValueAndValidity();
            }

            if (componentInstance.validateForm.valid) {
              componentInstance.save();
              componentInstance.changesSavedCallback = () => {
                this.loading = false;
                this.disabled = false;
                that.editedUser = componentInstance.user as any;
                that.addNewUserToList();
                that.modalRef.destroy();
              };

              componentInstance.changesCancelledCallback = () => {
                that.editedUser = null;
                that.sourceUser = null;
                that.modalRef.destroy();
              };
            }
            else {
              setTimeout(() => {
                this.loading = false;
                this.disabled = false;
              }, 500);

            }
          }

        },
      ],
      nzWidth: '600px',
      nzComponentParams: {
        isGeneralEditor: true,
        isShowOnModal:true
      }
    });
    this.modalRef.afterOpen.subscribe(() => {
      let info: UserInfoComponent = this.modalRef.getContentComponent();
      info.editUser(row, this.allRoles);
    });
  }

  deleteUser(row: UserEdit) {
    this.showConfirm(row);
    //this.alertService.showDialog('Are you sure you want to delete \"' + row.userName + '\"?', DialogType.confirm, () => this.deleteUserHelper(row));
  }


  deleteUserHelper(row: UserEdit) {
    console.log()
    if(this.authService.currentUser.id==row.id){
      this.createErrorMessage('error','Cannot delete yourself.');
      return;
    }
    this.createMessage('Deleting...');
    this.accountService.deleteUser(row)
      .subscribe(results => {
        this.messageService.remove(this.msgid);

        this.rowsCache = this.rowsCache.filter(item => item !== row);
        this.rows = this.rows.filter(item => item !== row);
      },
        error => {
          this.messageService.remove(this.msgid);
          this.loadingIndicator = false;
          this.createErrorMessage('error', `Delete Error.An error occured whilst deleting the user.\r\nError: "${Utilities.getHttpResponseMessages(error)}"`)

        });
  }
  get canAssignRoles() {
    return this.accountService.userHasPermission(Permission.assignRolesPermission);
  }

  get canViewRoles() {
    return this.accountService.userHasPermission(Permission.viewRolesPermission);
  }

  get canManageUsers() {
    return this.accountService.userHasPermission(Permission.manageUsersPermission);
  }

  createMessage(msg: string): void {
    this.msgid = this.messageService.loading(msg, { nzDuration: 0 }).messageId;

  }

  createErrorMessage(type, msg: string): void {
    this.msgid = this.messageService.create(type, msg, { nzDuration: 500 }).messageId;;
  }

  showConfirm(row): void {
    this.modalRef = this.modal.confirm({
      nzTitle: `Do you Want to delete these ${row.userName}?`,
      nzContent: '',
      nzOnOk: () => {
        this.deleteUserHelper(row);
      }
    });
  }
}
