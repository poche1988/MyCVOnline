using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Core.Repositories;
using MyCVonline.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MyCVonline.Persistence.Repositories
{
    public class NewsRepository : INewsRepository
    {   private readonly IApplicationDbContext _context;
        public NewsRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<News> GetAllNews()
        {
            return _context.News.Where(n => n.IsCanceled == false && n.Content != "Help").OrderBy(n => n.Date);
        }

        public News GetNews(int id) {
           return _context.News.Single(q => q.NewsId == id);
        }

        public void AddNews(News n)
        {
            _context.News.Add(n);
        }

        public NewsFormViewModel getNewsWithNotificationMessage(int id)
        {
            var NewsToUpdate = _context.Notifications.Where(n => n.News.NewsId == id)
                .Select(n => new { n.Message, n.News }).FirstOrDefault();

            var viewmodel = new NewsFormViewModel
            {
                NotificationMessage = NewsToUpdate.Message,
                ID = NewsToUpdate.News.NewsId,
                Title = NewsToUpdate.News.Title,
                Content = NewsToUpdate.News.Content,
                Date = NewsToUpdate.News.Date.ToString("dd-MM-yyyy"),
                Photo = NewsToUpdate.News.Photo
            };

            return viewmodel;
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}