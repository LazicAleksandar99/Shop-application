import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthenticationComponent } from './pages/authentication/authentication.component';
import { LoginComponent } from './pages/authentication/login/login.component';
import { RegistrationComponent } from './pages/authentication/registration/registration.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AddArticleComponent } from './components/add-article/add-article.component';
import { HasRoleDirective } from './directives/has-role.directive';
import { SellerListComponent } from './components/seller-list/seller-list.component';
import { AllOrdersComponent } from './components/all-orders/all-orders.component';
import { ShowUserProfileComponent } from './components/show-user-profile/show-user-profile.component';

@NgModule({
  declarations: [
    AppComponent,
    AuthenticationComponent,
    LoginComponent,
    RegistrationComponent,
    DashboardComponent,
    AddArticleComponent,
    SellerListComponent,
    AllOrdersComponent,
    ShowUserProfileComponent,
    HasRoleDirective,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
