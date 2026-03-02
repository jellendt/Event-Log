export enum Severity {
  Low = 0,
  Medium = 1,
  High = 2
}

export const SeverityLabels: Record<Severity, string> = {
  [Severity.Low]: 'Low',
  [Severity.Medium]: 'Medium',
  [Severity.High]: 'High'
};
