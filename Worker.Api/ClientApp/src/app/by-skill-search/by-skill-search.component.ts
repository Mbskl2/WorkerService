import { Component } from '@angular/core';
import WorkersService from '../shared/api/workers.service'
import WorkerProfile from '../shared/models/WorkerProfile';
import Skill from '../shared/models/Skill';

@Component({
  selector: 'app-by-skill-search',
  templateUrl: './by-skill-search.component.html',
  styleUrls: ['./by-skill-search.component.css']
})
export class BySkillSearchComponent {

  workers: Array<WorkerProfile> = new Array<WorkerProfile>();
  skills: Array<Skill> = new Array<Skill>();
  newSkill: Skill = new Skill();

  constructor(private workersService: WorkersService) { }

  search(skills: Array<Skill>): void {
    this.workersService.getBySkills(skills)
      .subscribe(data => { this.workers = data });
    this.skills = new Array<Skill>();
  }

  addSkill(skill: Skill): void {
    this.skills.push(skill);
    this.newSkill = new Skill();
  }
}
