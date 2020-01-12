/*
* Plugin Name: Themeum Core
* Plugin URI: http://www.themeum.com/item/core
* Author: Themeum
* Author URI: http://www.themeum.com
* License - GNU/GPL V2 or Later
* Description: Themeum Core is a required plugin for this theme.
* Version: 1.0
*/


var completeLoadingSliders=false;
var isLoadingPopular=false;
var startAppendGroup=false;
var data;
var currentRequest;
jQuery(document).ready(function ($) {
    'use strict';
     getHomeSliders();
     //-------------------------------Start owl year slider----------------------------------
    $(document).on("change", ".dropdown-select", function () {

        var selectedText = $(this).val();
        var year = parseInt(selectedText, 10);
        var owlObject = $('option:selected', this).attr('data-owl');

        initYearly(owlObject, year)
    });
});

 function makeRequest(settings) {
      // body...
    $.fn.hasScrollBar = function() {
        return this.get(0).scrollHeight > this.height();
    }  
    completeLoadingSliders=false;
    startAppendGroup=false;
    data;
    $.ajax(settings)
        .done(function (response) {
            $("#loadMoreSlidersGifImg").hide()  
            data=response;
            var key = Object.keys(data['group'])[0];
            var sliderType = data['group'][key]['type'];
            var htmlSection = buildHtmlSlider(data, key);

            init(data, htmlSection, sliderType, key);
            completeLoadingSliders=true;
            if(data['group'][key]!=undefined)
               delete data['group'][key];
            
            
            var currnetInterval=setInterval(function(){

                if($('body').hasScrollBar()==false)
                 {
                    //alert(false)
                     if(completeLoadingSliders==true)
                    {
                        getSlider(false);
                    }    
                  
                 }else{
                    clearInterval(currnetInterval);

                 }   

              },1000)    
              $(window).scroll(function () {

                
                if(completeLoadingSliders==true)
                {
                    getSlider(true);
                    
                }
                if(isLoadingPopular==false && isEmpty(data["group"]))
                {
                 
                    isLoadingPopular=true;
                    getPopularSldiers();
                }
                

              
            });

            
        });

  }   
function isEmpty(obj) {
    for(var key in obj) {
        if(obj.hasOwnProperty(key))
            return false;
    }
    return true;
}
function getHomeSliders() {
     $("#loadMoreSlidersGifImg").show()
     var settings = {
        //url: "/page/home/InActiveCustomList/" + getLang(),
        url: "/page/home/Sliders/" + getLang(),
        method: 'GET'
    };
     currentRequest="Sliders";
     makeRequest(settings);
}

function loadMoreSldiers(){
    $("#loadMoreSlidersGifImg").show()
    $("#loadMoreSldiers").hide();
    var settings = {
                        url: "/page/home/InActiveCustomList/" + getLang(),
                        method: 'GET'
                        };
    currentRequest="InActiveCustomList";                    
    makeRequest(settings);
}

function getPopularSldiers(){
     $("#loadMoreSlidersGifImg").show()
    var settings = {
                        url: "/page/home/PopularSliders/" + getLang(),
                        method: 'GET'
                    };
    currentRequest="PopularSliders";                
    makeRequest(settings);
   
}
//End owl year slider
function getSlider(hasScrollBar){
                  
                    var scrollTop = $(document).scrollTop();
                    var windowHeight = $(window).height();
                    var bodyHeight = $(document).height() - windowHeight;
                    var scrollPercentage = (scrollTop / bodyHeight);
                    if (startAppendGroup==false && (scrollPercentage > 0.6 || hasScrollBar==false) && (Object.keys(data['group']).length > 0)) {
                        startAppendGroup=true;
                       
                        key = Object.keys(data['group'])[0];
                        htmlSection = buildHtmlSlider(data, key);
                        sliderType = data['group'][key]['type'];
                        if (data['group'][key]['option']!=undefined) {

                            getSliderOptions(data['group'][key]['option']).done(function (options) {

                            var htmlSliderOption = optionDropDownBuilder(options, key);
                            htmlSection = htmlSection.replace('<div class="container_custom">', '<div class="container_custom">' + htmlSliderOption);
                            init(data, htmlSection, sliderType, key);
                            delete data['group'][key];
                            startAppendGroup=false;
                            });

                        } else {

                            init(data, htmlSection, sliderType, key);
                            delete data['group'][key];
                            startAppendGroup=false;
                        }
                    }else{
                        if(currentRequest=="PopularSliders" && isEmpty(data["group"]))
                        {
                            $("#loadMoreSldiers").show(); 
                        }
                    }
}
//End owl year slider

