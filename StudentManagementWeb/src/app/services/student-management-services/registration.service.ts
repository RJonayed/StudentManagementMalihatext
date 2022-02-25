import { resigtrationInofModel } from './../../models/studen-management-models/resigtration-model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Constants } from 'src/app/helper/constants';
import { ResponseModel } from 'src/app/models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class ResigtrationService {

  constructor(private httpClient:HttpClient, private toastr:ToastrService) { }

  public registrationInfoModel:resigtrationInofModel=new resigtrationInofModel();

  private readonly registrationInfoUrl:string= Constants.API_KEY+"api/Resigstration/";
  private readonly studentUrl:string= Constants.API_KEY+"api/Student/";
  private readonly courseUrl:string= Constants.API_KEY+"api/Course/";

  public insert(params:any){
    return this.httpClient.post<ResponseModel>(this.registrationInfoUrl +"Insert",params)
  }
  public update(params:any){
    return this.httpClient.put<ResponseModel>(this.registrationInfoUrl +"Update",params)
  }
  public getAll()
  {
   return this.httpClient.get<ResponseModel>(this.registrationInfoUrl+"GetAll");
  }
  public getStudent()
  {
   return this.httpClient.get<ResponseModel>(this.studentUrl+"GetAll");
  }
  public getStudentById(id:number) {
    return this.httpClient.get<ResponseModel>(this.studentUrl+"GetAllStudentById?id="+id);
  }
  public getCourse()
  {
   return this.httpClient.get<ResponseModel>(this.courseUrl+"GetAll");
  }
  public getCourseById(id:number) {
    return this.httpClient.get<ResponseModel>(this.courseUrl+"GetAllCourseById?id="+id);
  }
  public delete(id:number){
    return this.httpClient.delete(`${this.registrationInfoUrl}${id}`)
  }
  public getById(id:number) {
    return this.httpClient.get<ResponseModel>(this.registrationInfoUrl+"GetById?id="+id);
  }

}
