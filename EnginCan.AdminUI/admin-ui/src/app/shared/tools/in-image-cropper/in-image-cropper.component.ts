import { ToastrService } from 'ngx-toastr';
import { Component, ElementRef, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DocsService } from '../../services/docs.service';
import { HelpersService } from '../../services/helpers.service';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'in-image-cropper',
  templateUrl: './in-image-cropper.component.html',
  styleUrls: ['./in-image-cropper.component.scss']
})
export class InImageCropperComponent implements OnInit {
  @ViewChild('imageInput', { static: false }) imageInput: ElementRef;
  @ViewChild('content', { read: TemplateRef }) content:TemplateRef<any>;

  selectedValue: string = '';
  @Output() valueChange = new EventEmitter();
  @Input()
  get value(): string {
    return this.selectedValue;
  }
  set value(val: string) {
    this.selectedValue = val;
    if (val) {
      this.fileName = 'Fotoğraf';
    } else {
      this.fileName = undefined;
    }
  }

  @Input() accept: string = 'image/x-png,image/jpeg,image/jpg';
  @Input() popupTitle: string = 'Fotoğraf Hazırla';
  @Input() required: boolean = false;
  @Input() readonly: boolean = false;
  @Input() requiredMessage: string = 'Gerekli';
  @Input() aspectRatio: number = 3 / 4;
  @Input() enableAspectRatio: boolean = true;
  @Input() resizeToWidth: number;
  @Input() quality: number = 1;
  @Input() format: 'png' | 'jpeg' | 'bmp' | 'webp' | 'ico' = 'jpeg';
  @Input() popupWidth: number | string = 400;
  @Input() popupHeight: number | string = 527;
  @Input() openHelper: boolean = false;
  @Input() helperData: HelperData = new HelperData();
  
  imageChangedEvent: any = '';
  croppedImage: any = '';
  cropperReady = false;
  fileName: string;

  popupValue: string;
  popupExtension: string;
  popupVisible: boolean = false;
  environment = environment;
  closeResult = '';
  modal;
  constructor( private helpersService: HelpersService,
    private docsService: DocsService, private toastrService:ToastrService,private modalService: NgbModal) { }

  ngOnInit() {
  }

  clear() {
    this.selectedValue = null;
    this.valueChange.emit(this.selectedValue);
  }

  fileChangeEvent(event: any): void { 
    if (event.target.files[0]) {
      this.fileName = event.target.files[0].name;
    }
    
    this.imageChangedEvent = event;
    this.modalService.open(this.content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  imageCropped(event) {
    this.croppedImage = event.base64;
    console.log(this.croppedImage,"sa2");
  }

  imageLoaded() {
    this.cropperReady = true;
  }

  saveCroppedImage() {
    this.imageInput.nativeElement.value = '';
    console.log(this.croppedImage,"sa");
    
    this.helpersService
      .base64ImageCompressor(this.croppedImage, this.quality, this.format)
      .then((croppedImage) => {
        this.docsService.postUploadImage(croppedImage).subscribe((res) => {
          if (res.fileName) {
            this.selectedValue = res.fileName;
            this.valueChange.next(res.fileName);
            this.valueChange.emit(res.fileName);
           
          } else {
            this.toastrService.error("Yükleme sırasında bir hata oluştu. Resim sisteme yüklenemedi.","Hata");      
          }
        });
      });
  }

  showFile() {
    if (this.selectedValue && this.accept) {
      this.popupValue = this.selectedValue;
      this.popupExtension = this.accept;
    }
  }

  close() {
    if (!this.selectedValue) {
      this.fileName = undefined;
    }
    this.imageInput.nativeElement.value = '';
    this.popupVisible = false;
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }


}

export class HelperData {
  photoWidth: string;
  photoHeight: string;
  faceCircle: string;
  faceCircleDot: string;
}

