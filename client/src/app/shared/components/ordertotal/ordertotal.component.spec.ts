import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrdertotalComponent } from './ordertotal.component';

describe('OrdertotalComponent', () => {
  let component: OrdertotalComponent;
  let fixture: ComponentFixture<OrdertotalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrdertotalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrdertotalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
