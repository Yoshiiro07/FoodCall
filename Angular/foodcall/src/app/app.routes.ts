import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { RestaurantsComponent } from './features/restaurants/restaurants.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'restaurantes', component: RestaurantsComponent },
  { path: '**', redirectTo: '' }
];
