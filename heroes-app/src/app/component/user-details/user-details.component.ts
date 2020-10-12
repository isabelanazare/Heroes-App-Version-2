import { AlertService } from './../../service/alert.service';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { VillainService } from 'src/app/service/villain.service';
import { Hero } from 'src/app/models/hero';
import { HeroService } from 'src/app/service/hero.service';
import { UserDataService } from 'src/app/service/user-data.service';
import { Component, OnInit } from '@angular/core';
import { UserData } from 'src/app/models/user-data';
import { Constants } from 'src/app/utils/constants';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css'],
})
export class UserDetailsComponent implements OnInit {
  public active = 1;
  public user: UserData;
  public hero: Hero;

  constructor(
    private userDataService: UserDataService,
    private authenticationService: AuthenticationService,
    private heroService: HeroService,
    private villainService: VillainService,
    private alertService: AlertService
  ) {}

  ngOnInit(): void {
    this.authenticationService.currentUser.subscribe((user) => {
      this.user = user;
      this.heroService.getHeroByUserId(this.user.id).subscribe((hero) => {
        this.hero = hero;
        const birthday = new Date(hero.birthday);
        birthday.setHours(birthday.getHours() + 3);
        this.hero.birthday = birthday.toISOString().substring(0, 10);
        this.hero.avatarPath = Constants.APP_URL + this.hero.avatarPath;
      });
    });
  }
}
