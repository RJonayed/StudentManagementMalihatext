import { ResigtrationComponent } from './modules/studentmanagement/resigtration/resigtration.component';
import { CourseComponent } from './modules/studentmanagement/course/course.component';
import { DepartmentComponent } from './modules/studentmanagement/department/department.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from './guards/auth.service';
import { LoggedInAuthGuard } from './guards/loggedInAuth.service';
import { HomeComponent } from './modules/deshboard/home/home.component';
import { PageNotFoundComponent } from './modules/deshboard/page-not-found/page-not-found.component';
import { ManageRoleComponent } from './modules/user-auth/manage-role/manage-role.component';
import { ManageUserAccountComponent } from './modules/user-auth/manage-user-account/manage-user-account.component';
import { SignInComponent } from './modules/user-auth/sign-in/sign-in.component';
import { SignUpComponent } from './modules/user-auth/sign-up/sign-up.component';
import { StudentComponent } from './modules/studentmanagement/student/student.component';

const routes: Routes = [
  {path: "sign-up", component: SignUpComponent, canActivate:[LoggedInAuthGuard]},
  {path: "sign-in", component: SignInComponent, canActivate:[LoggedInAuthGuard]},
  {path: "", component: HomeComponent, canActivate:[AuthGuardService]},
  {path: "manage-role", component: ManageRoleComponent, canActivate:[AuthGuardService]},
  {path: "manage-user", component: ManageUserAccountComponent, canActivate:[AuthGuardService]},
  {path: "department", component: DepartmentComponent, canActivate:[AuthGuardService]},
  {path: "student", component: StudentComponent, canActivate:[AuthGuardService]},
  {path: "course", component: CourseComponent, canActivate:[AuthGuardService]},
  {path: "Resigtration", component: ResigtrationComponent, canActivate:[AuthGuardService]},

 { path: '**', pathMatch: 'full', component: PageNotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
