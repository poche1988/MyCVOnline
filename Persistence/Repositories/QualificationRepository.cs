using Microsoft.AspNet.Identity;
using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;

namespace MyCVonline.Repositories
{
    public class QualificationRepository :IQualificationRepository
    {
        private readonly IApplicationDbContext _context;
        public QualificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Qualification> GetQualifications() {
            var id = ClaimsPrincipal.Current.Identity.GetUserId();

             return _context.Qualifications.Include(e => e.level)
                 .Where(q => q.UserId == id && q.Active == true)
                 .OrderBy(q => q.level.QualificationLevelId)
                 .ThenByDescending(Q => Q.To)
                 .ToList();
          
        }

        public IEnumerable<QualificationLevel> GetQualificationLevels() {
            return _context.Qualificationlevels.ToList();
        }

        public Qualification GetQualification(int id) {
            var currentUserId = ClaimsPrincipal.Current.Identity.GetUserId();

            return _context
                .Qualifications
                .Where(q => q.QualificationId == id && q.Active == true && q.UserId == currentUserId).FirstOrDefault();
        }

        public void Add(Qualification qual)
        {
            _context.Qualifications.Add(qual);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}