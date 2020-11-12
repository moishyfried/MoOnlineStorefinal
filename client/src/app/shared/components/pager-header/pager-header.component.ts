import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-pager-header',
  templateUrl: './pager-header.component.html',
  styleUrls: ['./pager-header.component.scss']
})
export class PagerHeaderComponent implements OnInit {

  constructor() { }
@Input() PageNumber : number;
@Input() PageSize : number;
@Input() totalCount : number;
  ngOnInit(): void {
  }

}
