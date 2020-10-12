import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class AlertService {
  constructor() {}

  public alertSuccess(message: string) {
    Swal.fire({
      icon: 'success',
      title: message,
      showConfirmButton: false,
      timer: 1500,
    });
  }

  public alertError(message: string) {
    Swal.fire({
      icon: 'error',
      title: 'Oops...',
      text: message,
    });
  }

  public alertLoading() {
    let timerInterval;
    Swal.fire({
      title: 'Loading',
      html: 'Please wait',
      timer: 1000,
      timerProgressBar: true,
      onBeforeOpen: () => {
        Swal.showLoading();
        timerInterval = setInterval(() => {
          const content = Swal.getContent();
          if (content) {
            const b = content.querySelector('b');
            if (b) {
              b.textContent = Swal.getTimerLeft().toString();
            }
          }
        }, 100);
      },
      onClose: () => {
        clearInterval(timerInterval);
      },
    }).then((result) => {
      if (result.dismiss === Swal.DismissReason.timer) {
        console.log('I was closed by the timer');
      }
    });
  }

  public alertConfirm() {
    return Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    });
  }

  public alertShowTimer() {
    let timerInterval;
    Swal.fire({
      title: 'Loading',
      html: 'Please wait',
      timer: 1000,
      timerProgressBar: true,
      onBeforeOpen: () => {
        Swal.showLoading();
        timerInterval = setInterval(() => {
          const content = Swal.getContent();
          if (content) {
            const b = content.querySelector('b');
            if (b) {
              b.textContent = Swal.getTimerLeft().toString();
            }
          }
        }, 100);
      },
      onClose: () => {
        clearInterval(timerInterval);
      },
    }).then((result) => {
      if (result.dismiss === Swal.DismissReason.timer) {
        console.log('I was closed by the timer');
      }
    });
  }

  public alertWon(message, text) {
    return Swal.fire({
      imageUrl: '../../../assets/image/heroes-won.gif',
      imageHeight: 300,
      imageAlt: 'Winning image',
      title: message,
      html: text,
    });
  }

  public alertLost(message, text) {
    return Swal.fire({
      imageUrl: '../../../assets/image/game-over.gif',
      imageHeight: 300,
      imageAlt: 'Losing image',
      title: message,
      html: text,
    });
  }

  public alertEqualScore(message, text) {
    return Swal.fire({
      imageUrl: '../../../assets/image/tie.gif',
      imageHeight: 200,
      imageAlt: 'Equal score image',
      title: message,
      html: text,
    });
  }

  public alertBattleStarted() {
    Swal.fire({
      title: 'Battle has started. Wait for the result!',
      imageUrl: '../../../assets/image/battle-started.gif',
      imageWidth: 500,
      imageHeight: 250,
      showConfirmButton: false
    });
  }

  public alertBattleFinished() {
    return Swal.fire({
      title: 'Battle has finished! The result will appear soon',
      imageUrl: '../../../assets/image/battle-finished.gif',
      imageWidth: 500,
      imageHeight: 250,
      timer: 4000,
      showConfirmButton: false,
    });
  }

  public alertNotification(count: number) {
    Swal.fire({
      icon: 'warning',
      title: `You participated in ${count} match(es)`,
      showConfirmButton: true,
      timer: 10000,
    });
  }
}
