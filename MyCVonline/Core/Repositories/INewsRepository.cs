using MyCVonline.Core.Models;
using MyCVonline.Core.ViewModels;
using System.Collections.Generic;

namespace MyCVonline.Core.Repositories
{
    public interface INewsRepository
    {
        IEnumerable<News> GetAllNews();
        News GetNews(int id);
        
        void AddNews(News n);

        NewsFormViewModel getNewsWithNotificationMessage(int id);

    }
}
