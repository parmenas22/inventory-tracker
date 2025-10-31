export interface ApiResponse<T = any> {
  succeeded: boolean;
  message: string;
  statusCode: number;
  error: any | null;
  value: T | null;
  timestamp: string;
}

export const authService = {};
