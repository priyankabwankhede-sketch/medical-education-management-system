import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Trainee {
  traineeId: number;
  username: string;
  programName: string;
}

@Injectable({
  providedIn: 'root'
})
export class TraineeService {
  private apiUrl = 'http://localhost:5103/api/trainees'; // your .NET backend endpoint

  constructor(private http: HttpClient) { }

  // GET all trainees
  getTrainees(): Observable<Trainee[]> {
    return this.http.get<Trainee[]>(this.apiUrl);
  }

  // POST a new trainee
createTrainee(data: { username: string; programId: number }): Observable<Trainee> {
  return this.http.post<Trainee>(this.apiUrl, data);
}

deleteTrainee(id: number) {
  return this.http.delete(`http://localhost:5103/api/trainees/${id}`);
}

}
