export * from './auth.service';
import { AuthApiService } from './auth.service';
export * from './auth.serviceInterface';
export * from './task.service';
import { TaskApiService } from './task.service';
export * from './task.serviceInterface';
export const APIS = [AuthApiService, TaskApiService];
