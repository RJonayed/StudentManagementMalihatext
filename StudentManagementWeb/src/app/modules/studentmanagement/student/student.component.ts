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
  selector: 'app-student',
  templateUrl: './student.component.html',
  styles: [
  ]
})
export class StudentComponent implements OnInit {
  filePath: string = "../../../../assets/dist/img/loding.gif";
  noImage:string = "../../../../assets/dist/img/noImage.png"
  fileToUpload: any;
  imageSource:string=Constants.API_KEY+"Images/student_Images/";
  public formSubmitAttempt: boolean= false;
  @ViewChild('closebutton') closebutton: any;
  constructor(private formBuilder: FormBuilder, private toastr: ToastrService, private StudentInfoService: StudentInfoService,private DepartmentService: DepartmentService) { }
  public studentList:studentInfoModel[]=[];
  public departmentList: departmentInfoModel[]=[];

  ngOnInit(): void {
    this.clearForm();
    this.getAll();
    this.getAllDepartment();

  }
  getAll(){
    this.StudentInfoService.getAll().subscribe((data: any) => {
      this.studentList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })
  }
  handleFileInput(e: any) {
    const reader = new FileReader();
    if (e?.target?.files && e?.target?.files.length) {
      this.fileToUpload = e?.target?.files[0];
      reader.readAsDataURL(this.fileToUpload);
      reader.onload = () => {
        this.filePath = reader.result as string;
        this.studentInformationForm.patchValue({
          fileSource: reader.result
        });

      };
    }
  }

  public studentInformationForm = this.formBuilder.group({
    studentId: [0,],
    studentName: ['', Validators.required],
    departmentId: ['', Validators.required],
    dateOfBirth: ['', Validators.required],
    imageName: ['']
  })
  get studentName() { return this.studentInformationForm.get("studentName") };
  get departmentId() { return this.studentInformationForm.get("departmentId") };
  get dateOfBirth() { return this.studentInformationForm.get("dateOfBirth") };
  get deptName() { return this.studentInformationForm.get("deptName") };

  pupulateForm(selectedRecord: studentInfoModel) {
    this.studentInformationForm.patchValue({
      studentId: selectedRecord.id,
      studentName: selectedRecord.name,
      departmentId: selectedRecord.deptId,
      dateOfBirth: selectedRecord.dateOfBirth,
      imageName: selectedRecord.imagePath,

    });
    if(selectedRecord.imagePath!=null ){
      this.filePath = this.imageSource + selectedRecord.imagePath;
    }
    else{
      this.filePath= "../../../../assets/dist/img/loding.gif";
    }
  }

  onSubmit() {
    debugger;
    if(this.studentInformationForm.valid){
      if (this.studentInformationForm.value.studentId == 0||this.studentInformationForm.value.studentId == null) {
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
    formData.append('Id', this.studentInformationForm.value.studentId);
    formData.append('Name', this.studentInformationForm.value.studentName);
    formData.append('DeptId', this.studentInformationForm.value.departmentId);
    formData.append('DateOfBirth', this.studentInformationForm.value.dateOfBirth);
    formData.append('Photo', this.fileToUpload);
    formData.append("ImagePath", this.studentInformationForm.value.imageName);

    this.StudentInfoService.insert(formData).subscribe((data: ResponseModel) => {
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

    formData.append('Id', this.studentInformationForm.value.studentId);
    formData.append('Name', this.studentInformationForm.value.studentName);
    formData.append('DeptId', this.studentInformationForm.value.departmentId);
    formData.append('DateOfBirth', this.studentInformationForm.value.dateOfBirth);
    formData.append('Photo', this.fileToUpload);
    formData.append("ImagePath", this.studentInformationForm.value.imageName);

    this.StudentInfoService.update(formData).subscribe((data: ResponseModel) => {
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
      this.StudentInfoService.delete(id).subscribe(
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
    this.studentInformationForm.reset();
    this.filePath= "../../../../assets/dist/img/loding.gif";
    this.formSubmitAttempt=false;

    this.fileToUpload='';
    this.studentInformationForm.get('studentId')?.setValue('');
    this.studentInformationForm.get('StudentName')?.setValue('');
    this.studentInformationForm.get('departmentId')?.setValue('');
    this.studentInformationForm.get('dateOfBirth')?.setValue('');

  }
  getAllDepartment(){
    this.DepartmentService.getAll().subscribe((data:any) => {
      this.departmentList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })
  }
}
