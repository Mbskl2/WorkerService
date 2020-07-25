import { Component, OnInit } from '@angular/core';
import WorkersService from '../shared/api/workers.service'
import WorkerProfile from '../shared/models/WorkerProfile';
import Address from '../shared/models/Address';
import Skill from '../shared/models/Skill';

@Component({
  selector: 'app-worker-list',
  templateUrl: './worker-list.component.html',
  styleUrls: ['./worker-list.component.css']
})
export class WorkerListComponent implements OnInit {

  workers: Array<WorkerProfile>;
  currentWorker: WorkerProfile;
  newSkill: Skill = new Skill();

  constructor(private workersService: WorkersService) { }

  ngOnInit(): void {
    this.initializeNewWorker()
    this.workersService.getAll().subscribe(data => { // TODO: Przed załadowaniem powinno być Loading...
      this.workers = data;
    });
  }

  initializeNewWorker(): void {
    this.currentWorker = new WorkerProfile();
    this.currentWorker.address = new Address(); // TODO: Pozwolić użytkownikowi wybrać z listy krajów
    this.currentWorker.skills = new Array<Skill>(); 
  }

  createAndClear(worker: WorkerProfile): void {
    this.workersService.save(worker).subscribe(this.workers.push);
    this.initializeNewWorker();
  }

  addSkill(skill: Skill) {
    this.currentWorker.skills.push(skill);
    this.newSkill = new Skill();
  }
}
