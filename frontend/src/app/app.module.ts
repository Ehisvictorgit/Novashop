//Systems components
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  provideHttpClient,
  withInterceptorsFromDi,
  withFetch,
} from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { DatePipe } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CommonModule } from '@angular/common';

//Kendo UI components
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { GridModule } from '@progress/kendo-angular-grid';
import { IconsModule } from '@progress/kendo-angular-icons';

//Users components
import { LoginComponent } from './features/auth/login/login.component';
import { HomeComponent } from './features/home/home/home.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { SuccessDialogComponent } from '@shared/components/modals/success-dialog/success-dialog.component';
import { FailureDialogComponent } from '@shared/components/modals/failure-dialog/failure-dialog.component';
import { WarningDialogComponent } from '@shared/components/modals/warning-dialog/warning-dialog.component';
import { ConfirmDialogComponent } from '@shared/components/modals/confirm-dialog/confirm-dialog.component';
import { ShopMainComponent } from '@features/shop/pages/shop-main/shop-main.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SuccessDialogComponent,
    FailureDialogComponent,
    WarningDialogComponent,
    ConfirmDialogComponent,
    LoginComponent,
    ShopMainComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    MatDialogModule,
    ButtonsModule,
    InputsModule,
    DropDownsModule,
    GridModule,
    IconsModule,
    CommonModule,
  ],
  providers: [
    DatePipe,
    provideHttpClient(withInterceptorsFromDi(), withFetch()),
    provideAnimationsAsync(),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
