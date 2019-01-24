import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { LoadingModule } from 'ngx-loading';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AlertModule } from './_common/alert/alert.module';
import { ModalModule } from './_common/modal/modal.module';
import { ConfirmationDialogModule } from './_common/dialogs/confirmation/confirmation-dialog.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BlogPostsListComponent } from './blogposts/blogposts-list.component';
import { BlogPostsService } from './_services/blogposts.service';
import { WindowRef } from './_common/window-ref/window-ref';

@NgModule({
  declarations: [
    AppComponent,
    BlogPostsListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LoadingModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AlertModule,
    ModalModule,
    ConfirmationDialogModule.forRoot()
  ],
  providers: [
    BlogPostsService,
    WindowRef
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

