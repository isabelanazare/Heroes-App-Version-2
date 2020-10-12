import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { Villain } from '../models/villain';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private villainSource = new BehaviorSubject<Villain>(null);
  currentVillain = this.villainSource.asObservable();

  private isUpdatedSource = new BehaviorSubject<boolean>(null);
  currentIsUpdated = this.isUpdatedSource.asObservable();

  constructor() { }

  changeCurrentVillain(villain: Villain) {
    this.villainSource.next(villain);
  }

  changeCurrentIsUpdated(value: boolean) {
    this.isUpdatedSource.next(value);
  }
}