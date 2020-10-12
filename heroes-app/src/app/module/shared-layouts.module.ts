import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderLayoutComponent } from '../component/header/header.component';
import { SidebarComponent } from '../component/sidebar/sidebar.component';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { UserProfileModalComponent } from '../component/modals/user-profile-modal/user-profile-modal.component';
import { AgGridModule } from 'ag-grid-angular';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    HeaderLayoutComponent,
    SidebarComponent,
    UserProfileModalComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    FlexLayoutModule,
    AgGridModule.withComponents([]),
    ModalModule.forRoot(),
    FormsModule,
  ],
  exports: [
    HeaderLayoutComponent,
    SidebarComponent
  ]
})
export class SharedLayoutsModule { }
