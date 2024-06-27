import { Component, OnInit } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { Employee } from "../employee";
import { EmployeesService } from "../employees-service/employees.service";
import { Router, ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-employee-form',
  standalone: true,
    imports: [
        FormsModule,
        CommonModule
    ],
  templateUrl: './employee-form.component.html',
  styleUrl: './employee-form.component.css'
})
export class EmployeeFormComponent implements OnInit {
  employee : Employee = {
    id: 0,
    firstName: '',
    lastName: '',
    phone: '',
    position: '',
    email: ''
  };

  errorMessage: string = '';

  isEditing : boolean = false;

  ngOnInit() {
    this.activatedRoute.paramMap.subscribe((result) => {
      const id = result.get('id');

      if(id)
      {
        this.isEditing = true;
        this.employeesService.getEmployeeById(Number(id)).subscribe({
          next: (response) => this.employee = response,
          error: (err) => {
            console.log('Error loading employee', err);
            this.errorMessage = err.status ? `Error ${err.status}` : 'Unknown error';
          }
        });
      }
    });
  }

  constructor(
    private employeesService : EmployeesService,
    private router: Router,
    private activatedRoute: ActivatedRoute) {
  }

  addEmployee(employee: Employee) {
    this.employeesService.addEmployee(employee).subscribe();
  }

  updateEmployee(employee: Employee) {
    this.employeesService.updateEmployee(employee).subscribe();
  }

  onSubmit(): void {
    if(!this.isEditing){
      this.addEmployee(this.employee);
    }
    else{
      this.updateEmployee(this.employee);
    }

    this.clearForm();
    this.router.navigate(['/']);
  }

  clearForm() : void {
    this.employee = {
      id: 0,
      firstName: '',
      lastName: '',
      phone: '',
      position: '',
      email: ''
    };
  }
}
