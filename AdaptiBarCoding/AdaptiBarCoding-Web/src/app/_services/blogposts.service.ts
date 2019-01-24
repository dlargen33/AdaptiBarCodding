import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { BlogPost } from '../_models/blogpost';
import { DeleteBlogPostInfo } from '../_models/deleteBlogPostInfo'

@Injectable()
export class BlogPostsService {

    private readonly apiController: string;

    constructor(private readonly httpClient: HttpClient) {
        this.apiController = `${environment.webApiUrl}/api/BlogPost`;
    }

    getBlogPosts(): Observable<BlogPost[]> {
        const httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

        return this.httpClient.get<BlogPost[]>(`${this.apiController}/GetBlogPosts`, { headers: httpHeaders })
            .pipe(map(
                    (posts: BlogPost[]) => {
                        return posts;
                    }
                )
            );         
    }

    deleteBlogPost(blogPostId: number): Observable<any> {
        const httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
        return this.httpClient.post<any>(`${this.apiController}/DeleteBlogPost?blogPostId=${blogPostId}`, {headers: httpHeaders});
    }

    addBlogPost(blogPost: BlogPost): Observable<BlogPost>{
        const httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
        return this.httpClient.post<any>(`${this.apiController}/AddBlogPost`, blogPost,  {headers: httpHeaders})
            .pipe(map(
                (post: BlogPost) => {
                    return post;
                }
            )
        );
    }
}