function init(data, htmlSection, sliderType, key) {
    var url;

    $('.section:last').length ?
        $('.section:last').after(htmlSection) :
        $('#sliders').prepend(htmlSection);
    var lang = getLang();
    
    if (data['group'][key]['content'][0]['list_id'] == "10") {

        url = "/page/user/history/"+lang +"/1";
        $(".effect-underline:last").attr('href', url);

    }else if (data['group'][key]['content'][0]['list_id']) {

        url = "/page/movie/videoCollection/"+lang +"?kind=" + data['group'][key]['content'][0]['kind']
            + "&listID=" + data['group'][key]['content'][0]['list_id'] + "&mDate=2018";
        $(".effect-underline:last").attr('href', url);
    
    } else {
        $("a.effect-underline:last").removeClass("effect-underline");
    }
    var carouselitem = buildCarouselCards(key, data['group'][key]['content'], data['group'][key]['content'][0]['list_id']);
    $("#" + key + "").append(carouselitem);
    $("." + key + "").attr("data-type", sliderType);
    var $spmvmovie = $("." + key + "");
    if (sliderType != 'popular') {
        $spmvmovie.on('changed.owl.carousel', function (e) {
            if (e.item.count != 0) {
                if ((e.item.count) <= (e.item.index + e.relatedTarget.settings.items + 4)) {
                    var page = parseInt($(this).attr('data-page'), 10);
                    url = getUrlPagination($(this),page,$(this).attr('data-id'));
                    owlPagination(url, e);
                    $(this).attr('data-page', ++page);
                }
            }
        });
    }
    
    initCarousel($spmvmovie);
    $('.' + key + "slider").css({ opacity: 0, visibility: "visible" }).animate({ opacity: 1.0 }, 500)

}


function getUrlPagination(carousel,page,dataID) {
    var url;
    if (carousel.attr('data-type') == 'custom') {
        url = paginationCustomSlide('paginationCustomList', page, dataID)
    } else if (carousel.attr('data-type') == 'yearly') {
        url = paginationYearlySection(carousel.attr('data-api'), 2018, page);
    } else if (carousel.attr('data-type') == 'default') {
        url = paginationDefaultSlide(carousel.attr('data-api'), page)
    }
    return url;
}
function getLang() {
    var isRtl = location.pathname.search('ar');
    var lang;
    if (isRtl === -1) {
        lang = "en";
    } else {
        lang = "ar";
    }
    return lang;
}

function buildHtmlSlider(data, owl) {

    var SliderInfo = data['group'][owl]
    var sectionId = SliderInfo['content'][0]['list_id'];
    var sectionName = SliderInfo['name'];
    var section = '<section class="section  main bg_color transition-cnt" id="section_' + owl + '" title="' + sectionName + '" style="background: #191919;padding:0">'

        + '<h1 text-align="left">'
        + '<a  class="effect-underline video-collection">'
        + sectionName
        + '</a>'
        + '</h1>'

        + '<div class="container_custom">'
        + '<div id="post-68" class="post-68 page type-page status-publish hentry analytics" '
        + 'data-analytics="|eventID=5|eventInt=' + sectionId + '">'
        + '<div class="entry-content">'
        + '<div data-vc-full-width="true" data-vc-full-width-init="false" data-vc-stretch-content="true" class="vc_row wpb_row vc_row-fluid container-fullwidth vc_custom_1455700416777 vc_row-no-padding">'
        + '<div class="wpb_column vc_column_container vc_col-sm-12">'
        + '<div class="vc_column-inner vc_custom_1455700439913">'
        + '<div class="wpb_wrapper">'
        + '<div id="moview-movie" style="visibility: hidden;" class="moview-movie-featured slideContainer ' + owl + 'slider">'
        + '<div id="' + owl + '" class="row-fluid"></div>'
        + '</div></div></div></div></div></div></div></div></section> ';
    return section


}

