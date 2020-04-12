import { TimeSlice } from './../../models/time-slice';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TimeTrackerService {

  constructor(private http: HttpClient) { }

  public add(item: TimeSlice): Observable<Object> {
    return this.http.post('api/timeTracker', item);
  }

  public update(item: TimeSlice) {
    return this.http.put('api/timeTracker', item);
  }

  public getAll(): Observable<TimeSlice[]> {
    return this.http.get<TimeSlice[]>('api/timeTracker');
  }

  public deleteAll(): Observable<Object> {
    return this.http.delete('api/timeTracker');
  }
}
