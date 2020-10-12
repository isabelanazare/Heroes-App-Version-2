import { EditHeropowerModalComponent } from './../component/modals/edit-heropower-modal/edit-heropower-modal.component';
import { VillainAddPowersComponent } from './../component/modals/villain-add-powers/villain-add-powers.component';
import { DeleteHeroPowerRendererComponent } from './../component/renderers/delete-hero-power-renderer/delete-hero-power-renderer.component';
import { VillainPowerTableComponent } from './../component/villain-power-table/villain-power-table.component';
import { ChangeLocationRendererComponent } from './../component/renderers/change-location-renderer/change-location-renderer.component';
import { GoogleMapsModule } from '@angular/google-maps';
import { HeroChangeLocationComponent } from './../component/modals/hero-change-location/hero-change-location.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from '../layout/layout.component';
import { HomeComponent } from '../component/home/home.component';
import { HeroesComponent } from '../component/heroes/heroes.component';
import { PowersComponent } from '../component/powers/powers.component';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SharedLayoutsModule } from './shared-layouts.module';
import { ChartModule } from './chart.module';
import { AgGridModule } from 'ag-grid-angular';
import { AddHeroModalComponent } from '../component/modals/add-hero-modal/add-hero-modal.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MultiSelectAllModule } from '@syncfusion/ej2-angular-dropdowns';
import { EditHeroModalComponent } from '../component/modals/edit-hero-modal/edit-hero-modal.component';
import { AddPowerModalComponent } from '../component/modals/add-power-modal/add-power-modal.component';
import { EditPowerModalComponent } from '../component/modals/edit-power-modal/edit-power-modal.component';
import { LoginComponent } from '../component/login/login.component';
import { RegisterModalComponent } from '../component/modals/register-modal/register-modal.component';
import { AccountValidationComponent } from '../component/account-validation/account-validation.component';
import { ResetPasswordModalComponent } from '../component/modals/reset-password-modal/reset-password-modal.component';
import { PageNotFoundComponent } from '../component/page-not-found/page-not-found.component';
import { UnauthorisedPageComponent } from '../component/unauthorised-page/unauthorised-page.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from 'src/app/helpers/jwt-interceptor';
import { ErrorInterceptor } from 'src/app/helpers/error-interceptor';
import { HeroPictureRendererComponent } from '../component/renderers/hero-picture-renderer/hero-picture-renderer.component';
import { DeletePowerButtonRendererComponent } from '../component/renderers/delete-power-button-renderer/delete-power-button-renderer.component';
import { DeleteHeroButtonRendererComponent } from '../component/renderers/delete-hero-button-renderer/delete-hero-button-renderer.component';
import { DpDatePickerModule } from 'ng2-date-picker';
import { DatePickerRendererComponent } from '../component/renderers/date-picker-renderer/date-picker-renderer.component';
import { AddVillainModalComponent } from '../component/modals/add-villain-modal/add-villain-modal.component';
import { VillainDetailComponent } from '../component/villain-detail/villain-detail.component';
import { VillainsComponent } from '../component/villains/villains.component';
import { VillainImageRendererComponent } from '../component/renderers/villain-image-renderer/villain-image-renderer.component';
import { DeleteVillainRendererComponent } from '../component/renderers/delete-villain-renderer/delete-villain-renderer.component';
import { BrowserModule } from '@angular/platform-browser';
import { AgmCoreModule } from '@agm/core';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AdminMapComponent } from '../component/admin-map/admin-map.component';
import { AgmSnazzyInfoWindowModule } from '@agm/snazzy-info-window';
import { DetailCalendarComponent } from '../component/detail-calendar/detail-calendar.component';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { AgmJsMarkerClustererModule } from '@agm/js-marker-clusterer';

@NgModule({
  declarations: [
    LayoutComponent,
    LoginComponent,
    HomeComponent,
    HeroesComponent,
    PowersComponent,
    AddHeroModalComponent,
    EditHeroModalComponent,
    AddPowerModalComponent,
    EditPowerModalComponent,
    RegisterModalComponent,
    AccountValidationComponent,
    ResetPasswordModalComponent,
    PageNotFoundComponent,
    UnauthorisedPageComponent,
    HeroPictureRendererComponent,
    DeletePowerButtonRendererComponent,
    DeleteHeroButtonRendererComponent,
    DatePickerRendererComponent,
    AddVillainModalComponent,
    VillainDetailComponent,
    VillainsComponent,
    VillainImageRendererComponent,
    DeleteVillainRendererComponent,
    HeroChangeLocationComponent,
    ChangeLocationRendererComponent,
    AdminMapComponent,
    VillainPowerTableComponent,
    DeleteHeroPowerRendererComponent,
    VillainAddPowersComponent,
    EditHeropowerModalComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    FlexLayoutModule,
    SharedLayoutsModule,
    ChartModule,
    AgGridModule.withComponents([
      HeroPictureRendererComponent,
      DeletePowerButtonRendererComponent,
      DeleteHeroButtonRendererComponent,
      DatePickerRendererComponent,
      ChangeLocationRendererComponent,
      DeleteHeroPowerRendererComponent,
    ]),
    ModalModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    MultiSelectAllModule,
    DpDatePickerModule,
    BrowserModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyApBHHxQnpPUNZzFSYvI0PFYrub5lzdqJg',
      libraries: ['places', 'geometry'],
    }),

    BrowserAnimationsModule,

    GoogleMapsModule,
    MatTabsModule,
    BrowserAnimationsModule,
    NoopAnimationsModule,
    NgbModule,
    AgmSnazzyInfoWindowModule,
    AgmJsMarkerClustererModule,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
})
export class LayoutModule {}
