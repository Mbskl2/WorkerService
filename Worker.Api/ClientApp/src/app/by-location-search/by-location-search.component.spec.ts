import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ByLocationSearchComponent } from './by-location-search.component';

describe('ByLocationSearchComponent', () => {
  let component: ByLocationSearchComponent;
  let fixture: ComponentFixture<ByLocationSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ByLocationSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ByLocationSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
