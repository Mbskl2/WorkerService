import { Component, OnInit } from '@angular/core';
import WorkersService from '../shared/api/workers.service'
import WorkerProfile from '../shared/models/WorkerProfile';
import Skill from '../shared/models/Skill';

@Component({
  selector: 'app-worker-list',
  templateUrl: './worker-list.component.html',
  styleUrls: ['./worker-list.component.css']
})
export class WorkerListComponent implements OnInit {

  workers: Array<WorkerProfile>;

  constructor(private workersService: WorkersService) { }

  ngOnInit(): void {
    this.workersService.getAll().subscribe(data => {
      this.workers = data;
    });
  }
}
