import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WorkerListComponent } from './worker-list/worker-list.component';
import WorkersService from './shared/api/workers.service';
import { BySkillSearchComponent } from './by-skill-search/by-skill-search.component';
import { ByLocationSearchComponent } from './by-location-search/by-location-search.component';
import { WorkerDetailComponent } from './worker-detail/worker-detail.component';
import { SkillListComponent } from './skill-list/skill-list.component';
import { AddressDetailComponent } from './address-detail/address-detail.component';
import { WorkerDashboardComponent } from './worker-dashboard/worker-dashboard.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { ProfileComponent } from './profile/profile.component'

@NgModule({
  declarations: [
    AppComponent,
    WorkerListComponent,
    BySkillSearchComponent,
    ByLocationSearchComponent,
    WorkerDetailComponent,
    SkillListComponent,
    AddressDetailComponent,
    WorkerDashboardComponent,
    NavBarComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    WorkersService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
