$(document).ready(function () {  
    $(window).scroll(function () {
        var scroll = $(window).scrollTop();
        if (scroll >= 80) {
            $(".links-col").addClass("fixed");
        } else {
            $(".links-col").removeClass("fixed");
        }
    });
    if ($(".select2")) {
        if ($(".select2").length > 0) {
            $(".select2").select2();
        }
    }
    $('.noSearch select').select2({
        minimumResultsForSearch:-1
    });

    if ($(".js-select2")) {
        if ($(".js-select2").length > 0) {
            $(".js-select2").select2({
                closeOnSelect: false,
                placeholder: "Select warehouse",
                allowHtml: true,
                allowClear: true,
                tags: true // создает новые опции на лету
            });
        }
    }
});
$(window).scroll(function () {
    var scroll = $(window).scrollTop();

    if (scroll >= 40) {
        $("#sticky-breadcrumb-header").addClass("sticky");
    } else {
        $("#sticky-breadcrumb-header").removeClass("sticky");
    }
});
$.each($('.row-nav'), function () {
    var h = $(this).attr("data-height");
    $(this).css("height", h);
});
var sectionIds = {};

$(".row-nav").each(function () {
    var $this = $(this);
    sectionIds[$this.attr("id")] = $this.first().offset().top - 750;
});

var count2 = 0;
$(window).scroll(function (event) {

    var scrolled = $(this).scrollTop();

    //If it reaches the top of the row, add an active class to it
    $(".row-nav").each(function () {

        var $this = $(this);

        if (scrolled >= $this.first().offset().top - 750) {
            $(".row-nav").removeClass("active");
            $this.addClass("active");

            $(".animation").removeClass('animationActive');
            $this.find(".animation").addClass('animationActive');

        }
    });

    //when reaches the row, also add a class to the navigation
    for (key in sectionIds) {
        if (scrolled >= sectionIds[key]) {
            $(".nav-btn").removeClass("active");
            var c = $("[data-row-id=" + key + "]");
            c.addClass("active");

            var i = c.index();
            $('#nav-indicator').css('left', i * 100 + 'px');
        }
    }
});



/**************
 IN-NAVIGATION
**************/
$(".nav-btn").click(function () {
    $(this).addClass("active");
    $(this).siblings().removeClass("active");

    var i = $(this).index();
    $('#nav-indicator').css('left', i * 100 + 'px');

    var name = $(this).attr("data-row-id");
    var id = "#" + name;
    var top = $(id).first().offset().top - 60;
    $('html, body').animate({ scrollTop: top + 'px' }, 300);

});

$(function () {

    var link = $('#navbar a.dot');

    // Move to specific section when click on menu link
    link.on('click', function (e) {
        var target = $($(this).attr('href'));
        $('html, body').animate({
            scrollTop: target.offset().top - 60
        }, 600);
        //$(this).addClass('active');
        e.preventDefault();
    });

    // Run the scrNav when scroll
    $(window).on('scroll', function () {
        scrNav();
    });

    // scrNav function 
    // Change active dot according to the active section in the window
    function scrNav() {
        var sTop = $(window).scrollTop();
        $('section').each(function () {
            var id = $(this).attr('id'),
                offset = $(this).offset().top - 150,
                height = $(this).height();
            if (sTop >= offset && sTop < offset + height) {
                link.removeClass('active');
                $('#navbar').find('[data-scroll="' + id + '"]').addClass('active');
            }
        });
    }
    scrNav();
});
function moveScroller() {
    var $anchor = $("#scroller-anchor");
    var $scroller = $('#scroller');

    var move = function () {
        var st = $(window).scrollTop();
        if ($anchor.length) {
            var ot = $anchor.offset().top;
            if (st > ot) {
                $scroller.addClass("picked-fixed");
            } else {
                $scroller.removeClass("picked-fixed");
            }
        }
    };
    $(window).scroll(move);
    move();
}

$(function () {
    moveScroller();
});
function ScrollToTop() {
    window.scrollY != 0 && window.scrollTo(0, window.scrollY - 999999)
    var body = document.body;
    body.classList.add("overflow-hidden-body");
}
function removeScrollToTop() {
    window.scrollY != 0 && window.scrollTo(0, window.scrollY - 999999)
    var body = document.body;
    body.classList.remove('overflow-hidden-body');
}
