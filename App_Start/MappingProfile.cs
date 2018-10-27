using AutoMapper;
using MyCVonline.Core.DTOs;
using MyCVonline.Core.Models;

namespace MyCVonline.App_Start
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Notification,NotificationDTO>();
            CreateMap<News,NewsDTO>();
        }
    }
}