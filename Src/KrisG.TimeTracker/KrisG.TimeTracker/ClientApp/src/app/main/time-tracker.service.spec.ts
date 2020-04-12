/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { TimeTrackerService } from './time-tracker.service';

describe('Service: TimeTracker', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TimeTrackerService]
    });
  });

  it('should ...', inject([TimeTrackerService], (service: TimeTrackerService) => {
    expect(service).toBeTruthy();
  }));
});
