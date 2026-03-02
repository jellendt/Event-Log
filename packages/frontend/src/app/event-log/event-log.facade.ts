import { inject } from "@angular/core";
import { EventLogApiService } from "./api.service";
import { BehaviorSubject, combineLatest, switchMap, tap } from "rxjs";

export class EventLogFacade{
  eventLogApiService = inject(EventLogApiService);

  page$ = new BehaviorSubject<number>(1);
  pageSize$ = new BehaviorSubject<number>(25);
  selectedDate = new BehaviorSubject<Date>(new Date("2026-03-02T12:07:41.900Z"));

  totalPages$ = new BehaviorSubject<number>(1);

  eventLog$ = combineLatest([this.page$, this.pageSize$, this.selectedDate]).pipe(
    switchMap(([page, pageSize, selectedDate]) => this.eventLogApiService.getEventLogs(page, pageSize, selectedDate)),
    tap(pagedResult => this.totalPages$.next(Math.ceil(pagedResult.totalCount / pagedResult.pageSize))
  ));
}
