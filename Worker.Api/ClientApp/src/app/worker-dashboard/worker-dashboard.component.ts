import { Component, OnInit } from '@angular/core';
import WorkersService from '../shared/api/workers.service'
import WorkerProfile from '../shared/models/WorkerProfile';

@Component({
  selector: 'app-worker-dashboard',
  templateUrl: './worker-dashboard.component.html',
  styleUrls: ['./worker-dashboard.component.css']
})
export class WorkerDashboardComponent implements OnInit {

  workers: Array<WorkerProfile>;
  selectedWorker: WorkerProfile;

  constructor(private workersService: WorkersService) { }

  ngOnInit(): void {
    this.workersService.getAll().subscribe(data => { // TODO: Przed załadowaniem powinno być Loading...
      this.workers = data;
    });
  }

  save(worker: WorkerProfile): void {
    this.workersService.save(worker).subscribe(this.workers.push); // TODO: Nowy worker nie pokazuje się w worker-list
  }
}
