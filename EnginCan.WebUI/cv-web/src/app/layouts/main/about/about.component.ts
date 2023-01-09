import { About } from './models/about.model';
import { Component, OnInit } from '@angular/core';
import { AboutService } from './services/about.service';

@Component({
  selector: 'in-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {

  about:About = new About();
  aboutId:number;
  constructor(private aboutService : AboutService) { }

  ngOnInit() {
    this.aboutId = 1;
    this.getAbout(this.aboutId);
  }

  getAbout(aboutId: number) {
    this.aboutService.getAbout(aboutId).subscribe((res) => {
      this.about = res.data;
     this.about.dogumTarih =  new Date(this.about.dogumTarih);
      
    });
  }

}
