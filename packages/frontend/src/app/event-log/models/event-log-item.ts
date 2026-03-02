import { Severity } from "./severity.enum";

export interface EventLogItem {
  id: number;
  message: string;
  timestamp: Date;
  severity: Severity
}
