import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/core/account.service';
import { AuthService } from 'src/app/core/auth.service';
import {Permission} from '../../models/permission.model'

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  isCollapsed = false;
  user:any;

  constructor(private authService: AuthService, private router: Router,private accountService:AccountService) { }

  ngOnInit() {
    if (this.router.url == '/dashboard')
      this.router.navigateByUrl('/dashboard/welcome');
    if(this.authService.isLoggedIn){
      this.user=this.authService.currentUser;
    }
  }

  logout() {
    this.authService.logout();
    this.authService.redirectLogoutUser();
  }
  get canViewUser() {
    return this.accountService.userHasPermission(Permission.viewUsersPermission); // eg. viewCustomersPermission
  }
}
