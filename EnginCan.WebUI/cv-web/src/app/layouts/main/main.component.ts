import { Component, OnInit, ViewChild } from '@angular/core';
import Typed from 'typed.js';
declare var $: any;
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent implements OnInit {
  constructor() {}

  ngOnInit() {
   
    // Skills
    $('.skill').waypoint(
      function () {
        $('.progress .progress-bar').each(function () {
          $(this).css('width', $(this).attr('aria-valuenow') + '%');
        });
      },
      { offset: '80%' }
    );

    // Portfolio isotope and filter
    var portfolioIsotope = $('.portfolio-container').isotope({
      itemSelector: '.portfolio-item',
      layoutMode: 'fitRows',
    });
    $('#portfolio-flters li').on('click', function () {
      $('#portfolio-flters li').removeClass('active');
      $(this).addClass('active');

      portfolioIsotope.isotope({ filter: $(this).data('filter') });
    });

    // Back to top button
    $(window).scroll(function () {
      if ($(this).scrollTop() > 200) {
        $('.back-to-top').fadeIn('slow');
      } else {
        $('.back-to-top').fadeOut('slow');
      }
    });
    $('.back-to-top').click(function () {
      $('html, body').animate({ scrollTop: 0 }, 1500, 'easeInOutExpo');
      return false;
    });

    // Testimonials carousel
    $('.testimonial-carousel').owlCarousel({
      autoplay: true,
      smartSpeed: 1500,
      dots: true,
      loop: true,
      items: 1,
    });
  }
}
