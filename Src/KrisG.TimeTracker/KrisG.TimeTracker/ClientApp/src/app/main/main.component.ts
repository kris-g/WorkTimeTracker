import { Observable, interval, of, timer } from 'rxjs';
import { map } from 'rxjs/operators';
import { TimeSlice } from './../../models/time-slice';
import { TimeTrackerService } from './time-tracker.service';
import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  public currentStart: TimeSlice;
  public timeSplitItems: TimeSlice[] = [];
  public totalDuration: moment.Duration = moment.duration(0);
  public durationUntilFinished: Observable<string>;
  public timeWorked: Observable<string>;
  public durationWorked$: Observable<string>;
  public durationUntilFinish$: Observable<string>;
  public currentFinishTime$: Observable<Date>;

  constructor(private timeTrackerService: TimeTrackerService) { }

  ngOnInit() {
    this.refreshTimeTrackers();

    this.durationUntilFinished = timer(0, 1000).pipe(
      map(() => this.durationUntilFinish())
    );

    this.durationUntilFinish$ = timer(0, 1000).pipe(
      map(() => this.timeUntilFinish()),
      map(x => x.asSeconds() < 0 ? '00:00:00' : this.formatDuration(x))
    );

    this.currentFinishTime$ = timer(0, 1000).pipe(
      map(() => this.currentFinishTime())
    );

    this.timeWorked = timer(0, 1000).pipe(
      map(() => {
        if (this.currentStart !== undefined) {
           return this.totalDuration.clone().add(moment().diff(this.currentStart.start));
        }
        return this.totalDuration;
      }),
      map(x => this.formatDuration(x))
    );

    this.durationWorked$ = timer(0, 1000).pipe(
      map(() => {
        if (this.currentStart !== undefined) {
           return this.totalDuration.clone().add(moment().diff(this.currentStart.start));
        }
        return this.totalDuration;
      }),
      map(x => this.formatDuration(x))
    );
  }

  public getClosedTimeSlices(): TimeSlice[] {
    return this.timeSplitItems.filter(x => x.end !== null);
  }

  public onStartClick() {
    this.start();
  }

  public onStopClick() {
    this.stop();
  }

  public onClearClick() {
    this.clear();
  }

  public formatDuration(diff: moment.Duration): string {
    const hours = Math.floor(diff.asHours()).toFixed(0).padStart(2, '0');
    const minutes = diff.minutes().toFixed(0).padStart(2, '0');
    const seconds = diff.seconds().toFixed(0).padStart(2, '0');

    return `${hours}:${minutes}:${seconds}`;
  }

  public durationUntilFinish() {
    const workingDayHours = 8;
    let remainingWork = moment.duration(moment().add(workingDayHours, 'hours').diff(moment().add(this.totalDuration)));

    if (this.currentStart !== undefined) {
      remainingWork = remainingWork.subtract(moment().diff(this.currentStart.start));
    }

    return remainingWork.humanize();
    // moment(new Date(Date.now())).add(moment(remainingWork, 'duration'));
  }

  public timeUntilFinish() {
    const workingDayHours = 8;
    let remainingWork = moment.duration(moment().add(workingDayHours, 'hours').diff(moment().add(this.totalDuration)));

    if (this.currentStart !== undefined) {
      remainingWork = remainingWork.subtract(moment().diff(this.currentStart.start));
    }

    return remainingWork;
  }

  public currentFinishTime() {
    const workingDayHours = 8;
    let remainingWork = moment().add(workingDayHours, 'hours').subtract(this.totalDuration);

    if (this.currentStart !== undefined) {
      remainingWork = remainingWork.subtract(moment().diff(this.currentStart.start));
    }

    return remainingWork.toDate();
    // moment(new Date(Date.now())).add(moment(remainingWork, 'duration'));
  }

  private start() {
    this.currentStart = { start: new Date(Date.now()) };
    this.timeTrackerService.add(this.currentStart)
      .subscribe(() => this.refreshTimeTrackers());
  }

  private refreshTimeTrackers() {
    this.timeTrackerService.getAll().subscribe(items => {
      this.timeSplitItems = items;
      this.currentStart = items.find(x => x.end === null);

      this.totalDuration = this.timeSplitItems
        .filter(x => x.end !== null)
        .map(x => moment.duration(moment(x.end).diff(moment(x.start))))
        .reduce((pv, cv) => pv.add(cv), moment.duration(0));
    });
  }

  private stop() {
    const timeSlice = this.currentStart;
    timeSlice.end = new Date(Date.now());

    this.timeTrackerService.update(timeSlice).subscribe(() => this.refreshTimeTrackers());
    // const duration = moment.duration(moment(end).diff(moment(start)));

    // this.timeSplitItems.push({ start, end, duration });


    this.currentStart = undefined;
  }

  private clear() {
    this.timeTrackerService.deleteAll().subscribe(() => this.refreshTimeTrackers());
  }

}
