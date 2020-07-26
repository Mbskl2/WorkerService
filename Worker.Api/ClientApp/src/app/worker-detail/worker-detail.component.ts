import { Component, Input, Output, EventEmitter } from '@angular/core';
import WorkerProfile from '../shared/models/WorkerProfile';
import Address from '../shared/models/Address';
import Skill from '../shared/models/Skill';

@Component({
  selector: 'app-worker-detail',
  templateUrl: './worker-detail.component.html',
  styleUrls: ['./worker-detail.component.css']
})
export class WorkerDetailComponent {

  @Input() worker: WorkerProfile;
  @Input() areNewAllowed = false;
  @Output() saveAccepted = new EventEmitter<WorkerProfile>(); // TODO: Wszędzie tak zrobić, że jeżeli inicjalizacja to bez typu

  saveAndClear(worker: WorkerProfile): void {
    this.saveAccepted.emit(worker);
    this.clear();
  }

  clear(): void {
    this.initializeNewWorker();
  }

  initializeNewWorker(): void {
    this.worker = new WorkerProfile();
    this.worker.address = new Address();
    this.worker.skills = new Array<Skill>();
  }
}
