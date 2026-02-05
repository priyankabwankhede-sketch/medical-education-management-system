import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Rotation {
  rotationId: number;
  traineeId: number;
  username: string;
  programName: string;
  startDate: string;
  endDate: string;
}

@Injectable({
  providedIn: 'root'
})
export class RotationService {

  private apiUrl = 'http://localhost:5103/api/rotations'; // backend endpoint

  constructor(private http: HttpClient) {}

  getRotations(): Observable<Rotation[]> {
    return this.http.get<Rotation[]>(this.apiUrl);
  }

  createRotation(data: { traineeId: number; startDate: string; endDate: string }): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }

  deleteRotation(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
