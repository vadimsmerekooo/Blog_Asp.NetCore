using System;
using System.Collections.Generic;

namespace Blog_Version_2.ModelsDAL
{
    public partial class Comments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Themes { get; set; }
        public int Post { get; set; }
        public string Text { get; set; }
        public string Email { get; set; }
    }
}