function optionDropDownBuilder(options, owl) {
    var element = '';
    element = '<div  class="dropdown-option dropdown-dark">'
        + '<select name="two" class="dropdown-select" id="select_' + owl + '">'
        + '<option selected="selected"  value="">Select Year…</option>'
    Object.keys(options).forEach(function (key) {

        element += '<option data-owl="' + owl + '" value="' + options[key]['value'] + '">' + options[key]['value'] + '</option>';
    });
    element += '</select>'
    element += '</div>'

    return element;
}
function getSliderOptions(sliderInfo) {
    return $.ajax({
        url: "/page/home/silderOptions/" + sliderInfo[0]['id'],
        method: 'GET',
    });
}

//Start owl pagination function
function paginationYearlySection(item, year, page) {
    var url = "/api/android/" + item + "/page/" + page + "/year/" + year;
    return url;
}

function paginationCustomSlide(item, page, listID) {
    var url = "/api/android/" + item + "/page/" + page + "/groupID/" + listID;
    return url;
}

function paginationDefaultSlide(item, page) {
    var url = "/api/android/" + item + "/page/" + page;
    return url;

}

function owlPagination(urlParam, e) {

    var isRtl = location.pathname.search('ar');
    var lang = "en";
    if (isRtl === -1) {
        lang = "en";
    } else {
        lang = "ar";
    }

    $.ajax({
        url: urlParam,
        success: function (result) {
            for (var i in result) {
                var itemBuild = '<div class="owl-item ">'
                var videoTitle = "";
                if (result[i].kind == 2) {
                    if (lang == "ar") {
                        videoTitle = result[i].ar_title;
                    }
                    else {
                        videoTitle = result[i].en_title;
                    }
                }
                else {
                    if (lang == "ar") {
                        videoTitle = result[i].ar_title
                    }
                    else {
                        videoTitle = result[i].en_title
                    }
                }
                if ($.cookie("token")) {
                    var notifyClass = "notify";
                }
                itemBuild += '<div class="item analytics ' + notifyClass + ' "  data-analytics="|eventID=6|eventInt=1|videoID=' + result[i].nb + '|">'
                itemBuild += '<div class="featuredContent">'
                itemBuild += '<a id="' + result[i].nb + '" class="cast watchLaterBtnV2  analytics" data-analytics="|eventID=6|eventInt=2|videoID=' + result[i].nb + '|">'
                itemBuild += '<i class="themeum-moviewclock "></i>'
                itemBuild += '</a></div>'
                itemBuild += '<div class="movie-poster"><a><img  class="item-responsive" src="' + result[i].imgMediumThumbObjUrl + '" alt=""></a></div>'
                itemBuild += '<a href="/page/movie/watch/' + lang + '/' + result[i].nb + '" class="play-icon" onclick="event.stopPropagation();" window.event.cancelBubble = "true">'
                itemBuild += '<i class="themeum-moviewplay" style="font-size: 40px;">'
                itemBuild += '</i>'
                itemBuild += '</a>'
                itemBuild += '<div class="content-wrap">'
                itemBuild += '<div class="video-container">'
                itemBuild += '</div>'
                itemBuild += '</div>'
                itemBuild += '<div class="movie-details" style="text-align:left;">'
                if (result[i].parent_skipping == '1') {
                    itemBuild += '<div class="moview-rating-wrapper">'
                    itemBuild += '<span class="moview-rating-summary">'
                    itemBuild += '<span class="parentalResponsive" ></span>'
                    itemBuild += '</span>'
                    itemBuild += '</div>'
                }
                itemBuild += '<div class="movie-name">'
                itemBuild += '<h2 class="movie-title" style="font-size: 14px"><a>' + videoTitle + '</a></h2>'
                itemBuild += '</div>'
                itemBuild += '<div class="moview-rating-wrapper">'
                itemBuild += ' <div class="moview-rating">'
                itemBuild += '<span class="star active" style="padding:0px"></span>'
                itemBuild += '</div>'
                itemBuild += '<span class="moview-rating-summary" style="padding:0px">'
                itemBuild += '<span>' + result[i].stars + '</span>'
                itemBuild += '/10</span>'
                itemBuild += '</div>'
                itemBuild += '</div>'
                itemBuild += '</div>'
                e.relatedTarget.add($(itemBuild))
            }
            e.relatedTarget.update();

        }
    });
}

