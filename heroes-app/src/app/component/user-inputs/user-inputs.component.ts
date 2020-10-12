import { AlertService } from './../../service/alert.service';
import { HeroService } from 'src/app/service/hero.service';
import { Hero } from 'src/app/models/hero';
import { Component, Input, OnInit } from '@angular/core';
import { Constants } from 'src/app/utils/constants';
import { FileService } from 'src/app/service/file.service';

@Component({
  selector: 'app-user-inputs',
  templateUrl: './user-inputs.component.html',
  styleUrls: ['./user-inputs.component.css'],
})
export class UserInputsComponent implements OnInit {
  @Input() hero: Hero;
  public initialImgPath: string;
  public initialHero: Hero;
  public showEditButtons: boolean = false;

  constructor(
    private fileService: FileService,
    private heroService: HeroService,
    private alertService: AlertService
  ) {}

  ngOnInit(): void { }

  public saveDetails() {
    this.heroService
      .changeDetails(this.hero)
      .subscribe(() => this.alertService.alertSuccess('Details were updated'));
  }

  public cancel() {
    this.hero = this.initialHero;
    let dbPath = this.initialImgPath?.substr(Constants.APP_URL.length);
    this.heroService.changeHeroAvatar(this.hero.id, dbPath).subscribe();
    this.showEditButtons = false;
  }

  public uploadFile(files: any, _: any) {
    this.initialImgPath = this.hero.avatarPath;
    
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append(Constants.FILE_PREFIX, fileToUpload, fileToUpload.name);

    this.fileService
      .getHeroAvatarPath(formData, this.hero.id)
      .subscribe((imagePath) => {
        let dbPath = imagePath.result?.substr(Constants.APP_URL.length);
        this.hero.avatarPath = Constants.APP_URL + dbPath;
        this.heroService
          .changeHeroAvatar(this.hero.id, dbPath)
          .subscribe();
      });
  }

  enterEditMode() {
    this.initialHero = JSON.parse(JSON.stringify(this.hero));
    this.showEditButtons = true;
  }
}
