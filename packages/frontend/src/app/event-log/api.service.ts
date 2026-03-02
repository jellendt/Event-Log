import { HttpClient, HttpParams } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { PagedResult } from "./models/paged-result";
import { EventLogItem } from "./models/event-log-item";

@Injectable({ providedIn: 'root' })
export class EventLogApiService {
  http = inject(HttpClient)

  getEventLogs(page: number, pageSize: number, timeFrom: Date) : Observable<PagedResult<EventLogItem>> {
    const params = new HttpParams()
    .set('page', page)
    .set('pageSize', pageSize)
    .set('timeFrom', timeFrom.toISOString());
    return this.http.get<PagedResult<EventLogItem>>('https://localhost:7036/event-log', {
      params: params
    });
  }
}
