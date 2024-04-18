using BlogApp.Data;
using BlogApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace BlogApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly DataContext _dataContext;
        public BlogController(DataContext datacontext)
        {
            this._dataContext = datacontext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var blog = _dataContext.BlogDetails.FirstOrDefault(b => b.Id == id);
                if (blog == null)
                {
                    return NotFound();
                }
                return View(blog);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogDetailsView detailsView)
        {
            try
            {
                var blogDetails = new BlogDetails
                {
                    Title = detailsView.Title,
                    Authorname = detailsView.Authorname,
                    Content = detailsView.Content, 
                    PublicationDate = detailsView.PublicationDate
                };

                await _dataContext.BlogDetails.AddAsync(blogDetails);
                await _dataContext.SaveChangesAsync();

                return Ok(new { redirectToUrl = Url.Action("Index", "Home") });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }


        public IActionResult ViewDetails(int id)
        {
            try
            {
                var blog = _dataContext.BlogDetails.FirstOrDefault(b => b.Id == id);
                if (blog == null)
                {
                    return NotFound();
                }
                return View(blog);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(BlogDetails detailsView)
        {
            try
            {
                var blog = await _dataContext.BlogDetails.FindAsync(detailsView.Id);

                if (blog == null)
                {
                    return NotFound();
                }

                blog.Title = detailsView.Title;
                blog.Authorname = detailsView.Authorname;
                blog.Content = detailsView.Content;
                blog.PublicationDate = detailsView.PublicationDate;

                _dataContext.BlogDetails.Update(blog);
                await _dataContext.SaveChangesAsync();

                return Ok(new { redirectToUrl = Url.Action("Index", "Home") });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }


        public async Task<IActionResult> Delete(UpdateBlogDetails detailsView)
        {
            try
            {
                var blog = await _dataContext.BlogDetails.FindAsync(detailsView.Id);

                if (blog != null)
                {
                    _dataContext.BlogDetails.Remove(blog);
                    await _dataContext.SaveChangesAsync();

                    return Ok(new { redirectToUrl = Url.Action("Index", "Home") });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500); // Internal server error response
            }
        }




    }
}
