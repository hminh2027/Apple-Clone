const swiper = new Swiper('.swiper', {
  // Responsive
  breakpoints: {
      320: {
        slidesPerView: 2,
        spaceBetween: 10
      },
      600: {
        slidesPerView: 2,
        spaceBetween: 30
      },
      992: {
        slidesPerView: 5,
        spaceBetween: 30
      }
  }
})

const related = new Swiper('#related-products', {
    // Responsive
    breakpoints: {
        320: {
            slidesPerView: 2,
            spaceBetween: 10
        },
        600: {
            slidesPerView: 2,
            spaceBetween: 30
        },
        992: {
            slidesPerView: 3,
            spaceBetween: 30
        }
    }
})