//-------------------------------End pagination owl--------------------------------------
//-------------------------------Start owl reinitial popular-----------------------------------------
$(document).on("click", 'a[data-owl="popularFilms"],a[data-owl="popularForeignSeries"],a[data-owl="popularArabicSeries"]', function () {
    element = 'a[data-owl="' + $(this).attr("data-owl") + '"]';
    $(element).removeClass('ActiveTab');
    $(this).addClass(' ActiveTab');
    var owl = $(this);
    var owlObject = $(this).attr('data-owl');
    if (owlObject == null) {
        return;
    }
    initPopular(owlObject, owl);
});

//---------------------------Start Yearly Carousel---------------------------------------
function initYearly(owlObject, owl) {
    $("#" + owlObject).html("");

    if ($("#" + owlObject + "").html().replace(/\s+/g, '') == "") {
        var baseUrl = "/api/android/" + owlObject + "/year/" + owl + "";
        $("#" + owlObject + "").append('<img id="owloadgif" style="width:3%" alt="blankimage" src="/loadingImages/loader.gif">')
        $.ajax({
            url: baseUrl, success: function (result) {
                $("#owloadgif").remove()
                var itemBuild = buildCarouselCards(owlObject, result);
                $("#" + owlObject + "").append(itemBuild);
                //Featured Movie
                var $spmvmovie = $("." + owlObject + "");
                $spmvmovie.on('changed.owl.carousel', function (e) {
                    if (e.item.count != 0) {
                        if ((e.item.count) <= (e.item.index + e.relatedTarget.settings.items + 4)) {
                            var page = parseInt($(this).attr('data-page'), 10);
                            var url = paginationYearlySection($(this).attr('data-api'), owl, page);
                            owlPagination(url, e);
                            $(this).attr('data-page', ++page);
                        }
                    }
                })
                initCarousel($spmvmovie);
                $('.slideContainer').css({ opacity: 0, visibility: "visible" }).animate({ opacity: 1.0 }, 500);
            }
        });
    }
}

