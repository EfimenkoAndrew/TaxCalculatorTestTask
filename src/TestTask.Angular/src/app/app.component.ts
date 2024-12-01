import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CalculateComponent } from "./calculate/calculate.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CalculateComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.sass'
})
export class AppComponent {
  title = 'TestTask.Angular';
}
