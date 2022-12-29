import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { GROUP, MESSAGE, MESSAGE_TO_ADD, MESSAGE_TO_UPDATE } from 'src/app/models/chat.models';
import { ACCESS_TOKEN_KEY, environment } from 'src/environments/environments';
import { USER } from 'src/app/models/authorization.models';

@Injectable({
  providedIn: 'root'
})
export class SocialMediaService {

  baseUrl: string = environment.api;
  userEmail = localStorage.getItem('email') as string;
  token = localStorage.getItem(ACCESS_TOKEN_KEY) as string;
  constructor(private http: HttpClient) { }

  getGroups(userEmail: string): Observable<Array<GROUP>>{
    const url: string = this.baseUrl + 'api/group/usergroups';
    const params = new HttpParams().set('userEmail', userEmail);
    return this.http.get<Array<GROUP>>(url, {params});
  }
  getGroupUsers(groupId: number): Observable<Array<USER>>{
    const url: string = this.baseUrl + 'api/group/members/' + groupId;
    return this.http.get<Array<USER>>(url);
  }
  addUsersToGroup(groupId: number, users: Array<USER>): Observable<any>
  {
    const url: string = this.baseUrl + 'api/group/update/' + groupId;
    return this.http.put(url, users).pipe(take(1));;
  }
  searchGroups(searchString: string): Observable<Array<GROUP>>{
    const url: string = this.baseUrl + 'api/group/search/' + searchString;
    return this.http.get<Array<GROUP>>(url).pipe(take(1));
  }

  getMessages(groupId: number): Observable<Array<MESSAGE>>{
    const url: string = this.baseUrl + 'api/message/chat';
    const params = new HttpParams().set('groupId', groupId);
    return this.http.get<Array<MESSAGE>>(url, {params }).pipe(take(1));
  }
  addMessage(message: MESSAGE_TO_ADD): Observable<any>{
    const url: string = this.baseUrl + 'api/message/add';

    return this.http.post(url, message);
  }
  replyMessage(message: MESSAGE_TO_ADD): Observable<any>{
    const url: string = this.baseUrl + 'api/message/reply';

    return this.http.post(url, message);
  }
  editMessage(message: MESSAGE_TO_UPDATE): Observable<any>{
    const url: string = this.baseUrl + 'api/message/update';

    return this.http.put(url, message);
  }
  deleteMessage(messageId: number): Observable<any>{
    const url: string = this.baseUrl + 'api/message/delete/' + messageId;

    return this.http.delete(url);
  }
  deleteForUserMessage(messageId: number): Observable<any>{
    const url: string = this.baseUrl + 'api/message/delete/for/user';
    return this.http.put(url, messageId);
  }
}
