import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BySkillSearchComponent } from './by-skill-search.component';

describe('BySkillSearchComponent', () => {
  let component: BySkillSearchComponent;
  let fixture: ComponentFixture<BySkillSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BySkillSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BySkillSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
