import { Component } from '@angular/core';
import { ICellRendererAngularComp } from "@ag-grid-community/angular";
import { Constants } from 'src/app/utils/constants';
import { FileService } from 'src/app/service/file.service';
import { VillainService } from 'src/app/service/villain.service';
import { Router } from '@angular/router';

@Component({
  selector: 'villain-image-cell',
  templateUrl: './villain-image-renderer.component.html',
  styleUrls: ['./villain-image-renderer.component.css']
})

export class VillainImageRendererComponent implements ICellRendererAngularComp {
  private params: any;
  public cell: any;

  constructor(
    private villainService: VillainService,
    private fileService: FileService
  ) { }

  agInit(params: any): void {
    this.params = params;
    this.cell = { row: params.value, col: params.colDef.headerName };
  }

  public refresh(): boolean {
    return false;
  }

  public getImagePath(): string {
    return `${this.params.data.avatarPath}`;
  }

  public uploadFile(files: any, _: any) {
    let id = this.params.data.id;

    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append(Constants.FILE_PREFIX, fileToUpload, fileToUpload.name);

    this.fileService
      .getHeroAvatarPath(formData, id)
      .subscribe((imagePath) => {
        let dbPath = imagePath.result?.substr(Constants.APP_URL.length);
        this.villainService
          .changeVillainImage(id, dbPath)
          .subscribe(() => this.params.onClick(this.params));
      });
  }
}