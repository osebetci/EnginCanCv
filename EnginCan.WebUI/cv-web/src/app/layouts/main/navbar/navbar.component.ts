import { Component, OnInit } from '@angular/core';
import Typed from 'typed.js';
declare var $: any;
@Component({
  selector: 'in-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor() { }

  ngOnInit() {
     // Navbar on scrolling
     $(window).scroll(function () {
      if ($(this).scrollTop() > 200) {
        $('.navbar').fadeIn('slow').css('display', 'flex');
      } else {
        $('.navbar').fadeOut('slow').css('display', 'none');
      }
    });

    // Smooth scrolling on the navbar links
    $('.navbar-nav a').on('click', function (event) {
      if (this.hash !== '') {
        event.preventDefault();

        $('html, body').animate(
          {
            scrollTop: $(this.hash).offset().top - 45,
          },
          1500,
          'easeInOutExpo'
        );

        if ($(this).parents('.navbar-nav').length) {
          $('.navbar-nav .active').removeClass('active');
          $(this).closest('a').addClass('active');
        }
      }
    });

    
    // Scroll to Bottom
    $(window).scroll(function () {
      if ($(this).scrollTop() > 100) {
        $('.scroll-to-bottom').fadeOut('slow');
      } else {
        $('.scroll-to-bottom').fadeIn('slow');
      }
    });

  }

}
