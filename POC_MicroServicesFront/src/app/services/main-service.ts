import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { MainState } from '../states/main.state';

@Injectable({
  providedIn: 'root',
})
export class MainService {
  private readonly rootUrl: string = 'https://localhost:7116/';

  private readonly store: Store = inject(Store);

  constructor(private http: HttpClient) {
  }

  register(username: string, password: string, email: string): Observable<any> {
    console.log(`${this.rootUrl}api/register`)
    return this.http.post(`${this.rootUrl}auth/Register`, { username, password, email });
  }

  login(username: string, password: string): Observable<any> {
    return this.http.post(`${this.rootUrl}auth/Login`, { username, password });
  }

  sendMail(to: string, content: string): Observable<any> {
    const token: string = this.store.selectSnapshot(MainState.token) || "";

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    console.log(`${this.rootUrl}form`)
    return this.http.post(`${this.rootUrl}form`, { to, content }, {headers: headers});
  }
}
