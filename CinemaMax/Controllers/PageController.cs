using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using CinemaMaxFeeder.ModelJson;
using CinemaMaxFeeder;
using Microsoft.EntityFrameworkCore;
using CinemaMaxFeeder.ModelJson.HomePageSlider;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace CinemaMax.Controllers
{
    public class PageController : Controller
    {
        private readonly MovieContext _context;

        public PageController(MovieContext context)
        {
            _context = context;
        }

        
        [HttpPost("api/info/ShabakatyInfo")]
        public IActionResult GetInfo()
        {
            return Ok(new {
                Status = false,
                error = "Unknown method."
            });
        }
        [HttpGet("page/home/Sliders/en")]
        public async Task<IActionResult> IndexAsync()
        {

            var slideHomePageJson = new HomePageSliderJson();

            slideHomePageJson.IsLogin = false;
            slideHomePageJson.Notify = "";
            slideHomePageJson.IsSessionExists = false;
            slideHomePageJson.MitgliedName = "";
            slideHomePageJson.MitgliedLighttigerImg = "";

    

            var slideModel = await _context.HomePageSliders.Where(Q => Q.Type == "1").ToListAsync();


            Dictionary<string, HomePageSliderGroupJson> grupContent = new Dictionary<string, HomePageSliderGroupJson>();

            foreach (var item in slideModel)
            {
                var groupJson = new HomePageSliderGroupJson();
                groupJson.Name = item.Name;
                groupJson.Type = "custom";

                var contentsJson = new List<HomePageSliderContentJson>();

                var slideModel22 = await _context.HomePageSliderMovie.Where(Q => Q.HomePageSlider == item).Include(In => In.Movie).ToListAsync();

                var number = 1;
                foreach (var movieBasic in slideModel22)
                {

                    if (movieBasic.Movie == null)
                    {
                        continue;
                    }

                    HomePageSliderContentJson contentJson = new HomePageSliderContentJson();


                    var movie = movieBasic.Movie;


                    contentJson.EnTitle = movie.EnTitle;
                    contentJson.ArTitle = movie.ArTitle;


                    contentJson.CustomArTitle = movie.ArTitle;
                    contentJson.CustomEnTitle = movie.EnTitle;

                    contentJson.OtherTitle = movie.OtherTitle;

                    contentJson.MDate = movie.MDate;

                    if (movie.Trailer != null)
                    {
                        contentJson.Trailer = movie.Trailer.ToString();
                    }

                    contentJson.ImgObjUrl = new Uri(movie.ImgObjUrlFull);
                    contentJson.ImgThumbObjUrl = new Uri(movie.ImgThumbObjUrlFull);
                    contentJson.ImgMediumThumbObjUrl = new Uri(movie.ImgMediumThumbObjUrlFull);
                    contentJson.Stars = movie.Stars.ToString();
                    contentJson.ItemOrderList = number;
                    contentJson.Year = movie.Year;

                    contentJson.ListId = item.Id;



                    number++;
                    contentsJson.Add(contentJson);
                }
                groupJson.Content = contentsJson;
                grupContent.Add(item.Name, groupJson);
            }



            slideHomePageJson.Group = grupContent;



            return Ok(slideHomePageJson);
        }
    }
}