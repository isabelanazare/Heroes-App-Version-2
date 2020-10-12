import { UserData } from 'src/app/models/user-data';
import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { DataManagementBase } from 'src/app/utils/select-row';
import { BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderLayoutComponent
  extends DataManagementBase
  implements OnInit {
  public currentUser: UserData;

  constructor(
    protected modalService: BsModalService,
    protected router: Router,
    private authenticationService: AuthenticationService
  ) {
    super(modalService, router);
    this.authenticationService.currentUser.subscribe((u) => {
      this.currentUser = u;
      this.authenticationService
        .isAuthenticated()
        .subscribe((user) => (this.currentUser.avatarPath = user.avatarPath));
    });
  }

  ngOnInit(): void {}

  public logout() {
    this.authenticationService.logout();
    this.router.navigate(['']);
  }

  protected _loadData() {}
}
