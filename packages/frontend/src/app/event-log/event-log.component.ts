import { ZardPaginationImports } from '@/shared/components/pagination';
import { Component, effect, inject } from '@angular/core';
import { EventLogFacade } from './event-log.facade';
import { toSignal } from '@angular/core/rxjs-interop';
import { CommonModule } from '@angular/common';
import { SeverityLabels } from './models/severity.enum';

@Component({
  selector: 'app-event-log',
  imports: [ZardPaginationImports, CommonModule],
  template: `
  <div>
    @for(log of eventLog()?.items; track log.id) {
      <div>
        {{log.message}} - {{log.timestamp | date:'dd.MM.yyyy HH:mm'}} - {{SeverityLabels[log.severity]}}
      </div>
    }
  </div>
  <z-pagination [zTotal]="totalPages()" [zPageIndex]="page()" (zPageIndexChange)="onPageChange($event)"/>`,
  standalone: true,
  providers: [EventLogFacade],
})
export class EventLogComponent {
  readonly SeverityLabels = SeverityLabels;
  facade = inject(EventLogFacade);
  constructor(){
    effect(() => {
      console.log(this.page());
    })
  }
  eventLog = toSignal(this.facade.eventLog$, { initialValue: null });
  page = toSignal(this.facade.page$, { initialValue: 1 });
  totalPages = toSignal(this.facade.totalPages$, { initialValue: 1 });


  onPageChange(page: number) {
    this.facade.page$.next(page);
  }
}
