import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CustomvalidationService } from 'src/app/shared/services/custom-validation.service';
import { environment } from 'src/environments/environment';
import { About } from './models/about.model';
import { AboutService } from './services/about.service';
import {DatePipe, formatDate} from '@angular/common';
import { DataStatus } from 'src/app/shared/models/base-entity.model';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss'],
  providers: [DatePipe]

})
export class AboutComponent implements OnInit {
  about: About = new About();
  environment = environment;
  submitted = false;
  aboutForm: FormGroup;
  aboutId: number;
  constructor(
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private aboutService: AboutService,
    private activatedRoute: ActivatedRoute,
    private route: ActivatedRoute,
    private router: Router,
    private customValidator: CustomvalidationService,
    public datepipe: DatePipe
  ) {}

  ngOnInit() {
    this.aboutId = 1;
    this.getAbout(this.aboutId);
    this.createAboutUpdateForm();
  }

  get formControl() {
    return this.aboutForm.controls;
  }

  saveAbout() {
    this.submitted = true;
    if (this.aboutForm.valid) {
      let model: About = Object.assign({}, this.aboutForm.value);
      model.dogumTarih = new Date(model.dogumTarih);
      model.dataStatus = DataStatus[DataStatus.Activated];
      if (this.about.id) {
        this.updateAbout(model);
      } else {
        this.addAbout(model);
      }
    } else {
      this.toastr.error('Zorunlu alanları kontrol ediniz!', 'Hata');
    }
  }

  addAbout(model) {
    this.aboutService.postAbout(model).subscribe((res) => {
      if (res.success) {
        this.toastr.success(res.messages, 'Başarılı!');
        this.routeChange();
      } else {
        this.toastr.error(res.messages, 'Hata!');
      }
    });
  }
  updateAbout(model) {
    this.aboutService.updateAbout(model).subscribe((res) => {
      if (res.success) {
        this.toastr.success(res.messages, 'Başarılı!');
        this.routeChange();
      } else {
        this.toastr.error(res.messages, 'Hata!');
      }
    });
  }

  createAboutAddForm() {
    this.aboutForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      dogumTarih: ['', Validators.required],
      mezuniyetDurum: ['', Validators.required],
      deneyimSuresi: ['', Validators.required],
      email: ['', Validators.required],
      ozetMetin: ['', [Validators.required]],
      // photo:['',Validators.required]
    });
  }

  createAboutUpdateForm() {
    this.aboutForm = this.formBuilder.group({
      id:this.aboutId,
      fullName: ['', Validators.required],
      dogumTarih: ['', Validators.required],
      mezuniyetDurum: ['', Validators.required],
      deneyimSuresi: ['', Validators.required],
      email: ['', Validators.required],
      ozetMetin: ['', [Validators.required]],
      // photo:['',Validators.required]
    });
  }

  routeChange() {
    this.router.navigate(['/yonetim/hakkimda']);
  }

  getAbout(aboutId: number) {
    this.aboutService.getAbout(aboutId).subscribe((res) => {
      this.about = res.data;
      this.about.dogumTarih = this.datepipe.transform(this.about.dogumTarih, 'dd-MM-yyyy')
      console.log( this.about.dogumTarih);
    });
  }
}
