using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamathonDemo2.Data.Models;

namespace XamathonDemo2.Data.Models
{
    public class MovieRating
    {
        public Movie Movie { get; set; }

        public Rating Rating { get; set; }
    }
}
