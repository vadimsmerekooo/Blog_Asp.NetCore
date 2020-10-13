using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog_Version_2.ModelsDAL
{
    public partial class Posts
    {
        public int Id { get; set; }
        [Required]
        public string Header { get; set; }
        [Required]
        public string PreviewText { get; set; }
        [Required]
        public string PreviewPhoto { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Tags { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Author { get; set; }
        public int Blocked { get; set; }
        public int Watching { get; set; }
    }
}
