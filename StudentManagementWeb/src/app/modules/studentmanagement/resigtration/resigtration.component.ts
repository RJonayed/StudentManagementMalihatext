import { StudentInfoService } from './../../../services/student-management-services/student.service';
import { CourseService } from './../../../services/student-management-services/course.service';
import { ResigtrationService } from './../../../services/student-management-services/registration.service';
import { studentInfoModel } from './../../../models/studen-management-models/student-model';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ResponseCode } from 'src/app/enums/responseCode';
import { ResponseModel } from 'src/app/models/responseModel';
import { resigtrationInofModel } from 'src/app/models/studen-management-models/resigtration-model';
import { courseInfoModel } from 'src/app/models/studen-management-models/course-model';


@Component({
  selector: 'app-resigtration',
  templateUrl: './resigtration.component.html',
  styles: [
  ]
})
export class ResigtrationComponent  implements OnInit {
  public formSubmitAttempt: boolean= false;
  @ViewChild('closebutton') closebutton: any;
  constructor(private formBuilder: FormBuilder, private toastr: ToastrService, private ResigtrationService:ResigtrationService,private CourseService:CourseService,private StudentInfoService:StudentInfoService) { }
  public resigtrationList: resigtrationInofModel[] = [];
  public studentList: studentInfoModel[] = [];
  public courseList: courseInfoModel[] = [];
  ngOnInit(): void {
    this.clearForm();
    this.getAll();
    this.getAllCourse();
    this.getAllStudent();
  }
  getAll(){
    this.ResigtrationService.getAll().subscribe((data: any) => {
      this.resigtrationList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })
  }

  public resigtrationInfoForm = this.formBuilder.group({
    registrationId: [0,],
    studentId: ['', Validators.required],
    courseId: ['', Validators.required],
    enrollDate: ['', Validators.required],
    isPaymentComplete: ['']
  })
  get registrationId() { return this.resigtrationInfoForm.get("registrationId") };
  get studentId() { return this.resigtrationInfoForm.get("studentId") };
  get courseId() { return this.resigtrationInfoForm.get("courseId") };
  get enrollDate() { return this.resigtrationInfoForm.get("enrollDate") };
  get isPaymentComplete() { return this.resigtrationInfoForm.get("isPaymentComplete") };

  pupulateForm(selectedRecord: resigtrationInofModel) {
    this.resigtrationInfoForm.patchValue({
      registrationId: selectedRecord.registrationId,
      courseId: selectedRecord.courseId,
      studentId: selectedRecord.studentId,
      enrollDate: selectedRecord.enrollDate,
      isPaymentComplete:selectedRecord.isPaymentComplete

    });

  }

  onSubmit() {
    debugger;
    if(this.resigtrationInfoForm.valid){
      if (this.resigtrationInfoForm.value.registrationId == 0||this.resigtrationInfoForm.value.registrationId == null) {
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
debugger;
formData.append('RegistrationId', this.resigtrationInfoForm.value.registrationId);
formData.append('StudentId', this.resigtrationInfoForm.value.studentId);
formData.append('CourseId', this.resigtrationInfoForm.value.courseId);
formData.append('EnrollDate', this.resigtrationInfoForm.value.enrollDate);
formData.append('IsPaymentComplete', this.resigtrationInfoForm.value.isPaymentComplete);

    this.ResigtrationService.insert(formData).subscribe((data: ResponseModel) => {
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
    const formData: FormData = new FormData();

    formData.append('RegistrationId', this.resigtrationInfoForm.value.registrationId);
    formData.append('StudentId', this.resigtrationInfoForm.value.studentId);
    formData.append('CourseId', this.resigtrationInfoForm.value.courseId);
    formData.append('EnrollDate', this.resigtrationInfoForm.value.enrollDate);
    formData.append('IsPaymentComplete', this.resigtrationInfoForm.value.isPaymentComplete);

    this.ResigtrationService.update(formData).subscribe((data: ResponseModel) => {
      debugger;
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
      this.ResigtrationService.delete(id).subscribe(
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
    this.resigtrationInfoForm.reset();

    this.resigtrationInfoForm.get('studentId')?.setValue('');
    this.resigtrationInfoForm.get('courseId')?.setValue('');
    this.resigtrationInfoForm.get('enrollDate')?.setValue('');
    this.resigtrationInfoForm.get('isPaymentComplete')?.setValue('');

  }
  getAllStudent(){
    this.StudentInfoService.getAll().subscribe((data:any) => {
      this.studentList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })
  }
  getAllCourse(){
    this.CourseService.getAll().subscribe((data:any) => {
      this.courseList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })
  }
}
