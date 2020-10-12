import { Directive, Output, EventEmitter } from '@angular/core';
import { LoadingData } from '../utils/loading-data';

@Directive()
export abstract class ModalBase extends LoadingData {
  @Output() close = new EventEmitter<boolean>();

  public onCancel() {
    this.close.emit(false);
  }

  protected abstract onSave();
}
