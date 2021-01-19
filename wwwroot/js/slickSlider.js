// * Slider Slick jQuery path of code
const slickSlider = () => {
    $(document).ready(function () {
        $(".sl").slick({
            arrows: false,
            fade: true,
            cssEase: 'linear',
            slidesToShow: 1,
            slidesToScroll: 1,
            autoplay: true,
            autoplaySpeed: 2000,
            speed: 1000,
            infinite: true
        });

        $("#singl1").fancybox({
            helpers: {
                title: {
                    type: 'float'
                }
            }
        });

        $("#singl2").fancybox({
            openEffect: 'elastic',
            closeEffect: 'elastic',

            helpers: {
                title: {
                    type: 'inside'
                }
            }
        });

        $("#singl3").fancybox({
            openEffect: 'none',
            closeEffect: 'none',
            helpers: {
                title: {
                    type: 'outside'
                }
            }
        });
    });
}

export default slickSlider;