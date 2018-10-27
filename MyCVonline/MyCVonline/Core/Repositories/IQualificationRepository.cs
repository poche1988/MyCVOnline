using MyCVonline.Core.Models;
using System.Collections.Generic;

namespace MyCVonline.Core.Repositories
{
    public interface IQualificationRepository
    {
        IEnumerable<Qualification> GetQualifications();

        IEnumerable<QualificationLevel> GetQualificationLevels();

        Qualification GetQualification(int id);

        void Add(Qualification qual);
    }
}
