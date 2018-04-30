using MyCVonline.Core.Models;
using System.Collections.Generic;

namespace MyCVonline.Core.Repositories
{
    public interface ISectionRepository
    {
        IEnumerable<Section> GetSections();

        IEnumerable<TemplatePosition> GetPositions();

        Section GetSection(int id);
        TemplatePosition GetPosition(int id);

        void Add(Section sec);
        void Delete(Section sec);
    }
}
