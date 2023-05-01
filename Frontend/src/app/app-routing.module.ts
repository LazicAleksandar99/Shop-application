import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationComponent } from './pages/authentication/authentication.component';
import { LoginComponent } from './pages/authentication/login/login.component';
import { RegistrationComponent } from './pages/authentication/registration/registration.component';

const routes: Routes = [
  {path:'', redirectTo:'/authentication/login',pathMatch:'full'},
  {
    path:'authentication', component: AuthenticationComponent,
    children:[
      { path: 'login', component: LoginComponent},
      { path: 'registration', component: RegistrationComponent},
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
