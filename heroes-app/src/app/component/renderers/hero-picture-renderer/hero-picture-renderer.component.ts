import { Component } from '@angular/core';
import { Constants } from 'src/app/utils/constants';
import { FileService } from 'src/app/service/file.service';
import { HeroService } from 'src/app/service/hero.service';
import { ChildRendererBase } from '../../../utils/ChildRendererBase';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';

@Component({
  selector: 'app-hero-picture-renderer',
  templateUrl: './hero-picture-renderer.component.html',
  styleUrls: ['./hero-picture-renderer.component.css'],
})
export class HeroPictureRendererComponent extends ChildRendererBase {
  constructor(
    protected router: Router,
    private fileService: FileService,
    private heroService: HeroService,
    protected modalService: BsModalService
  ) {
    super(router, modalService);
  }

  public uploadFile(files: any, _: any) {
    this.getSelectedId();

    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append(Constants.FILE_PREFIX, fileToUpload, fileToUpload.name);

    this.fileService
      .getHeroAvatarPath(formData, this.selectedId)
      .subscribe((imagePath) => {
        let dbPath = imagePath.result?.substr(Constants.APP_URL.length);
        this.heroService
          .changeHeroAvatar(this.selectedId, dbPath)
          .subscribe(() => this.params.onClick(this.params));
      });
  }

  protected _loadData() {}
}
