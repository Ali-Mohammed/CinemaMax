using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaMaxFeeder.Database.Model
{
    public class HomePageSliderMovie
    {
        [Key]
        public long Id { get; set; }

        public Movie Movie { get; set; }
        public long MovieId { get; set; }

        public long HomePageSliderId { get; set; }
        public HomePageSlider HomePageSlider { get; set; }

    }
}
