import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Calculation, CreateCalculationRequest } from '../models/calculation';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class CalculationsService {
  private endpoint: string = environment.apiBaseUrl;
  constructor(private httpClient: HttpClient) { }
  public creataCalculation(data: CreateCalculationRequest): Observable<Calculation> {
    return this.httpClient.post<Calculation>( `${this.endpoint}/calculations`, data);
  }
}

