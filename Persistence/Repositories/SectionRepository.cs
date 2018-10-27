using Microsoft.AspNet.Identity;
using MyCVonline.Core.Models;
using MyCVonline.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;

namespace MyCVonline.Persistence.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly ApplicationDbContext _context;
        private string _CurrentUserId;
        public SectionRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _CurrentUserId = ClaimsPrincipal.Current.Identity.GetUserId();
        }

        public IEnumerable<Section> GetSections()
        {
            return _context.Sections
                .Include(p => p.Position)
                .Where(s => s.UserId == _CurrentUserId)
                .ToList();
            
        }

        public IEnumerable<TemplatePosition> GetPositions()
        {
            return _context.Positions.ToList();
        }

        public Section GetSection(int id)
        {
            return _context.Sections
                .Include(s=>s.Position)
                .Where(s => s.UserId == _CurrentUserId && s.SectionId == id).FirstOrDefault();
        }

        public TemplatePosition GetPosition(int id)
        {
            return _context.Positions.Where(p => p.ID == id).FirstOrDefault();
        }

        public void Add(Section sec)
        {
            _context.Sections.Add(sec);
        }

        public void Delete(Section sec)
        {
            _context.Sections.Remove(sec);
        }
    }
}