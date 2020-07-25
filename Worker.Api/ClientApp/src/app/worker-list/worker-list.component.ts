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
  newWorker: WorkerProfile = new WorkerProfile();
  newSkill: Skill = new Skill();

  constructor(private workersService: WorkersService) { }

  ngOnInit(): void {
    this.initializeNewWorker()
    this.workersService.getAll().subscribe(data => {
      this.workers = data;
    });
  }

  initializeNewWorker(): void {
    this.newWorker = new WorkerProfile();
    this.newWorker.address = new Address();
    this.newWorker.skills = new Array<Skill>(); // TODO: Pozwolić użytkownikowi wybrać z listy krajów
  }

  createAndClear(worker: WorkerProfile): void {
    this.workersService.save(worker).subscribe(this.workers.push);
    // this.initializeNewWorker(); // TODO: Odkomentować
  }

  addSkill(skill: Skill) {
    this.newWorker.skills.push(skill);
    this.newSkill = new Skill();
  }
}
