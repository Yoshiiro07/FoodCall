import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  standalone: true,
  template: `
    <div class="home-container">
      <h2>Bem-vindo ao FoodCall!</h2>
      <p>Escolha uma opção no menu acima para começar.</p>
    </div>
  `,
  styles: [`
    .home-container {
      max-width: 1200px;
      margin: 40px auto;
      padding: 20px;
      text-align: center;
    }

    h2 {
      font-size: 32px;
      color: #333;
      margin-bottom: 16px;
    }

    p {
      font-size: 18px;
      color: #666;
    }
  `]
})
export class HomeComponent {}
