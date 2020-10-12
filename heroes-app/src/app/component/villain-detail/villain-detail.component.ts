import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  Input,
  NgZone,
  OnInit,
  ViewChild,
} from '@angular/core';
import { DataManagementBase } from 'src/app/utils/select-row';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { VillainService } from 'src/app/service/villain.service';
import { SharedService } from 'src/app/service/shared.service';
import { Villain } from 'src/app/models/villain';
import { Constants } from 'src/app/utils/constants';
import { Power } from 'src/app/models/power';
import { HeroType } from 'src/app/models/hero-type';
import { forkJoin } from 'rxjs';
import { HeroTypeService } from 'src/app/service/hero-type.service';
import { PowerService } from 'src/app/service/power.service';
import { ErrorHandlerService } from 'src/app/service/error-handler.service';
import { Utils } from 'src/app/utils/utils';
import { PowerRowData } from 'src/app/models/power-row-data';
import { AlertService } from 'src/app/service/alert.service';
import { switchMap } from 'rxjs/operators';
import { MapsAPILoader, MouseEvent } from '@agm/core';

@Component({
  selector: 'app-villain-detail',
  templateUrl: './villain-detail.component.html',
  styleUrls: ['./villain-detail.component.css'],
})
export class VillainDetailComponent
  extends DataManagementBase
  implements OnInit {
  @Input() villain: Villain;

  @ViewChild('search')
  public searchElementRef: ElementRef;

  public zoom: number = 8;

  public villainId: number;

  public heroTypes: HeroType[] = [];
  public powers: Power[] = [];
  public powersRowData: PowerRowData[] = [];

  public localFieldsPowers: any = { text: 'name', value: 'id' };
  public localFieldsAllies: any = { text: 'name', value: 'id' };

  public placeholderPowers: string = Constants.POWERS_MULTISELECT_PLACEHOLDER;
  public placeholderAllies: string = Constants.ALLIES_MULTISELECT_PLACEHLODER;

  public allies: Villain[] = [];
  public powersIds: number[] = [];
  public alliesIds: number[] = [];

  public selectionHint: string = Constants.HERO_SELECTION_HINT;

  public initialHeroType: HeroType = new HeroType();
  public initialMainPower: Power = new Power(0);

  constructor(
    private alertService: AlertService,
    private heroTypeService: HeroTypeService,
    private powerService: PowerService,
    protected router: Router,
    private villainService: VillainService,
    protected modalService: BsModalService,
    private errorHandler: ErrorHandlerService,
    private route: ActivatedRoute,
    private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone
  ) {
    super(modalService, router);
  }

  public ngOnInit() {
    this._initializeAutocomplete();
    this.route.params.subscribe(
      (params: Params) => (this.villainId = +params['id'])
    );
    this._loadData();
  }

  private _initializeAutocomplete() {
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

          this.villain.latitude = place.geometry.location.lat();
          this.villain.longitude = place.geometry.location.lng();
          this.zoom = 12;
        });
      });
    });
  }

  protected _loadData() {
    this.startLoading();
    this.villainService.getVillainById(this.villainId).subscribe((villain) => {
      this.villain = villain;
      forkJoin([
        this.heroTypeService.getTypes(),
        this.powerService.getPowers(),
        this.villainService.getVillains(),
        this.powerService.getPowersDataTable(),
      ]).subscribe(([types, powers, allies, powersRowData]) => {
        this.heroTypes = types;
        this.powers = powers;
        this.allies = allies;
        this.powersRowData = powersRowData;
        this.initialHeroType = new HeroType(
          this.villain.typeId,
          this.villain.type
        );
        this.heroTypes = this.heroTypes.filter(
          (type) => type.id !== this.initialHeroType.id
        );
        if (!this.villain.mainPower) {
          this.villain.mainPower = new Power(0);
        }
        this.alliesIds = this.villain.allies?.map((ally) => ally.id);
        this.initialMainPower = new Power(this.villain.mainPower.id);
        this.villain.birthday = new Date(this.villain.birthday)
          .toISOString()
          .substring(0, 10);
        this.villain.avatarPath = Constants.APP_URL + this.villain.avatarPath;
        this.stopLoading();
      });
    });
  }

  public onSave() {
    this.convertAllies(this.villain);
    this._checkAndEditVillain();
  }

  public convertPowers(villain: Villain) {
    if (this.powersIds.length == 0) {
      this.villain.mainPower = new Power(0);
      this.villain.powers = [];
      return;
    }
    this.powersIds = this.powersIds?.filter(
      (powerId) => +powerId !== +this.villain.mainPower.id
    );
    villain.powers = this.powersIds?.map((powerId) => {
      return new Power(powerId);
    });
  }

  public convertAllies(villain: Villain) {
    villain.allies = this.alliesIds?.map((allyId) => {
      return new Villain(allyId);
    });
  }

  public getSelectedType(selectedValue: any) {
    this.villain.typeId = selectedValue.target.value;
  }

  public getPowerWithId(id: number): Power {
    for (var power of this.powers) {
      if (power.id === id) {
        return power;
      }
    }
    return new Power();
  }

  public getSelectedMainPower(selectedValue: any) {
    this.villain.mainPower = new Power(selectedValue.target.value);
  }

  private _checkAndEditVillain() {
    if (!this.errorHandler.checkVillainFields(this.villain)) {
      this.errorHandler.displayInvalidParameterError(
        Constants.INVALID_PARAMETER_MESSAGE + this.errorHandler.errorMessage
      );
    } else {
      this.villainService
        .updateVillain(this.villain as Villain)
        .subscribe(() => {
          this.alertService.alertSuccess(Constants.HERO_EDITED);
        });
    }
  }

  public onChoseLocation($event: MouseEvent): void {
    this.villain.latitude = $event.coords.lat;
    this.villain.longitude = $event.coords.lng;
  }

  public markerDragEnd($event: MouseEvent): void {
    this.villain.latitude = $event.coords.lat;
    this.villain.longitude = $event.coords.lng;
  }

  public saveLocation() {
    this.alertService.alertLoading();
    this.villainService
      .changeVillainLocation(
        this.villain.id,
        this.villain.latitude,
        this.villain.longitude
      )
      .subscribe(() => {
        this.villainService
          .getVillainById(this.villain.id)
          .subscribe((villain) => {
            this.villain = villain;
          });
        this.alertService.alertSuccess('Location updated');
      });
  }
}
