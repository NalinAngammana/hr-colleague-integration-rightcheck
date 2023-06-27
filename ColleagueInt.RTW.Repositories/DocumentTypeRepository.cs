using ColleagueInt.RTW.Database;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Repositories.Contracts;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ColleagueInt.RTW.Repositories
{
    public class DocumentTypeRepository : BaseRepository<DocumentType>, IDocumentTypeRepository
    {
        private new readonly RTWContext _context;

        public DocumentTypeRepository(RTWContext context) : base(context)
        {

            _context = context;
        }

        public async Task<IEnumerable<DocumentType>> GetAllDocumentTypesAsync()
        {
            return await _context.DocumentType.ToListAsync();
        }

    }
}
