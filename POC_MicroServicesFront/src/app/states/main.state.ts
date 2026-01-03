import { Inject, Injectable } from "@angular/core";
import { Action, Selector, State, StateContext } from "@ngxs/store";
import { LoginAction, RegisterAction, SendMailAction, UpdateLastMessageAction } from "../actions/main.actions";
import { MainService } from "../services/main-service";
import { retry, tap } from "rxjs";

export class MainStateModel {
    username?: string;
    email?: string;
    token?: string;
    mailTo?: string;
    mailContent?: string;
    lastMessage: string = '';
}

@State<MainStateModel>({
    name: 'main',
    defaults: {
        lastMessage: ''
    }
})
@Injectable()
export class MainState {
    constructor(private service: MainService) { }

    @Selector()
    static lastMessage(state: MainStateModel) {
        return state.lastMessage;
    }

    @Selector()
    static token(state: MainStateModel) {
        return state.token;
    }

    @Action(RegisterAction)
    register({ patchState }: StateContext<MainStateModel>, action: RegisterAction) {
        const p = action.payload;

        return this.service.register(p.username, p.password, p.email).pipe(
            tap(x => {
                patchState({
                    lastMessage: "User recorded"
                })
            })
        )
    }

    @Action(LoginAction)
    login({ patchState }: StateContext<MainStateModel>, action: LoginAction) {
        const p = action.payload;

        return this.service.login(p.username, p.password).pipe(
            tap(x => {
                patchState({
                    token: x.token,
                    lastMessage: "Token loaded " + x.token
                })
            })
        );
    }

    @Action(SendMailAction)
    sendMail({ patchState }: StateContext<MainStateModel>, action: SendMailAction) {
        const p = action.payload;

        return this.service.sendMail(p.to, p.body).pipe(
            tap(x => {
                console.log(x);
                patchState({
                    lastMessage: 'Mail sent successfully',
                });
            })
        );
    }

    @Action(UpdateLastMessageAction)
    updateLastMessage({ patchState }: StateContext<MainStateModel>, action: UpdateLastMessageAction) {
        const p = action.payload;

        patchState({
            lastMessage: p.message
        });
    }
}
