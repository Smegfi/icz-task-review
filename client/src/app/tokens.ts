import { InjectionToken } from "@angular/core";
import { TaskApiServiceInterface } from './api-client';


export const TaskApiServiceInterfaceToken = new InjectionToken<TaskApiServiceInterface>('TaskApiServiceInterface');