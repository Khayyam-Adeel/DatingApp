import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';

import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MembersListComponent } from './members/members-list/members-list.component';
import { MembersDetailsComponent } from './members/members-details/members-details.component';
import { ListComponent } from './list/list.component';
import { MessagesComponent } from './messages/messages.component';
import { SharedModule } from './_modules/shared.module';



@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MembersListComponent,
    MembersDetailsComponent,
    ListComponent,
    MessagesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
