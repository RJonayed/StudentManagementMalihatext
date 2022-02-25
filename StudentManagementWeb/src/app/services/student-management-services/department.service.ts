import { departmentInfoModel } from './../../models/studen-management-models/department.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ResponseModel } from 'src/app/models/responseModel';
import { Constants } from 'src/app/helper/constants';


@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  public departmentInfoModel:departmentInfoModel=new departmentInfoModel();
  constructor(private httpClient:HttpClient,private toastr:ToastrService) { }

  private readonly department:string=Constants.API_KEY+"api/department/";


  public insert(){
    return this.httpClient.post<ResponseModel>(this.department+"Insert",this.departmentInfoModel);

  }

  public  update (){

    return this.httpClient.put<ResponseModel>(this.department+"Update", this.departmentInfoModel);

  }

  public getAll()
  {
   return this.httpClient.get<ResponseModel>(this.department+"GetAll");
  }

  public delete(id:number){
    return this.httpClient.delete(`${this.department}${id}`)
  }
}
