import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HttpClient } from '@angular/common/http';
import { Message } from '../models/message';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private connection: any = new signalR.HubConnectionBuilder().withUrl("https://localhost:44324/chatsocket").build();

  readonly POST_URL = "https://localhost:44324/api/chat/send";

  private receivedMessageObject: Message = new Message();
  private sharedObj = new Subject<Message>();

  constructor(private http: HttpClient) {
    this.connection.onclose(async () => {
      await this.start();
    });
    
    this.connection.on("ReceiveOne", (user, message, avatarPath, hour) => { this.mapReceivedMessage(user, message, avatarPath, hour); });
    this.start();
  }

  public async start() {
    try {
      await this.connection.start();
    } catch (err) {
      setTimeout(() => this.start(), 5000);
    }
  }

  private mapReceivedMessage(user: string, message: string, avatarPath: string, hour: string): void {
    this.receivedMessageObject.user = user;
    this.receivedMessageObject.text = message;
    this.receivedMessageObject.avatarPath = avatarPath;
    this.receivedMessageObject.hour = hour;
    this.sharedObj.next(this.receivedMessageObject);
  }

  public broadcastMessage(message: any) {
    this.http.post(this.POST_URL, message).subscribe(data => console.log(data));
  }

  public retrieveMappedObject(): Observable<Message> {
    return this.sharedObj.asObservable();
  }
}
