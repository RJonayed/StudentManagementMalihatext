import { CourseComponent } from './course/course.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DepartmentComponent } from './department/department.component';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ResigtrationComponent } from './resigtration/resigtration.component';
import { StudentComponent } from './student/student.component';



@NgModule({
  declarations: [
    DepartmentComponent,
    CourseComponent,
    StudentComponent,
    ResigtrationComponent

  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
  ],
  exports: [
    DepartmentComponent,
    CourseComponent,
    StudentComponent,
    ResigtrationComponent
  ]
})
export class StudentmanagementModule { }
