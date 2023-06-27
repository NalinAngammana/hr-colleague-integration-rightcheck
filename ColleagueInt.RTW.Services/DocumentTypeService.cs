using AutoMapper;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Repositories.Contracts;
using ColleagueInt.RTW.Services.Contracts;
using ColleagueInt.RTW.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Services
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IMapper _mapper;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        
        public DocumentTypeService(IMapper mapper, IDocumentTypeRepository documentTypeRepository)
        {
            _mapper = mapper;
            _documentTypeRepository = documentTypeRepository;
        }

        public async Task<IEnumerable<DocumentTypeViewModel>> GetAllDocumentTypesAsync()
        {
            var allDocumentType = await _documentTypeRepository.GetAllDocumentTypesAsync();
            return _mapper.Map<List<DocumentTypeViewModel>>(allDocumentType);
        }

        public async Task<bool> UpdateDocumentTypeIdAsync(int id, long docTypeId)
        {
            var result = await _documentTypeRepository.GetByIdAsync(id);
            if (result == null)
                return false;
            result.HCMDocumentTypeId = docTypeId;
            await _documentTypeRepository.UpdateAsync(result);
            return true;
        }
    }
}
