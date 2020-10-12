import { Component, Input, OnInit } from '@angular/core';
import { Hero } from 'src/app/models/hero';
import { HeroService } from 'src/app/service/hero.service';
import { Constants } from 'src/app/utils/constants';

@Component({
  selector: 'admin-map-component',
  templateUrl: './admin-map.component.html',
  styleUrls: ['./admin-map.component.scss'],
})
export class AdminMapComponent implements OnInit {
  public openedWindow;
  public entities: any[];
  public heroes: any[];
  public villains: any[];
  public latitude: number = 51.673858;
  public longitude: number = 7.815982;
  public zoom: number = 5;
  public icon = {
    url: 'assets/svg/placeholder_hero.svg',
    scaledSize: {
      width: 40,
      height: 50,
    },
  };

  public iconVillain = {
    url: 'assets/svg/placeholder_villain.svg',
    scaledSize: {
      width: 40,
      height: 50,
    },
  };

  @Input() mapToggle: boolean;
  constructor(private _heroService: HeroService) {}
  ngOnInit() {
    this._heroService.getAminMapRowData().subscribe((heroes) => {
      this.entities = heroes;
      this.heroes = heroes.filter((hero) => hero.isBadGuy !== true);
      this.villains = heroes.filter((hero) => hero.isBadGuy === true);
    });
  }

  public seeLocationHero_Villain(data: Hero) {
    this.latitude = data.latitude;
    this.longitude = data.longitude;
    this.zoom = 15;
  }

  public displayAffiliation(hero: any): string {
    return hero.isBadGuy ? Constants.VILLAIN : Constants.HERO;
  }

  public isInfoWindowOpen(id) {
    return this.openedWindow == id;
  }

  public closeWindow(): void {
    this.openedWindow = null;
  }

  public openWindow(data): void {
    this.latitude = data.address.latitude;
    this.longitude = data.address.longitude;
    this.zoom = 15;
    setTimeout(() => {
      this.openedWindow = data.id;
    }, 100);
  }

  public openWindowFromSideMenu(data): void {
    this.latitude = data.latitude;
    this.longitude = data.longitude;
    this.zoom = 15;
    setTimeout(() => {
      this.openedWindow = data.id;
    }, 100);
  }
}
