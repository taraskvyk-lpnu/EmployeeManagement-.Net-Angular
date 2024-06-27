import { Injectable } from '@angular/core';
import { Employee } from '../employee';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
  private apiUrl = 'https://localhost:7147/api/employees';
  constructor(private httpClient : HttpClient) { }

  getEmployees(): Observable<Employee[]> {
    return this.httpClient.get<Employee[]>(this.apiUrl);
  }

  getEmployeeById(id: number): Observable<Employee> {
    return this.httpClient.get<Employee>(`${this.apiUrl}/${id}`);
  }

  addEmployee(employee: Employee): Observable<Employee> {
    return this.httpClient.post<Employee>(this.apiUrl, employee);
  }

  updateEmployee(employee: Employee): Observable<Employee> {
    return this.httpClient.put<Employee>(`${this.apiUrl}/${employee.id}`, employee);
  }

  deleteEmployee(id: number): Observable<Employee> {
    return this.httpClient.delete<Employee>(`${this.apiUrl}/${id}`);
  }
}