using AutoMapper;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.Repositories.Contracts;
using ColleagueInt.RTW.Services.Contracts;
using ColleagueInt.RTW.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Services
{
    public class ColleagueService : IColleagueService
    {
        private readonly IMapper _mapper;
        private readonly IColleagueRepository _colleagueRepository;
        private readonly IIncidentRepository _incidentRepository;

        public ColleagueService(IMapper mapper, IColleagueRepository colleagueRepository, IIncidentRepository incidentRepository)
        {
            _mapper = mapper;
            _colleagueRepository = colleagueRepository;
            _incidentRepository = incidentRepository;
        }

        public async Task<Colleague> AddColleagueAsync(ColleagueViewModel colleagueViewModel)
        {
            var colleague = _mapper.Map<Colleague>(colleagueViewModel);
            await _colleagueRepository.AddAsync(colleague);
            return colleague;
        }

        public async Task UpdateColleagueAsync(ColleagueViewModel colleagueViewModel)
        {
            var colleague = _mapper.Map<ColleagueViewModel, Colleague>(colleagueViewModel);
            await _colleagueRepository.UpdateAsync(colleague, colleague.Id);
        }

        public async Task<bool> AbleToCreateCheckRequestForColleagueAsync(string personNumber)
        {
            return  await _colleagueRepository.AbleToCreateCheckRequestForColleagueAsync(personNumber);
        }

        public async Task <string> GetTrackingReferenceAsync(string personNumber)
        {
            var splitcharacter = "_";

            var lastCollegueEntry = await _colleagueRepository.GetLastColleagueEntryAsync(personNumber);
            
            if (lastCollegueEntry == null)
                return ($"{personNumber}{splitcharacter}1");

            var lastTrackingReference = lastCollegueEntry.TrackingReference;
                        
            if (lastTrackingReference == "" || lastTrackingReference == null)
                return ($"{personNumber}{splitcharacter}1");
          
            var splittedStrings = lastTrackingReference.Split(splitcharacter);
            var nextTrackingNumber = lastTrackingReference;
               
            if (splittedStrings.Length == 2)
                nextTrackingNumber = (int.Parse(splittedStrings[1]) + 1).ToString() ;
          
            return ($"{personNumber}{splitcharacter}{nextTrackingNumber}");        
        }

        public async Task<IEnumerable<ColleagueViewModel>> GetInitialStageColleagueListAsync(int count)
        {
            var colleagueList =  await _colleagueRepository.GetInitialStageColleagueListAsync(count);
            var colleagueViewModel = _mapper.Map<IEnumerable<ColleagueViewModel>>(colleagueList);
            return colleagueViewModel;
        }

        public async Task<ColleagueViewModel> GetColleagueRecordByTrackingRefAsync(string trackingRef)
        {
            var result = await _colleagueRepository.GetWhereAsync(x => x.TrackingReference == trackingRef);
            return _mapper.Map<Colleague, ColleagueViewModel>(result.FirstOrDefault());
        }

        public async Task<IEnumerable<ColleagueViewModel>> GetLatestCheckRequestFailedListAsync()
        {
            var lastIncidentRecordedDate = await GetLastIncidentReportDateTimeAsync();

            var colleague = await _colleagueRepository.GetLatestCheckRequestFailedListAsync(lastIncidentRecordedDate);
            var colleagueViewModel = _mapper.Map<IEnumerable<ColleagueViewModel>>(colleague);
            return colleagueViewModel;
        }

        public async Task<DateTime> GetLastIncidentReportDateTimeAsync()
        {
            var errorCode = IncidentErrorCodes.Instance.GetIncidentErrorCode(IncidentErrorDescription.RTWAPIOutboundError);

            var lastIncident = await _incidentRepository.GetLastRecordedIncidentByErrorCodeAsync(errorCode);

            return lastIncident == null ? DateTime.MinValue : lastIncident.CreationTime;
        }
        public async Task<IEnumerable<ColleagueViewModel>> GetSuccessfullyCheckCompletedColleagueListAsync(int count)
        {
            var colleagueList = await _colleagueRepository.GetCheckSuccessfullyCompletedColleagueListAsync(count);
            var colleagueViewModel = _mapper.Map<IEnumerable<ColleagueViewModel>>(colleagueList);
            return colleagueViewModel;
        }

        public async Task<IEnumerable<ColleagueViewModel>> GetHCMUpdatedColleagueListAsync(int count, DateTime cutOffDate)
        {
            var colleagueList = await _colleagueRepository.GetHCMUpdatedColleagueListAsync(count, cutOffDate);
            var colleagueViewModel = _mapper.Map<IEnumerable<ColleagueViewModel>>(colleagueList);
            return colleagueViewModel;
        }

        public async Task RemoveColleagueDataAsync(DateTime tillDateTime)
        {
            var colleagueRecordsToDelete = await _colleagueRepository.GetWhereAsync(x => x.LastUpdateOn < tillDateTime);
            await _colleagueRepository.RemoveRangeAsync(colleagueRecordsToDelete);
        }

        public async Task<bool> AbleToCreateDocumentCheckRequestForColleagueAsync(string personNumber)
        {
            return await _colleagueRepository.AbleToCreateDocumentCheckRequestForColleagueAsync(personNumber);
        }
    }
}
