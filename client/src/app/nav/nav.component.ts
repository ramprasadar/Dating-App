import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { Observable, Subscription } from 'rxjs';
import { User } from '../_models/user';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit, OnDestroy {
  model: any = {};

  constructor(
    public accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  login() {
    this.accountService.login(this.model).subscribe({
      next: (response) => {
        console.log(response);
        this.router.navigateByUrl('/members');
        this.toastr.success('Logged In Successfully');
      },
      error: (error) => this.toastr.error(error.error),
      complete: () => console.log('Loged In'),
    });
  }
  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
    this.toastr.info('Logged Out Successfully');
  }

  ngOnDestroy(): void {}
}
