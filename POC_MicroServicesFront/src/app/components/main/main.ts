import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AsyncPipe, NgIf } from '@angular/common';
import { Observable } from 'rxjs';
import { MainState } from '../../states/main.state';
import { select, Select, Store } from '@ngxs/store';
import { LoginAction, RegisterAction, SendMailAction } from '../../actions/main.actions';
import { SignalRService } from '../../services/signal-rservice';

@Component({
  selector: 'app-main',
  imports: [FormsModule, AsyncPipe],
  templateUrl: './main.html',
  styleUrl: './main.scss',
})
export class Main implements OnInit {
  private store: Store = inject(Store);
  private signalRService: SignalRService = inject(SignalRService);

  ngOnInit(): void {
    this.signalRService.startConnection();
    this.signalRService.addMessageListener();
  }

  registerName: any;
  registerPwd: any;
  registerEmail: any;
  loginName: any;
  loginPwd: any;
  sendMsg: any;
  sendTo: any;

  lastMessage$: Observable<string> = this.store.select(MainState.lastMessage);


  register() {
    this.store.dispatch(new RegisterAction({
      username: this.registerName,
      password: this.registerPwd,
      email: this.registerEmail
    }));
  }
  login() {
    this.store.dispatch(new LoginAction({
      username: this.loginName,
      password: this.loginPwd
    }));
  }
  send() {
    this.store.dispatch(new SendMailAction({
      to: this.sendTo,
      body: this.sendMsg
    }));
  }
}
