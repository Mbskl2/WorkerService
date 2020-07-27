import { Component } from '@angular/core';
import WorkersService from '../shared/api/workers.service'
import WorkerProfile from '../shared/models/WorkerProfile';
import Address from '../shared/models/Address';

@Component({
  selector: 'app-by-location-search',
  templateUrl: './by-location-search.component.html',
  styleUrls: ['./by-location-search.component.css']
})
export class ByLocationSearchComponent {

  workers: Array<WorkerProfile> = new Array<WorkerProfile>();
  radiusInKm: number;
  address: Address = new Address();
  error: string;

  constructor(private workersService: WorkersService) { }

  search(radiusInKm: number, address: Address): void {
    this.workersService.getByLocation(radiusInKm, address)
      .subscribe(
        data => { 
          this.workers = data
          this.error = null;
        },
        error => { this.error = error.message });
  }
}
