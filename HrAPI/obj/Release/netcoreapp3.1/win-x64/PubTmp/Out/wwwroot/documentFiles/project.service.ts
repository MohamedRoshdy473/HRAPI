import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { project } from '../Models/project'


@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private httpClient: HttpClient) { }
  httpHeader = {
    headers: new HttpHeaders({
      'content-type': 'application/json',
      'Accept': '*/*'
    })
  };

  GetAllProjects(): Observable<project[]> {
    return this.httpClient.get<project[]>(`${environment.project}`, this.httpHeader);
  }
  AddProject(project): Observable<project> {
    return this.httpClient.post<project>(`${environment.project}`,project, this.httpHeader);
  }
}