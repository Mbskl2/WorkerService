import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import WorkerProfile from '../models/WorkerProfile';
import Address from '../models/Address';
import Skill from '../models/Skill';

const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };

@Injectable()
export default class WorkersService {

  public BACKEND = 'https://localhost:5001'; // TODO: Przenieść do configa
  public API = `${this.BACKEND}/api/workers`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Array<WorkerProfile>> {
    return this.http.get<Array<WorkerProfile>>(this.API);
  }

  get(id: string): Observable<Array<WorkerProfile>> {
    return this.http.get<Array<WorkerProfile>>(`${this.API}/${id}`);
  }

  getBySkills(skills: Array<Skill>): Observable<Array<WorkerProfile>> {
    const skillString = skills.map(skill => { return `skills=${skill.name}` }).join('&');
    return this.http.get<Array<WorkerProfile>>(`${this.API}?${skillString}`);
  }

  getByLocation(radiusInKm: number, address: Address): Observable<Array<WorkerProfile>> {
    const params = `radiusInKm=${radiusInKm}
                      &countryIsoCode=${address.country}
                      &city=${address.city}
                      &street=${address.street}
                      &houseNumber=${address.houseNumber}`;
    return this.http.get<Array<WorkerProfile>>(`${this.API}?${params}`);
  }

  save(worker: WorkerProfile): Observable<WorkerProfile> {
    if (worker.id) {
      return this.http.put<WorkerProfile>(`${this.API}/${worker.id}`, worker, httpOptions);
    } else {
      return this.http.post<WorkerProfile>(this.API, worker, httpOptions);
    }
  }
}
