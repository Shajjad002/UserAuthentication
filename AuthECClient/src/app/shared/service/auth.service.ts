import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpClient) { }
  baseUrl:string = 'https://192.168.11.62:45459/api';

  createUser(formData:any) {
    return this.http.post(`${this.baseUrl}/signup`, formData);
  }
}
