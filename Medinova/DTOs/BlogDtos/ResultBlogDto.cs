using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.BlogDtos
{
    public class ResultBlogDto
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogSubtitle { get; set; }
        public string BlogContent { get; set; }
        public string BlogWriter { get; set; }
        public string WriterProfile { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
    }
}