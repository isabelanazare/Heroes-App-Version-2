import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-unauthorised-page',
  templateUrl: './unauthorised-page.component.html',
  styleUrls: ['./unauthorised-page.component.css'],
})
export class UnauthorisedPageComponent implements OnInit {
  public returnUrl: string;

  constructor(private route: ActivatedRoute, private router: Router) {}

  public ngOnInit(): void {
    this.route.queryParams
      .pipe(filter((params) => params.returnUrl))
      .subscribe((params) => {
        this.returnUrl = params.returnUrl;
      });
  }

  public goBack(): void {
    this.router.navigateByUrl(this.returnUrl);
  }
}
