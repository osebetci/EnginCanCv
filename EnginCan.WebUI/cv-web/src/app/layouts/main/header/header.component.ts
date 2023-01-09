import { Component, OnInit } from '@angular/core';
import Typed from 'typed.js';
declare var $: any;
@Component({
  selector: 'in-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    // Typed Initiate
    if ($('.typed-text-output').length == 1) {
      var typed_strings = $('.typed-text').text();
      var typed = new Typed('.typed-text-output', {
        strings: typed_strings.split(', '),
        typeSpeed: 100,
        backSpeed: 20,
        smartBackspace: false,
        loop: true,
      });
    }

  }

}
