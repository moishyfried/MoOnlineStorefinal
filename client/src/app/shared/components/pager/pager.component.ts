import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {
@Input() pagesize :number;
@Input() totalcount :number;
@Input() Pagenumber :number;
@Output() pageChanged = new EventEmitter<number>();
maxbuttons = 5;
  constructor() { }

  ngOnInit(): void {
  }

onPageChange(event: any){
this.pageChanged.emit(event.page)
}
}
