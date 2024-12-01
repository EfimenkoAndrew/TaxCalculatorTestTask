import { Component, OnInit, NgModule } from '@angular/core';
import { FormGroup, FormBuilder, ReactiveFormsModule, FormsModule, UntypedFormGroup, Validators, UntypedFormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { CalculationsService } from '../services/calculations.service';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { Calculation, CreateCalculationRequest } from '../models/calculation';

@Component({
  selector: 'app-calculate',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule,
    FormsModule,
  ],
  templateUrl: './calculate.component.html',
  styleUrl: './calculate.component.sass'
})

export class CalculateComponent implements OnInit {
  public calculateForm!: UntypedFormGroup;
  public calculation: Calculation | null = null;
  
  constructor(private fb: FormBuilder, private calculationsService: CalculationsService) {
  }

  public ngOnInit(): void {
    this.calculateForm = new UntypedFormGroup({
      grossAnnualSalary: new UntypedFormControl(null, [Validators.required, Validators.min(1)]),
     });
  }

  public onSubmit(): void {
    const createCommand: CreateCalculationRequest = this.calculateForm.getRawValue();
    if(this.calculateForm.invalid) { return; }
    this.calculationsService.creataCalculation(createCommand).subscribe(
      (responce: Calculation) => {
        this.calculation = responce;
      },
      (error: any) => {
        this.calculation = null;
        console.error(error);
      }
    );
  }
}
