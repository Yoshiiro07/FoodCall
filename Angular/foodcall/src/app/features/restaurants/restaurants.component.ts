import { Component, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface Restaurant {
  id: number;
  name: string;
  category: string;
  rating: number;
  image: string;
}

@Component({
  selector: 'app-restaurants',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './restaurants.component.html',
  styleUrl: './restaurants.component.css'
})
export class RestaurantsComponent {
  searchTerm = signal('');

  restaurants: Restaurant[] = [
    { id: 1, name: 'Pizzaria Bella Napoli', category: 'Italiana', rating: 4.5, image: 'images/Pizzaria Bella Napoli.jpg' },
    { id: 2, name: 'Burger House', category: 'Hamburguer', rating: 4.8, image: 'images/BurgerHouse.jpg' },
    { id: 3, name: 'Sushi Premium', category: 'Japonesa', rating: 4.7, image: 'images/Sushi Premium.png' },
    { id: 4, name: 'Churrascaria Gaúcha', category: 'Churrasco', rating: 4.6, image: 'https://via.placeholder.com/300x200?text=Churrasco' },
    { id: 5, name: 'Cantina Italiana', category: 'Italiana', rating: 4.4, image: 'https://via.placeholder.com/300x200?text=Massas' },
    { id: 6, name: 'Taco Loco', category: 'Mexicana', rating: 4.5, image: 'https://via.placeholder.com/300x200?text=Tacos' },
    { id: 7, name: 'Açaí da Praia', category: 'Sobremesas', rating: 4.9, image: 'https://via.placeholder.com/300x200?text=Acai' },
    { id: 8, name: 'Dragon Wok', category: 'Chinesa', rating: 4.3, image: 'https://via.placeholder.com/300x200?text=Wok' },
  ];

  filteredRestaurants = computed(() => {
    const term = this.searchTerm().toLowerCase();
    if (!term) {
      return this.restaurants;
    }
    return this.restaurants.filter(r => 
      r.name.toLowerCase().includes(term) || 
      r.category.toLowerCase().includes(term)
    );
  });

  onSearchChange(value: string) {
    this.searchTerm.set(value);
  }

  viewMenu(restaurantId: number) {
    console.log('Ver cardápio do restaurante:', restaurantId);
    // TODO: Navegar para página de cardápio
  }
}
