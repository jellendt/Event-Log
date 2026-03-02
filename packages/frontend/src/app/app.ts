import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { EventLogComponent } from "./event-log/event-log.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, EventLogComponent],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('frontend');
}