//---------------------------End Yearly Carousel---------------------------------------
//---------------------------Start Popular carousel ---------------------------------
function initPopular(owlObject, owl) {
    $("#" + owlObject).html("");
    if ($("#" + owlObject + "").html().replace(/\s+/g, '') == "") {
        var timeType = owl.attr('data-Time');
        var videosType = owl.attr('data-videoType');
        var baseUrl = "/api/android/" + videosType + "/kind/" + timeType + "";

        $("#" + owlObject + "").append('<img id="owloadgif" style="width:3%" alt="blankimage" src="/loadingImages/loader.gif">')
        $.ajax({
            url: baseUrl, success: function (result) {
                $("#owloadgif").remove()

                var itemBuild = buildCarouselCards(owlObject, result);
                $("#" + owlObject + "").append(itemBuild);
                //Featured Movie
                var $spmvmovie = $("." + owlObject + "");
                initCarousel($spmvmovie);
                $('.slideContainer').css({ opacity: 0, visibility: "visible" }).animate({ opacity: 1.0 }, 500);
            }
        });
    }
}
//---------------------------End Popular carousel ---------------------------------
function initCarousel($spmvmovie) {

    $spmvmovie.owlCarousel({
        loop: false,
        dots: false,
        nav: true,
        navClass: ['owl-prev', 'owl-next'],
        slideBy: 3,
        navText: ['<i class="themeum-moviewangle-left" aria-hidden="true"></i>', '<i class="themeum-moviewangle-right" aria-hidden="true"></i>'],
        rtl: false,
        autoplay: false,
        autoplayTimeout: 3000,
        autoplayHoverPause: true,
        mouseDrag: false,
        autoHeight: false,
        lazyLoad: true,
        responsive: {
            0: {
                items: 2
            },
            420: {
                items: 2
            },
            600: {
                items: 3
            },
            767: {
                items: 4
            },
            900: {
                items: 5
            },
            1050: {
                items: 6
            },
            1150: {
                items: 7
            },
            1250: {
                items: 8
            },
            1400: {
                items: 9
            },
            2000: {
                items: 10
            }

        }
    });
}
//------------------Start build items------------------------------
function buildCarouselCards(owlObject, result, sliderId) {

    var itemBuild = '<div class="movie-featured ' + owlObject + '" data-api="' + owlObject + '"data-id="' + sliderId + '" data-page="1">';
    var lang;
    var isRtl = location.pathname.search('ar');
    if (isRtl === -1) {
        lang = "en";
    } else {
        lang = "ar";
    }
    for (var i in result) {

        var videoTitle = "";
        if (result[i].kind == 2) {
            if (lang.value == "ar") {
                videoTitle = result[i].er_title;
            }
            else {
                videoTitle = result[i].en_title;
            }
        }
        else {
            if (lang.value == "ar") {
                videoTitle = result[i].ar_title
            }
            else {
                videoTitle = result[i].en_title
            }
        }
        if ($.cookie("token")) {
            var notifyClass = "notify";
        } else {
            notifyClass = "";
        }
        itemBuild += '<div class="item analytics ' + notifyClass + '"  data-analytics="|eventID=6|eventInt=1|videoID=' + result[i].nb + '|">'
        itemBuild += '<div class="featuredContent">'
        itemBuild += '<a id="' + result[i].nb + '" class="cast watchLaterBtnV2  analytics" data-analytics="|eventID=6|eventInt=2|videoID=' + result[i].nb + '|">'
        itemBuild += '<i class="themeum-moviewclock "></i>'
        itemBuild += '</a></div>'
        itemBuild += '<div class="movie-poster"><a><img style="" class="item-responsive" src="' + result[i].imgMediumThumbObjUrl + '" alt=""></a></div>'
        itemBuild += '<a href="/page/movie/watch/' + lang + '/' + result[i].nb + '" class="play-icon" onclick="event.stopPropagation();" window.event.cancelBubble = "true">'
        itemBuild += '<i class="themeum-moviewplay" style="font-size: 40px;">'
        itemBuild += '</i>'
        itemBuild += '</a>'
        itemBuild += '<div class="content-wrap">'
        itemBuild += '<div class="video-container">'
        itemBuild += '</div>'
        itemBuild += '</div>'
        itemBuild += '<div class="movie-details" style="text-align:left;">'
        if (result[i].parent_skipping == '1') {
            itemBuild += '<div class="moview-rating-wrapper">'
            itemBuild += '<span class="moview-rating-summary">'
            itemBuild += '<span class="parentalResponsive" ></span>'
            itemBuild += '</span>'
            itemBuild += '</div>'
        }
        itemBuild += '<div class="movie-name">'
        itemBuild += '<h2 class="movie-title" style="font-size: 18px"><a>' + videoTitle + '</a></h2>'
        itemBuild += '</div>'
        itemBuild += '<div class="moview-rating-wrapper">'
        itemBuild += ' <div class="moview-rating">'
        itemBuild += '<span class="star active" style="padding:0px"></span>'
        itemBuild += '</div>'
        itemBuild += '<span class="moview-rating-summary" style="padding:0px">'
        itemBuild += '<span>' + result[i].stars + '</span>'
        itemBuild += '/10</span>'
        itemBuild += '</div>'
        itemBuild += '</div>'
        itemBuild += '</div>'
    }
    itemBuild += '</div>'

    return itemBuild;
}

//------------------End build items------------------------------
// ----------------- Start Build Dynamic Sliders -----------------

// ----------------- End Build Dynamic Sliders -----------------
