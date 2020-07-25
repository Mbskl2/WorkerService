/// <reference path="worker-list/worker-list.component.ts" />
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WorkerListComponent } from './worker-list/worker-list.component'
import { ByLocationSearchComponent } from './by-location-search/by-location-search.component'
import { BySkillSearchComponent } from './by-skill-search/by-skill-search.component'

const routes: Routes = [
  { path: 'worker-list', component: WorkerListComponent },
  { path: 'by-location-search', component: ByLocationSearchComponent},
  { path: 'by-skill-search', component: BySkillSearchComponent },
  { path: '', redirectTo: '/worker-list', pathMatch: 'full' }
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
