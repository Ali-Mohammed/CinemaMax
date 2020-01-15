using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaMaxFeeder.Database.Model
{
    public class HomePageSlider
    {
        public long Id { get; set; }
        public long Order { get; set; }
        public long Nb { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Options { get; set; }
        public string Comments { get; set; }

        public List<HomePageSliderMovie> HomePageSliderMovies { get; set; }
    }
}
