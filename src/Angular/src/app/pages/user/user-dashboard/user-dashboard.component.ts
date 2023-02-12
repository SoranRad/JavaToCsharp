import { Component, OnInit } from '@angular/core';
import {StarRatingColor} from '../add-feedback/add-feedback.component';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit {
  rating:number = 3;
  starCount:number = 5;
  starColor: StarRatingColor = StarRatingColor.accent;
  starColorP: StarRatingColor = StarRatingColor.primary;
  starColorW: StarRatingColor = StarRatingColor.warn;
  constructor() { }

  ngOnInit(): void {
  }

}
