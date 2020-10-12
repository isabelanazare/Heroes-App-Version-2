import { UserDetailsComponent } from './component/user-details/user-details.component';
import { UserRole } from './models/user-role';
import { RoleGuard } from './helpers/role-guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { HomeComponent } from './component/home/home.component';
import { HeroesComponent } from './component/heroes/heroes.component';
import { PowersComponent } from './component/powers/powers.component';
import { LoginComponent } from './component/login/login.component';
import { AccountValidationComponent } from './component/account-validation/account-validation.component';
import { AuthGuard } from '../app/helpers/auth-guard';
import { PageNotFoundComponent } from './component/page-not-found/page-not-found.component';
import { UnauthorisedPageComponent } from './component/unauthorised-page/unauthorised-page.component';
import { VillainsComponent } from './component/villains/villains.component';
import { VillainDetailComponent } from './component/villain-detail/villain-detail.component';
import { AdminMapComponent } from './component/admin-map/admin-map.component';
import { DetailCalendarComponent } from './component/detail-calendar/detail-calendar.component';
import { HeroMapComponent } from './component/hero-map/hero-map.component';
import { ChatComponent } from './component/chat/chat.component';

const routes: Routes = [
  {
    path: 'home',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', component: HomeComponent },
      {
        path: 'user-details',
        component: UserDetailsComponent,
        canActivate: [RoleGuard],
        data: { expectedRole: UserRole.Regular },
      },
      {
        path: 'heroes',
        component: HeroesComponent,
        canActivate: [RoleGuard],
        data: {
          expectedRole: UserRole.Admin,
        },
      },
      {
        path: 'powers',
        component: PowersComponent,
        canActivate: [RoleGuard],
        data: {
          expectedRole: UserRole.Admin,
        },
      },
      {
        path: 'admin-map',
        component: AdminMapComponent,
        canActivate: [RoleGuard],
        data: {
          expectedRole: UserRole.Admin,
        },
      },
      {
        path: 'villains',
        component: VillainsComponent,
        canActivate: [RoleGuard],
        data: { expectedRole: UserRole.Admin },
        children: [{ path: ':id', component: VillainDetailComponent }],
      },
      {
        path: 'chat',
        component: ChatComponent,
        canActivate: [RoleGuard],
        data: { expectedRole: UserRole.Regular }
      }
    ],
  },
  {
    path: '',
    component: LoginComponent,
  },
  {
    path: 'account-activation',
    component: AccountValidationComponent,
  },
  { path: 'unauthorised', component: UnauthorisedPageComponent },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
