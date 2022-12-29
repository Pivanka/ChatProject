import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { USER } from 'src/app/models/authorization.models';
import { GROUP } from 'src/app/models/chat.models';
import { environment } from 'src/environments/environments';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  baseUrl: string = environment.api;
  constructor(private http: HttpClient) { }

  getUsers(): Observable<Array<USER>>{
    const url: string = this.baseUrl + 'api/auth/users/all';
    return this.http.get<Array<USER>>(url);
  }
  getUser(userEmail: string): Observable<USER>{
    const url: string = this.baseUrl + 'api/auth/user/' + userEmail;
    return this.http.get<USER>(url);
  }

  getGroups(): Observable<Array<GROUP>>{
    const url: string = this.baseUrl + 'api/group/groups';
    return this.http.get<Array<GROUP>>(url);
  }
  getOwnGroups(userEmail: string): Observable<Array<GROUP>>{
    const url: string = this.baseUrl + 'api/group/usergroups';
    const params = new HttpParams().set('userEmail', userEmail);
    return this.http.get<Array<GROUP>>(url, {params});
  }
  addGroup(group: GROUP): Observable<GROUP>{
    const url: string = this.baseUrl + 'api/group/add';
    return this.http.post<GROUP>(url, group);
  }

  getFriends(userEmail: string): Observable<Array<USER>>{
    const url: string = this.baseUrl + 'api/friendship/friends';
    const params = new HttpParams().set('userEmail', userEmail);
    return this.http.get<Array<USER>>(url, {params});
  }
  addFriend(userEmail: string, friendEmail: string) : Observable<any> {
    const url: string = this.baseUrl + 'api/friendship/addfriend';
    const params = new HttpParams().set('userEmail', userEmail).set('friendEmail', friendEmail);
    return this.http.post(url, null, {params});
  }
  deleteFriend(userEmail: string, friendEmail: string) : Observable<any> {
    const url: string = this.baseUrl + 'api/friendship/delete';
    const params = new HttpParams().set('userEmail', userEmail).set('friendEmail', friendEmail);
    return this.http.delete(url, {params});
  }
}
