import { Component, OnInit, ViewChild } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';
import { BlogPostsService } from '../_services/blogposts.service';
import { BlogPost } from '../_models/blogpost';
import { ThrowStmt } from '@angular/compiler';
import { AlertService } from '../_common/alert/alert.service';
import { ModalService } from '../_common/modal/modal.service';

@Component( {
    selector: 'blogposts-list',
    templateUrl: './blogposts-list.component.html',
    styleUrls: ['./blogposts-list.component.scss'],
})
export class BlogPostsListComponent implements OnInit {
    
    isLoading = false;
    blogPosts : BlogPost[] = [];
    model: any = {};
    isAdding = false;

    constructor(
        private readonly blogPostService: BlogPostsService,
        private readonly alertService: AlertService,
        private readonly modalService: ModalService,) {}

    
    ngOnInit(): void {
        this.isLoading = true;
        this.blogPostService.getBlogPosts().subscribe(
            (blogPosts: BlogPost[]) => {
                this.blogPosts = blogPosts;
                this.isLoading = false;
            },
            (error: any) => {
                this.isLoading = false;
                this.alertService.error(error);
            }
        );
    }

    cancelCreateBlogPost(): void {
        this.isLoading = false;
        this.isAdding = false;        
    }

    submitBlogPost(formName): void {
        this.isLoading = true;
        var blogPost = new BlogPost();
        blogPost.blogText = this.model.blogText;
        this.blogPostService.addBlogPost(blogPost).subscribe (
            (blogPost: BlogPost) => {
                this.blogPostService.getBlogPosts().subscribe(
                    (blogPosts: BlogPost[]) => {
                        this.blogPosts = blogPosts;
                        this.isLoading = false;
                        this.isAdding = false;  
                        formName.resetForm();
                    },
                    (error: any) => {
                        this.isLoading = false;
                        this.alertService.error(error);
                    }
                );
            },
            (error: any) => {
                this.isLoading = false;
                this.alertService.error(error);
            }
        );
    }

    createBlogPost() {
        this.isAdding = true;
    }

    deleteBlogPost(id: number): void {
        this.isLoading = true;
        this.blogPostService.deleteBlogPost(id).subscribe (
            () => {
                this.blogPostService.getBlogPosts().subscribe(
                    (blogPosts: BlogPost[]) => {
                        this.blogPosts = blogPosts;
                        this.isLoading = false;
                    },
                    (error: any) => {
                        this.isLoading = false;
                        this.alertService.error(error);
                    }
                );
            },
            (error: any) => {
                this.isLoading = false;
                this.alertService.error(error);
            }
        );
    }
}