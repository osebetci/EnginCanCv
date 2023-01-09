import { ConfirmationDialogService } from './../../../../shared/services/confirmation-dialog.service';
import { UserService } from './../services/user.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

declare var $;
@Component({
  selector: 'app-tum-kullanicilar',
  templateUrl: './tum-kullanicilar.component.html',
  styleUrls: ['./tum-kullanicilar.component.scss'],
})
export class TumKullanicilarComponent implements OnInit {
  users: any[] = [];
  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private confirmationDialogService: ConfirmationDialogService
  ) {}

  ngOnInit() {
    this.uploadData();
  }

  delete(id) {
    if (id) {
      this.confirmationDialogService
        .confirm('İşlem Onayı', 'Kullanıcı silmek istediğinize emin misiniz ?')
        .then((confirmed) => {
          if (confirmed) {
            this.userService.deleteUser(id).subscribe((res) => {
              this.uploadData();
            });
          }      
        });
    }
  }

  uploadData() {
    var self = this;
    this.userService.getAllUser().subscribe((res) => {
      this.users = res.data;
      $('#jsGrid1').jsGrid({
        width: '100%',
        height: 300,
        sorting: true,
        paging: true,
        pageSize: 10,
        pageButtonCount: 5,
        deleteConfirm: 'Kullanıcıyı silmek istediğinize emin misiniz?',
        inserting: false,
        editing: false,
        data: this.users,
        viewrecords: true,
        gridview: true,
        autoencode: true,
        loadonce: true,
        fields: [
          {
            type: 'control',
            width: 60,
            editButton: false,
            deleteButton: false,
            title: 'İşlemler',
            itemTemplate: function (value, item) {
              var $iconPencil = $('<i>').attr({ class: 'fa fa-pencil p-2' });
              var $iconTrash = $('<i>').attr({ class: 'fa fa-trash p-2' });
  
              var $customEditButton = $('<button>')
                .attr({
                  class:
                    'btn btn-warning btn-xs mr-2 d-flex justify-content-center',
                })
                .attr({ role: 'button' })
                .attr({ title: 'Düzenle' })
                .attr({ id: 'btn-edit-' + item.id })
                .click((e) => {
                  document.location.href =
                    'yonetim/giris-islemleri/kullanici-islemleri/yeni-kullanici/' +
                    item.id;
                  e.stopPropagation();
                })
                .append($iconPencil);
  
              var $customDeleteButton = $('<button>')
                .attr({ class: 'btn btn-danger btn-xs' })
                .attr({ role: 'button' })
                .attr({ title: 'Sil' })
                .attr({ id: 'btn-delete-' + item.id })
                .click(function (e) {
                  self.delete(item.id);
                })
                .append($iconTrash);
  
              return $('<div>')
                .attr({ class: 'btn-toolbar' })
                .append($customEditButton)
                .append($customDeleteButton);
            },
          },
          {
            name: 'fullName',
            type: 'text',
            width: 150,
            title: 'Ad Soyad',
            autosearch: true,
          },
          { name: 'phoneNumber', type: 'text', width: 150, title: 'Telefon' },
          { name: 'email', type: 'text', width: 150, title: 'Mail' },
          {
            name: 'username',
            type: 'text',
            width: 150,
            title: 'Kullanıcı Adı',
          },
        ],
      });
    });
  
  
  }
}
