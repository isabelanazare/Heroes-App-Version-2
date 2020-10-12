import { Component, ElementRef, ViewChild } from '@angular/core';
import { ICellEditorComp } from 'ag-grid-community';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ChildRendererBase } from 'src/app/utils/ChildRendererBase';
import { Constants } from 'src/app/utils/constants';
import { HeroService } from 'src/app/service/hero.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-date-picker-renderer',
  templateUrl: './date-picker-renderer.component.html',
  styleUrls: ['./date-picker-renderer.component.css'],
})
export class DatePickerRendererComponent
  extends ChildRendererBase
  implements ICellEditorComp {
  public datePickerElement: HTMLElement;
  public selectedDate: string = Constants.EMPTY_STRING;

  @ViewChild('datePicker') set playerRef(ref: ElementRef<HTMLElement>) {
    this.datePickerElement = ref.nativeElement;
  }

  constructor(
    protected router: Router,
    protected modalService: BsModalService,
    private heroService: HeroService
  ) {
    super(router, modalService);
  }

  public getValue() {
    this.selectedId = this.params.data.id;
    if (this.selectedDate) {
      this.heroService
        .updateHeroBirthday(this.selectedId, this.selectedDate.toString())
        .subscribe();
      return this.selectedDate;
    }
    return this.params.data.birthday;
  }

  public isPopup?(): boolean {
    return true;
  }

  public getPopupPosition?(): string {
    return Constants.POPUP_POSITION;
  }

  public getGui(): HTMLElement {
    return this.datePickerElement;
  }

  public getSelectedDate(event: any) {
    this.selectedDate = event.format(Constants.DATE_FORMAT);
  }

  protected _loadData() {}
}
