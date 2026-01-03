import { inject, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Store } from '@ngxs/store';
import { UpdateLastMessageAction } from '../actions/main.actions';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;

  private readonly rootUrl: string = 'https://localhost:7293/hub';

  private readonly store: Store = inject(Store);

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.rootUrl, { skipNegotiation: false, transport: signalR.HttpTransportType.WebSockets })
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR Connection started'))
      .catch(err => console.log('Error establishing SignalR connection: ' + err));
  }

  public addMessageListener = () => {
    this.hubConnection.on('notification', (content: string) => {
      console.log('Message received: ' + content);
      this.store.dispatch(new UpdateLastMessageAction({ message: content }));
    });
  }

  public handleDisconnects = () => {
    this.hubConnection.onclose(() => {
      console.log('Connection lost. Attempting to reconnect...');
      setTimeout(() => this.startConnection(), 3000);  // Try reconnecting after 3 seconds
    });
  }
}