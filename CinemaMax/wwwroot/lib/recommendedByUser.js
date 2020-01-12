// $( window ).load(function() {    
//     $.ajax({
//         type: "GET",
//         url: "/page/movie/UBIMSRecommendedItemsByUser/1",
//         success: function(data){
            
//             if (data) {
//                 itemBuild = buildRecommendedSlider("recommendedVideo",data);
//                 $("section").eq(1).after(itemBuild);
//                 var $spmvmovie = $(".recommendedVideo");
//                 initCarousel($spmvmovie);
//                 $('.slideContainer').css({opacity: 0, visibility: "visible"}).animate({opacity: 1.0}, 500);        
//             }
            
//         },
//         failure: function(errMsg) {
            
//         }
//     });
// });
    

