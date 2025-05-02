import { Component } from '@angular/core';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Products';
  isLogged:boolean = false;


  constructor(private auth: AuthService) {

  }

  ngOnInit() {
    this.auth.isLoggedIn$.subscribe(isLogged => {
      this.isLogged = isLogged;
    });
  }
}
