import { Component, OnInit } from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {FeedbackService} from 'src/app/services/feedback.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-list-feedback',
  templateUrl: './list-feedback.component.html',
  styleUrls: ['./list-feedback.component.css']
})

export class ListFeedbackComponent implements OnInit {
  feedbacks : MatTableDataSource<Feedback> ;
  displayedColumns = ['fullName', 'email', 'contactNumber', 'rate','commnets'];
  

  constructor(private _feedbackService: FeedbackService) {}

  ngOnInit(): void {
     this._feedbackService.getFeedbackList().subscribe(
      (data: any) => {
        console.log(data);
        this.feedbacks = new MatTableDataSource<Feedback>(data.dataSet);
        console.log(this.feedbacks);
      },
      (error) => {
        console.log(error);
        Swal.fire('Error !!', 'Feebacks loading error !!', 'error');
      }
   ); 
  }
}


export interface Feedback{
  FullName :string;
  Email : string; 
  ContactNumber :string;
  Rate : number ;
  Commnets : string;
}