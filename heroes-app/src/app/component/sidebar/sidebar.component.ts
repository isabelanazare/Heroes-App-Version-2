import { AlertService } from 'src/app/service/alert.service';
import { HeroService } from 'src/app/service/hero.service';
import { BattleService } from 'src/app/service/battle.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { SharedService } from 'src/app/service/shared.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent implements OnInit {
  public role: string;
  public isLoading: boolean;

  constructor(
    private sharedService: SharedService,
    private router: Router,
    private authenticationService: AuthenticationService,
    private battleService: BattleService,
    private heroService: HeroService,
    private alertService: AlertService
  ) {}

  public ngOnInit(): void {
    this.isLoading = true;
    this.authenticationService.currentUser.subscribe((user) => {
      this.role = user.role;
      this.isLoading = false;
      if (this.role === 'Regular') {
        this.heroService.getHeroByUserId(user.id).subscribe((hero) => {
          this.battleService.getBattlesCount(hero.id).subscribe((res) => {
            if (!localStorage.getItem('battleCount')) {
              localStorage.setItem('battleCount', res.toString());
            } else {
              const current = +localStorage.getItem('battleCount');
              if (current != res) {
                localStorage.setItem('battleCount', res.toString());
                this.alertService.alertNotification(res - current);
              }
            }
          });
        });
      }
    });
  }

  public logout(): void {
    this.authenticationService.logout();
    this.router.navigate(['']);
  }

  public updateSharedService() {
    this.sharedService.changeCurrentIsUpdated(false);
  }
}
