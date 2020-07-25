import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WorkerListComponent } from './worker-list/worker-list.component';
import WorkersService from './shared/api/workers.service'

@NgModule({
  declarations: [
    AppComponent,
    WorkerListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    WorkersService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
