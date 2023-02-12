import { Component, OnInit, Input, Output, EventEmitter, ViewEncapsulation} from '@angular/core';
import Swal from 'sweetalert2';
import {FeedbackService} from 'src/app/services/feedback.service';
import {MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-add-feedback',
  templateUrl: './add-feedback.component.html',
  styleUrls: ['./add-feedback.component.css'],
  encapsulation: ViewEncapsulation.Emulated
})
export class AddFeedbackComponent implements OnInit {

  feedback = {
    FullName: '',
    Email: '',
    ContactNumber: '',
    Commnets: '',
    Rate:0,
  };
    @Input('rating')  rating: number = 3;
    @Input('starCount')  starCount: number = 5;
    @Input('color')  color: string = 'accent';
    @Output() private ratingUpdated = new EventEmitter();

      snackBarDuration: number = 2000;
      ratingArr = [];

    // tslint:disable-next-line:variable-name
    constructor
        (
          private _feedback: FeedbackService,  
          private _snack: MatSnackBar
        ) { }

  ngOnInit(): void {
      console.log("a "+this.starCount);
      for (let index = 0; index < this.starCount; index++) {
          this.ratingArr.push(index);
      }
  }
  onClick(rating:number) {
      console.log(rating);
      this._snack.open('You rated ' + rating + ' / ' + this.starCount, '', {
          duration: this.snackBarDuration
      });
      this.ratingUpdated.emit(rating);
      this.feedback.Rate = rating;
      return false;
  }

  showIcon(index:number) {
      if (this.rating >= index + 1) {
          return 'star';
      } else {
          return 'star_border';
      }
  }

  formSubmit() {
    if (this.feedback.FullName.trim() == '' || this.feedback.FullName == null) {
      this._snack.open('Name  Required !!', '', {
        duration: 3000,
      });
      return;
    }

    //all done

    this._feedback.addFeedback(this.feedback).subscribe(
        (data: any) => {
          this.feedback.FullName = '';
          this.feedback.Email = '';
          this.feedback.ContactNumber = '';
          this.feedback.Commnets = '';

          Swal.fire('Success !!', 'ّFeedback is added successfuly', 'success');
        },
        (error) => {
          console.log(error);
          Swal.fire('Error !!', 'Server error !!', 'error');
        }
    );
  }

}

export enum StarRatingColor {
    primary = "primary",
    accent = "accent",
    warn = "warn"
}
