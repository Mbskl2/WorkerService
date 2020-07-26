import { Component, Input } from '@angular/core';
import Skill from '../shared/models/Skill';

@Component({
  selector: 'app-skill-list',
  templateUrl: './skill-list.component.html',
  styleUrls: ['./skill-list.component.css']
})
export class SkillListComponent {

  @Input() skills: Array<Skill>;
  newSkill = new Skill();

  addSkill(skill: Skill): void {
    this.skills.push(skill);
    this.newSkill = new Skill();
  }
}
