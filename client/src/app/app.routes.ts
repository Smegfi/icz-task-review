import { Routes } from '@angular/router';
export const routes: Routes = [

    
    {
        path: 'backend',
        children: [
            {
                path: 'task',
                loadComponent: () => import('./pages/backend/task/task.page').then(m => m.TaskPage)
            },
        ]
    },
    {
        path: 'login',
        loadComponent: () => import('./pages/login/login.page').then(m => m.LoginPage)
    },{ path: '', redirectTo: 'login', pathMatch: 'full' },
]

