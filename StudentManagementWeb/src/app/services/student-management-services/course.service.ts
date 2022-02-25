import { courseInfoModel } from './../../models/studen-management-models/course-model';
import { studentInfoModel } from './../../models/studen-management-models/student-model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Constants } from 'src/app/helper/constants';
import { ResponseModel } from 'src/app/models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  constructor(private httpClient:HttpClient, private toastr:ToastrService) { }

  public courseInfoModel:courseInfoModel=new courseInfoModel();

  private readonly courseInfoUrl:string= Constants.API_KEY+"api/Course/";

  public insert(params:any){
    return this.httpClient.post<ResponseModel>(this.courseInfoUrl +"Insert", params)
  }
  public update(params:any){
    return this.httpClient.put<ResponseModel>(this.courseInfoUrl +"Update", params)
  }
  public getAll()
  {
   return this.httpClient.get<ResponseModel>(this.courseInfoUrl+"GetAll");
  }
  public delete(id:number){
    return this.httpClient.delete(`${this.courseInfoUrl}${id}`)
  }
}
