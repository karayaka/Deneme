using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Deneme.DAL.Class.BlogClasses;
using Deneme.DAL.Class.Definations;
using Deneme.DAL.Class.UserClasses;
using Deneme.DAL.DataContexts;
using Deneme.DAL.Enums;
using Deneme.Entity.Bussenes;
using Deneme.Entity.Interfaces;
using Deneme.WepApi.Models.BlogModels;
using Deneme.WepApi.Models.CategoriModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;


namespace Deneme.WepApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
       
       
        public readonly IAppBaseRepository AppRepo;
        public readonly IBlogRepository BlogRepo;
        public readonly IUnitOfWork unitOfWork;
        private  string UserID;
        public BlogsController(IAppBaseRepository _AppRepo, IBlogRepository _BlogRepo, IUnitOfWork _unitOfWork)
        {
            AppRepo = _AppRepo;
            BlogRepo = _BlogRepo;
            unitOfWork = _unitOfWork;
          
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]//bu bölüm enumtype olarak kullanılabilir rol bazlı autantication
        public IActionResult Get()
        {
            var a = UserID= User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //Include Yapının çalışması için Microsoft.AspNetCore.Mvc.NewtonsoftJson Pakedi Yüklenmli ve starup 60 . satırdaki kod eklenmöeli
            var blogs = AppRepo.GetNonDeleted<BlogClass>(t=>t.ObjectStatus==ObjectStatus.NonDeleted);
            var b= blogs.Include(t=>t.Categori);
            List<BlogListModel> models = new List<BlogListModel>();
            foreach (var item in b)
            {
                BlogListModel model = new BlogListModel()
                {
                    ID = item.ID,
                    Status = item.Status,
                    BlogCategori = item.Categori.CategoriName,
                    BlogContext = item.BlogContext,
                    BlogName = item.BlogTitle,
                };
                models.Add(model);
            }           
            //user iject etme işlemine bakılacak!!
            return Ok(models);
        }
        [HttpPost]
        public IActionResult SaveBlog(BlogModel blog)
        {
            var model = new BlogClass()
            {
                BlogContext = blog.BlogContext,
                BlogTitle = blog.BlogTitle,
                CategoriID = blog.CategoriID,
                CreatedBy = 1,          
                LastUpdateBy = 1,
                LastUpdateDate = DateTime.Now,         
                PublishDate = DateTime.Now,        
            };
            AppRepo.Add(model);
            return Ok(blog);
        }
        [HttpPost]
        public IActionResult SaveCategori(CategoriCreateModel categori)
        {

            
            var model = new BlogCategoriDefination()
            {
                CategoriDesc= categori.CategoriDesc,
                CategoriName=categori.CategoriName,
                CategoriType=1,
                CreatedBy = 1,                
                LastUpdateBy = 1,
            };
            AppRepo.Add(model);
            BlogRepo.GetFiveBlog();
            return Ok(model);
        }

        public IActionResult CreateWor()
        {
            //Bu Yapı İle Bütün Repositoriler Tek bir Contex Kullanması Sağlandı Ve Bu Sayede Contex NEsnesi VCerimli Ve Hizli Çalışır Hale Geldi!!
            var blog = new BlogClass() 
            {
                BlogContext="",
                BlogTitle="",
                CategoriID=1,
                CreatedBy=1,
                LastUpdateBy=1,
                PublishDate=DateTime.UtcNow,
                
            };
            unitOfWork.BlogRepository.Add<BlogClass>(blog);
            unitOfWork.SaveChange();
            unitOfWork.BlogRepository.GetFiveBlog();
            return Ok();
        }

    }
}