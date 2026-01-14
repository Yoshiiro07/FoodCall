import { Component, signal, inject } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, FormsModule, CommonModule, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly authService = inject(AuthService);
  
  protected readonly title = signal('foodcall');
  protected showLoginModal = signal(false);
  protected username = '';
  protected password = '';
  protected isLoading = signal(false);
  protected errorMessage = signal<string | null>(null);
  
  // Observable do usuário atual
  protected currentUser$ = this.authService.currentUser$;

  onLoginClick() {
    this.showLoginModal.set(true);
    this.errorMessage.set(null);
  }

  closeLoginModal() {
    this.showLoginModal.set(false);
    this.username = '';
    this.password = '';
    this.errorMessage.set(null);
    this.isLoading.set(false);
  }

  onSubmitLogin(event: Event) {
    event.preventDefault();
    
    if (!this.username || !this.password) {
      this.errorMessage.set('Por favor, preencha todos os campos');
      return;
    }

    this.isLoading.set(true);
    this.errorMessage.set(null);

    this.authService.login(this.username, this.password).subscribe({
      next: (response) => {
        console.log('Login realizado com sucesso:', response);
        this.closeLoginModal();
        // TODO: Redirecionar para página inicial ou dashboard
      },
      error: (error) => {
        console.error('Erro no login:', error);
        this.errorMessage.set(error.message || 'Erro ao fazer login. Verifique suas credenciais.');
        this.isLoading.set(false);
      }
    });
  }

  onLogoutClick() {
    this.authService.logout();
    console.log('Logout realizado');
  }
}
