import { Component, OnInit } from '@angular/core';
import { Hero } from 'src/app/models/hero';
import { UserData } from 'src/app/models/user-data';
import { AlertService } from 'src/app/service/alert.service';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { HeroService } from 'src/app/service/hero.service';
import { Constants } from 'src/app/utils/constants';
import { Message } from '../../models/message'
import { ChatService } from '../../service/chat.service'

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {
  public hero: Hero;
  public user: UserData;
  public message: Message = new Message();
  public messages: Message[] = [];

  constructor(
    private alertService: AlertService,
    private heroService: HeroService,
    private authenticationService: AuthenticationService,
    private chatService: ChatService) { }

  ngOnInit(): void {
    this.chatService.retrieveMappedObject().subscribe((receivedObj: Message) => {
      this._addNewMessage(receivedObj);
    });

    this.authenticationService.currentUser.subscribe((user) => {
      this.user = user;
      this.heroService.getHeroByUserId(this.user.id).subscribe((hero) => {
        this.hero = hero;
      });
    });
  }

  public send(): void {
    if (this.message) {
      this.message.user = this.hero.name;
      this.message.avatarPath = this.user.avatarPath;
      this.message.hour = this._getCurrentHour();

      if (this.message.text.length == 0) {
        this.alertService.alertError("Please fill the message box");
      } else {
        this.chatService.broadcastMessage(this.message);
        this.message.text = Constants.EMPTY_STRING;
      }
    }
  }

  private _addNewMessage(obj: Message): void {
    let newMessage = new Message();
    newMessage.user = obj.user;
    newMessage.text = obj.text;
    newMessage.avatarPath = obj.avatarPath;
    newMessage.hour = obj.hour;

    this.messages.push(newMessage);
  }

  private _addZero(i) {
    if (i < 10) {
      i = "0" + i;
    }
    return i;
  }

  private _getCurrentHour(): string {
    var d = new Date();
    var h = this._addZero(d.getHours());
    var m = this._addZero(d.getMinutes());
    return h + ":" + m;
  }
}
