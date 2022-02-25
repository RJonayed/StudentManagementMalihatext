import { studentInfoModel } from './../../models/studen-management-models/student-model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Constants } from 'src/app/helper/constants';
import { ResponseModel } from 'src/app/models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class StudentInfoService {

  constructor(private httpClient:HttpClient, private toastr:ToastrService) { }

  public studentInfoModel:studentInfoModel=new studentInfoModel();

  private readonly studentInfoUrl:string= Constants.API_KEY+"api/Student/";
  private readonly departmentUrl:string= Constants.API_KEY+"api/Department/";

  public insert(params:any){
    return this.httpClient.post<ResponseModel>(this.studentInfoUrl +"Insert", params)
  }
  public update(params:any){
    return this.httpClient.put<ResponseModel>(this.studentInfoUrl +"Update", params)
  }
  public getAll()
  {
   return this.httpClient.get<ResponseModel>(this.studentInfoUrl+"GetAll");
  }
  public getAllDepartment() {
    return this.httpClient.get<ResponseModel>(this.departmentUrl+"GetAll");
  }
  public getDepartmentById(id:number) {
    return this.httpClient.get<ResponseModel>(this.departmentUrl+"GetAllDepartmentById?id="+id);
  }
  public delete(id:number){
    return this.httpClient.delete(`${this.studentInfoUrl}${id}`)
  }
}
