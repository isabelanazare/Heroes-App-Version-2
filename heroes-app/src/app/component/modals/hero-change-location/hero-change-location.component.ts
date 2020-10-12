import { ModalBase } from '../../../modals/modal-base';
import { Component, ElementRef, Input, NgZone, ViewChild } from '@angular/core';
import { Hero } from '../../../models/hero';
import { HeroService } from '../../../service/hero.service';

import { MapsAPILoader, MouseEvent } from '@agm/core';

declare var google;

@Component({
  selector: 'app-hero-change-location',
  templateUrl: './hero-change-location.component.html',
  styleUrls: ['./hero-change-location.component.css'],
})
export class HeroChangeLocationComponent extends ModalBase {
  @Input('selectedDataId') heroId: number;
  @Input('selectedDataName') name: string;

  @ViewChild('search')
  public searchElementRef: ElementRef;

  public hero: Hero = new Hero();
  public zoom: number = 8;

  constructor(
    private heroService: HeroService,
    private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone
  ) {
    super();
  }

  onChoseLocation($event: MouseEvent): void {
    this.hero.latitude = $event.coords.lat;
    this.hero.longitude = $event.coords.lng;
  }

  public ngOnInit() {
    this._initModal();

    this.mapsAPILoader.load().then(() => {
      let autocomplete = new google.maps.places.Autocomplete(
        this.searchElementRef.nativeElement
      );
      autocomplete.addListener('place_changed', () => {
        this.ngZone.run(() => {
          let place: google.maps.places.PlaceResult = autocomplete.getPlace();

          if (!place.geometry) {
            return;
          }

          this.hero.latitude = place.geometry.location.lat();
          this.hero.longitude = place.geometry.location.lng();
          this.zoom = 12;
        });
      });
    });
  }

  public onSave() {
    this.heroService
      .changeHeroLocation(this.hero.id, this.hero.latitude, this.hero.longitude)
      .subscribe(() => {
        this.close.emit(true);
        this.hero = new Hero();
      });
  }

  private _initModal(): void {
    this.heroService.getHero(this.heroId).subscribe((hero) => {
      this.hero = hero;
    });
  }

  markerDragEnd($event: MouseEvent) {
    this.hero.latitude = $event.coords.lat;
    this.hero.longitude = $event.coords.lng;
  }
}
