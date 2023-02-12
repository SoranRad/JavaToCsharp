import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import baseUrl  from './helper';

@Injectable({
  providedIn: 'root',
})
export class ScoreService {
  public loginStatusSubject = new Subject<boolean>();

  constructor(private _http: HttpClient) {}

  public saveScore(Score) {
    const token = localStorage.getItem('token');
    console.log(token);
    
    let h = new HttpHeaders();
    h.set('www-authenticate',' Bearer '+ token)
    h.set('Authorization', ' Bearer ' + token);

    return this._http.post(`${baseUrl}/Scores/`, Score,{ headers: h }); 
  }
  
  public getScores() {
    return this._http.get(`${baseUrl}/Scores/`);
  }
}
