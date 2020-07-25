import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import WorkerProfile from '../models/WorkerProfile';
import Address from '../models/Address';
import Skill from '../models/Skill';

@Injectable()
export default class WorkersService {

  public BACKEND = 'http://localhost:9090'; // TODO: Przenieść do configa
  public API = `${this.BACKEND}/api/workers`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Array<WorkerProfile>> {
    return this.http.get<Array<WorkerProfile>>(this.API);
  }

  get(id: string) {
    return this.http.get(`${this.API}/${id}`);
  }

  getBySkills(skills: Array<Skill>) {
    const skillString = skills.map(skill => { return `skills=${skill.name}` }).join('&');
    return this.http.get(`${this.API}/bySkills?${skillString}`);
  }

  getByLocation(radiusInKm: number, address: Address) {
    const params = `radiusInKm=${radiusInKm}
                      &countryIsoCode=${address.country}
                      &city=${address.city}
                      &street=${address.street}
                      &houseNumber=${address.houseNumber}`;
    return this.http.get(`${this.API}/byLocation?${params}`);
  }

  save(worker: WorkerProfile): Observable<WorkerProfile> {
    if (worker.workerProfileId) {
      return this.http.put<WorkerProfile>(`${this.API}/${worker.workerProfileId}`, worker);
    } else {
      return this.http.post<WorkerProfile>(this.API, worker);
    }
  }
}
