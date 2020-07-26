/// <reference path="worker-list/worker-list.component.ts" />
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { WorkerDashboardComponent } from './worker-dashboard/worker-dashboard.component'
import { ByLocationSearchComponent } from './by-location-search/by-location-search.component'
import { BySkillSearchComponent } from './by-skill-search/by-skill-search.component'
import { ProfileComponent } from './profile/profile.component';
import { AuthGuard } from './auth.guard';
import { InterceptorService } from './interceptor.service';

const routes: Routes = [
  { path: 'worker-dashboard', component: WorkerDashboardComponent, canActivate: [AuthGuard] },
  { path: 'by-location-search', component: ByLocationSearchComponent, canActivate: [AuthGuard] },
  { path: 'by-skill-search', component: BySkillSearchComponent, canActivate: [AuthGuard] },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: InterceptorService, multi: true }
  ]
})
export class AppRoutingModule { }
