import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from '@features/home/home/home.component';
import { LoginComponent } from './features/auth/login/login.component';
import { ShopMainComponent } from './features/shop/pages/shop-main/shop-main.component';
import { ProductDetailComponent } from './features/shop/pages/product-detail/product-detail.component';
import { CartComponent } from './features/shop/components/cart/cart.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'shop', component: ShopMainComponent },
  { path: 'shop/product/:id', component: ProductDetailComponent },
  { path: 'cart', component: CartComponent },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
