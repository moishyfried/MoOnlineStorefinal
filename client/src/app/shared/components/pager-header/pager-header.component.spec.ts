import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PagerHeaderComponent } from './pager-header.component';

describe('PagerHeaderComponent', () => {
  let component: PagerHeaderComponent;
  let fixture: ComponentFixture<PagerHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PagerHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PagerHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
