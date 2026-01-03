export class RegisterAction {
    static readonly type = '[Auth] Register';
    constructor(public payload: { username: string; password: string; email: string }) {}
}

export class LoginAction {
    static readonly type = '[Auth] Login';
    constructor(public payload: { username: string; password: string }) {}
}

export class SendMailAction {
    static readonly type = '[Mail] Send Mail';
    constructor(public payload: { to: string; body: string }) {}
}

export class UpdateLastMessageAction {
    static readonly type = '[Message] Update Last Message';
    constructor(public payload: { message: string }) {}
}