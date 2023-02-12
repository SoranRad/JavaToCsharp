import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import baseUrl from './helper';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  constructor(private  _http: HttpClient) { }
  
  public addFeedback(feedback) {
    return this._http.post(`${baseUrl}/Feedback`, feedback);
  }

  public getFeedbackList() {
    return this._http.get(`${baseUrl}/Feedback`);
  }

}
