import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ErrorsComponent } from './errors/errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { HomeComponent } from './home/home.component';
import { ListComponent } from './list/list.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MembersDetailsComponent } from './members/members-details/members-details.component';
import { MembersListComponent } from './members/members-list/members-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guard/auth.guard';

const routes: Routes = [
  {path:'', component:HomeComponent}, 
  {
    path:'',
    canActivate:[AuthGuard],
    runGuardsAndResolvers:'always',
    children:[
      {path:'members', component:MembersListComponent},
      {path:'member/:username', component:MembersDetailsComponent},
      {path:'member/Edit', component:MemberEditComponent},
      {path:'messages', component:MessagesComponent},
      {path:'lists', component:ListComponent},
      
    ]
},
{path:'error',component:ErrorsComponent},
{path:'not-found',component:NotFoundComponent},
{path:'server-error',component:ServerErrorComponent},
{path:'**', component:NotFoundComponent,pathMatch:'full'},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
