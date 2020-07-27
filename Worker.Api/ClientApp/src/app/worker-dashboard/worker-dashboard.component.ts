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
  error: string;

  constructor(private workersService: WorkersService) { }

  ngOnInit(): void {
    this.workersService.getAll().subscribe(data => {
        this.workers = data;
          this.error = null;
        },
        error => { this.error = error.error.title }
    );
  }

  save(worker: WorkerProfile): void {
    this.workersService.save(worker).subscribe(data => {
      this.workers.push(worker);
      this.error = null;
    },
    error => {this.error = error.error.title}
    );
  }
}
