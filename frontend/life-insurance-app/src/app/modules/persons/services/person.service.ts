import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { mapTo, Observable } from 'rxjs';

export interface Person {
  id: string;
  fullName: string;
  identification: string;
  age: number;
  gender: string;
  isActive: boolean;
  drives: boolean;
  usesGlasses:boolean,
  isDiabetic:boolean,
  otherDiseases:string
}

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  private apiUrl = 'https://localhost:7016/api/persons';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Person[]> {
    return this.http.get<Person[]>(this.apiUrl);
  }

  getById(id: string): Observable<Person> {
    return this.http.get<Person>(`${this.apiUrl}/${id}`);
  }

  create(person: Person): Observable<void> {
    return this.http.post<string>(this.apiUrl, person).pipe(mapTo(undefined));
  }

  update(id: string, person: Person): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, person);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
