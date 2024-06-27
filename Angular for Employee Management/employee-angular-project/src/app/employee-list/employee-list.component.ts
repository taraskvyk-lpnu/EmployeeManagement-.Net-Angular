import { Component, OnInit } from '@angular/core';
import { Employee } from '../employee';
import { CommonModule } from '@angular/common';
import { EmployeesService } from '../employees-service/employees.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.css'
})
export class EmployeeListComponent implements OnInit{
  employees: Employee[] = [];

  constructor(private employeesService : EmployeesService, private router: Router) {}

  ngOnInit(): void {
    this.getEmployees();
  }

  getEmployees() {
    this.employeesService.getEmployees().subscribe((employees) => {
      this.employees = employees;
    });
  }

  deleteEmployee(employee: Employee) {
    this.employeesService.deleteEmployee(employee.id).subscribe({
      next: (response) => {
        this.employees = this.employees.filter(emp => emp.id !== employee.id);
      },
      error: (err) => {
        console.log('Error while deleting', err);
      }
    });
  }

  updateEmployee(Id: number) {
    // redirecting to upsert form and passing the employeeToUpdate
    this.router.navigate(['update', Id]);
  }
}
