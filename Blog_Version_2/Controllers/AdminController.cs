using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blog_Version_2.Areas.Identity.Data;
using Blog_Version_2.Data;
using Blog_Version_2.ModelsDAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;

namespace Blog_Version_2.Controllers
{
    public class AdminController : Controller
    {
        private Blog_ContextDAL _context = new Blog_ContextDAL();
        static List<Tags> tagsContext = new List<Tags>();
        static List<Categorys> catContext = new List<Categorys>();

        IWebHostEnvironment _appEnvironment;

        public AdminController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }
        public ActionResult Panel()
        {
            tagsContext = _context.Tags.ToList();
            catContext = _context.Categorys.ToList();
            return View();
        }
        public ActionResult PanelMenu(string name)
        {
            return PartialView($"/Views/Admin/{name}AdminPartial.cshtml");
        }
        [HttpPost]

        public async Task<RedirectToActionResult> Create(Posts post, string editor, IFormFile inputImage)
        {
            Random rand = new Random();
            if (_context.Posts.Where(postDb => postDb.Url == post.Url).ToList().Count != 0)
                post.Url = post.Url + rand.Next(9999);
            string path = "/images/posts-images/" + post.Url + inputImage.FileName;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await inputImage.CopyToAsync(fileStream);
            }
            post.PreviewPhoto = path;
            post.Blocked = 0;
            post.Date = DateTime.Now;
            post.Description = editor;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(Categorys categorys)
        {
            if (categorys != null
                && categorys.Name != null
                && categorys.Url != null
                && (_context.Categorys.Where(cat => cat.Name == categorys.Name).ToList()).Count == 0
                && (_context.Categorys.Where(cat => cat.Url == categorys.Url).ToList()).Count == 0)
            {
                categorys.Url = categorys.Url.ToLower();
                _context.Categorys.Add(categorys);
                catContext.Add(categorys);
                await _context.SaveChangesAsync();
                return View(Microsoft.AspNetCore.Http.HttpResponseWritingExtensions.WriteAsync(this.Response, "<script>self.close();</script>"));
            }
            else
            {
                return View();
            }
        }
        public ActionResult CreateTags()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTags(Tags tagView)
        {
            if (tagView != null
                && tagView.Tag != null
                && tagView.Url != null
                && (_context.Tags.Where(tag => tag.Tag == tagView.Tag).ToList()).Count == 0
                && (_context.Tags.Where(tag => tag.Url == tagView.Url).ToList()).Count == 0)
            {
                tagView.Url = tagView.Url.ToLower();
                _context.Tags.Add(tagView);
                tagsContext.Add(tagView);
                await _context.SaveChangesAsync();
                return View(Microsoft.AspNetCore.Http.HttpResponseWritingExtensions.WriteAsync(this.Response, "<script>self.close();</script>"));
            }
            else
            {
                return View();
            }
        }
        public ActionResult UpdateTags()
        {
            string resultHtmlList = string.Empty;
            foreach (var tag in tagsContext)
            {
                resultHtmlList += @$"<option>{tag.Tag}</option>
";
            }
            return Content(resultHtmlList);
        }
        public ActionResult UpdateCategory()
        {
            string resultHtmlList = string.Empty;
            foreach (var cat in catContext)
            {
                resultHtmlList += @$"<option>{cat.Name}</option>
";
            }
            return Content(resultHtmlList);
        }
    }
}
