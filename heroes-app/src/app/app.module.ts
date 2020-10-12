import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { JwtInterceptor } from '../app/helpers/jwt-interceptor';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedLayoutsModule } from './module/shared-layouts.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LayoutModule } from './module/layout.module';
import { ChartsModule } from 'ng2-charts';
import { AgGridModule } from 'ag-grid-angular';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ErrorInterceptor } from 'src/app/helpers/error-interceptor';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgmCoreModule } from '@agm/core';
import { AgmJsMarkerClustererModule } from '@agm/js-marker-clusterer';
import { AgmSnazzyInfoWindowModule } from '@agm/snazzy-info-window';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UserDetailsComponent } from './component/user-details/user-details.component';
import { UserPowersTableComponent } from './component/user-powers-table/user-powers-table.component';
import { UserInputsComponent } from './component/user-inputs/user-inputs.component';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { DetailCalendarComponent } from './component/detail-calendar/detail-calendar.component';
import { TrainingButtonRendererComponent } from './component/renderers/training-button-renderer/training-button-renderer.component';
import { MainPowerCheckRendererComponent } from './component/renderers/main-power-check-renderer/main-power-check-renderer.component';
import { HeroMapComponent } from 'src/app/component/hero-map/hero-map.component';
import { BattleHistoryComponent } from './component/battle-history/battle-history.component';
import { ChatComponent } from './component/chat/chat.component';
import { HeroBadgesComponent } from './component/hero-badges/hero-badges.component';

@NgModule({
  declarations: [
    AppComponent,
    UserDetailsComponent,
    UserPowersTableComponent,
    UserInputsComponent,
    DetailCalendarComponent,
    TrainingButtonRendererComponent,
    MainPowerCheckRendererComponent,
    HeroMapComponent,
    BattleHistoryComponent,
    ChatComponent,
    HeroBadgesComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AgGridModule.withComponents([]),
    ChartsModule,
    LayoutModule,
    SharedLayoutsModule,
    ModalModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyApBHHxQnpPUNZzFSYvI0PFYrub5lzdqJg',
      libraries: ['places'],
    }),
    AgmSnazzyInfoWindowModule,
    AgmJsMarkerClustererModule,
    NgbModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent],
  exports: [UserDetailsComponent],
})
export class AppModule {}
