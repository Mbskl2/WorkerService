import { Component, Input, Output, EventEmitter } from '@angular/core';
import WorkerProfile from '../shared/models/WorkerProfile';

@Component({
  selector: 'app-worker-list',
  templateUrl: './worker-list.component.html',
  styleUrls: ['./worker-list.component.css']
})
export class WorkerListComponent {

  @Input() workers: Array<WorkerProfile>;
  @Input() areNewAllowed = false;
  @Output() saveAccepted = new EventEmitter<WorkerProfile>();
  selectedWorker: WorkerProfile;

  select(worker: WorkerProfile): void {
    this.selectedWorker = worker;
  }

  saveAcceptedHandle(worker: WorkerProfile): void {
    this.workers.push(worker);
    this.saveAccepted.emit(worker);
  }
}
