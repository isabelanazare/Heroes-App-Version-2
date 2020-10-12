import { ChildRendererBase } from './ChildRendererBase';
import { HeroService } from '../service/hero.service';
import { PowerService } from '../service/power.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import Swal from 'sweetalert2';
import { Constants } from './constants';
import { Router } from '@angular/router';

export abstract class DeleteRendererBase extends ChildRendererBase {

  constructor(
    protected router: Router,
    protected heroService: HeroService,
    protected powerService: PowerService,
    protected modalService: BsModalService) {
    super(router, modalService);
  }

  public deleteRow(event: any) {
    this._displayWarningMessage(event);
  }
  
  protected abstract _delete(event: any);

  private _displayWarningMessage(event: any) {
    Swal.fire({
      icon: 'warning',
      title: Constants.WARNING_TITLE_DELETE,
      text: Constants.WARNING_MESSAGE_DELETE,
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: Constants.WARNING_cONFIRM_BTN_DELETE

    }).then((result) => {
      if (result.value) {
        this._delete(event);
        this.displayDeleteResult();
      }
    })
  }
}
