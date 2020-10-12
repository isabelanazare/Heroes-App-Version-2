import Swal from 'sweetalert2';
import { Constants } from './constants';
import { TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Utils } from './utils';
import { Router } from '@angular/router';

export abstract class DataManagementBase extends Utils {
  public selectedDataId: number;
  public selectedDataName: string = Constants.EMPTY_STRING;
  private _isChanged: boolean = true;
  public gridApi;
  protected redirectRoute: string;

  public saveModal: BsModalRef = new BsModalRef();
  public editModal: BsModalRef = new BsModalRef();
  public locationModal: BsModalRef = new BsModalRef();

  protected chosenModal: BsModalRef;

  public isEditMode: boolean = true;

  public isUpdateSelected: boolean = false;

  constructor(
    protected modalService: BsModalService,
    protected router: Router
  ) {
    super();
  }

  public onExport(areHeroes: boolean) {
    const params = {
      columnGroups: true,
      allColumns: true,
      fileName: areHeroes ? Constants.HEROES_EXPORT : Constants.POWERS_EXPORT,
    };
    this.gridApi?.exportDataAsCsv(params);
  }

  public onGridReady(event: any) {
    this.gridApi = event.api;
  }

  private _changeSelectedData(id: number, name: string) {
    this.selectedDataId = id;
    this.selectedDataName = name;
  }

  public onRowSelected(event: any) {
    if (this._isChanged) {
      this._changeSelectedData(event.data.id, event.data.name);
      this._isChanged = false;
    }
  }
  
  public redirectToVillainDetails(id, name) {
    if (this._isChanged) {
      this.isUpdateSelected = true;
      this._changeSelectedData(id, name);
      this._isChanged = false;
      this.router.navigate(['home/villains', id]);
    }
  }

  public onRowDoubleClicked(event: any) {
    if (this._isChanged) {
      this.isUpdateSelected = true;
      this._changeSelectedData(event.data.id, event.data.name);
      this._isChanged = false;
      this.router.navigate(['home/villains', event.data.id]);
    }
  }

  public onSelectionChanged(_: any) {
    this._isChanged = true;
  }

  public onCheckboxChanged(event: any) {
    this.isEditMode = !event.selected;
  }

  public displayDeleteResult() {
    Swal.fire('Deleted!', 'Your record has been deleted.', 'success');
  }

  public showModal(selectedModal: TemplateRef<any>) {
    this.chosenModal = this.modalService.show(
      selectedModal,
      this.getModalConfig(Constants.CSS_MODAL_CLASS)
    );
  }

  public closeModal(shouldReloadData: boolean) {
    if (shouldReloadData) {
      this._loadData();
    }
    this.chosenModal.hide();
  }

  protected abstract _loadData();
}
