import { UserData } from 'src/app/models/user-data';
import { Constants } from 'src/app/utils/constants';
import { FileService } from '../service/file.service';
import { ModalBase } from 'src/app/modals/modal-base';
import { AuthenticationService } from '../service/authentication.service';

export abstract class UserProfileBase extends ModalBase {
  public progress: number;
  public message: string = Constants.EMPTY_STRING;
  public newUser: UserData = new UserData();
  public userAvatar: string = Constants.EMPTY_STRING;

  constructor(
    protected fileService: FileService,
    protected authenticationService: AuthenticationService
  ) {
    super();
    this.authenticationService.currentUser.subscribe((u) => {
      this.newUser = u;
      this.userAvatar = u.avatarPath;
    });
  }

  public uploadFile(files: any) {
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const formData = new FormData();

    formData.append(Constants.FILE_PREFIX, fileToUpload, fileToUpload.name);
    this.fileService
      .getUserAvatarPath(formData, this.newUser.id)
      .subscribe((imagePath) => {
        this._getUploadStatus(imagePath);
      });
  }

  private _getUploadStatus(image: any) {
    if (image?.isCompletedSuccessfully === true) {
      this.message = Constants.SUCCESS_UPLOAD_MESSAGE;
      this.userAvatar = image.result;
    } else {
      this.message = Constants.ERROR_UPLOAD_MESSAGE;
    }
  }
}
