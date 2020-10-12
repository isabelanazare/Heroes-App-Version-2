import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';
import { DataManagementBase } from '../utils/select-row';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';

export abstract class ChildRendererBase
  extends DataManagementBase
  implements ICellRendererAngularComp {
  public params: any;
  public selectedId: number;

  constructor(
    protected router: Router,
    protected modalService: BsModalService
  ) {
    super(modalService, router);
  }

  public refresh(_: any): boolean {
    return true;
  }

  public agInit(params: ICellRendererParams): void {
    this.params = params;
  }

  public getSelectedId() {
    if (this.params.onClick instanceof Function) {
      this.selectedId = this.params.node.data.id;
    }
  }
}
