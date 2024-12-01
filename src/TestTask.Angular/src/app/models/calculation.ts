export class Calculation{
  constructor(
    public grossAnnualSalary: number,
    public grossMonthlySalary: number,
    public netAnnualSalary: number,
    public netMonthlySalary: number,
    public annualTaxPaid: number,
    public monthlyTaxPaid: number,
  ){
  }
};

export class CreateCalculationRequest{
  constructor(public grossAnnualSalary: number) {
  }
}