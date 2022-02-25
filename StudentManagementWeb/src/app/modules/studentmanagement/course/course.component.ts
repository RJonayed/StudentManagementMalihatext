import { CourseService } from './../../../services/student-management-services/course.service';
import { courseInfoModel } from 'src/app/models/studen-management-models/course-model';
import { DepartmentService } from './../../../services/student-management-services/department.service';
import { departmentInfoModel } from './../../../models/studen-management-models/department.model';
import { studentInfoModel } from './../../../models/studen-management-models/student-model';
import { StudentInfoService } from 'src/app/services/student-management-services/student.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ResponseCode } from 'src/app/enums/responseCode';
import { Constants } from 'src/app/helper/constants';
import { ResponseModel } from 'src/app/models/responseModel';


@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styles: [
  ]
})
export class CourseComponent implements OnInit {

  public formSubmitAttempt: boolean= false;
  @ViewChild('closebutton') closebutton: any;
  constructor(private formBuilder: FormBuilder, private toastr: ToastrService, private CourseService: CourseService) { }
  public courseList:courseInfoModel[]=[];

  ngOnInit(): void {
    this.clearForm();
    this.getAll();
  }
  getAll(){
    this.CourseService.getAll().subscribe((data: any) => {
      this.courseList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })
  }

  public courseInformationForm = this.formBuilder.group({
    courseId: [0,],
    courseTitle: ['', Validators.required],
    seatCount: ['', Validators.required],
    fees: ['', Validators.required],
  })
  get courseTitle() { return this.courseInformationForm.get("courseTitle") };
  get seatCount() { return this.courseInformationForm.get("seatCount") };
  get fees() { return this.courseInformationForm.get("fees") };

  pupulateForm(selectedRecord: courseInfoModel) {
    this.courseInformationForm.patchValue({
      courseId: selectedRecord.id,
      courseTitle: selectedRecord.title,
      seatCount: selectedRecord.seatCount,
      fees: selectedRecord.fees
    });
  }

  onSubmit() {
    debugger;
    if(this.courseInformationForm.valid){
      if (this.courseInformationForm.value.courseId == 0||this.courseInformationForm.value.courseId == null) {
        this.insert();
      } else {
        this.update();
      }
    }else{
      this.formSubmitAttempt=true;
    }

  }
  insert() {
    const formData: FormData = new FormData();
    formData.append('Id', this.courseInformationForm.value.courseId);
    formData.append('Title', this.courseInformationForm.value.courseTitle);
    formData.append('SeatCount', this.courseInformationForm.value.seatCount);
    formData.append('Fees', this.courseInformationForm.value.fees);

    this.CourseService.insert(formData).subscribe((data: ResponseModel) => {
      if (data.responseCode == ResponseCode.OK) {
        this.toastr.success(data.responseMessage);

        this.getAll();
        this.clearForm();
        this.closebutton.nativeElement.click();
      } else {
        this.toastr.error(data.responseMessage)
      }
      console.log("response", data)
    }, err => {
      console.log("error", err);
      this.toastr.error("Something went wrong try again later")
    })
  }
  update() {
debugger;
    const formData: FormData = new FormData();

    formData.append('Id', this.courseInformationForm.value.courseId);
    formData.append('Title', this.courseInformationForm.value.courseTitle);
    formData.append('SeatCount', this.courseInformationForm.value.seatCount);
    formData.append('Fees', this.courseInformationForm.value.fees);

    this.CourseService.update(formData).subscribe((data: ResponseModel) => {
      if (data.responseCode == ResponseCode.OK) {
        this.toastr.success(data.responseMessage);

        this.getAll();
        this.clearForm();
        this.closebutton.nativeElement.click();
      } else {
        this.toastr.error(data.responseMessage)
      }
      console.log("response", data)
    }, err => {
      console.log("error", err);
      this.toastr.error("Something went wrong try again later")
    })
  }
  onDelete(id:number){
    if(confirm("Are u sure to delete this recored ?")){
      this.CourseService.delete(id).subscribe(
       res=>{
          this.getAll();
         this.toastr.success("Delete succfully");

       },
       err=>
       {
        this.toastr.error("Delete Failed")
         console.log(err)
        }
     )
   }
  }
  clearForm() {

    this.courseInformationForm.get('courseId')?.setValue('');
    this.courseInformationForm.get('courseTitle')?.setValue('');
    this.courseInformationForm.get('seatCount')?.setValue('');
    this.courseInformationForm.get('fees')?.setValue('');

  }
}
