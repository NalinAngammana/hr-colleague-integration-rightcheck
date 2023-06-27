using AutoMapper;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.Repositories.Contracts;
using ColleagueInt.RTW.Services.Contracts;
using ColleagueInt.RTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Services
{
    public class InboundErrorLogService : IInboundErrorLogService
    {
        private readonly IMapper _mapper;
        private readonly IInboundErrorLogRepository _inboundErrorLogRepository;
        private readonly IIncidentRepository _incidentRepository;

        public InboundErrorLogService(IMapper mapper, IInboundErrorLogRepository inboundErrorLogRepository, IIncidentRepository incidentRepository)
        {
            _mapper = mapper;
            _inboundErrorLogRepository = inboundErrorLogRepository;
            _incidentRepository = incidentRepository;
        }

        public async Task<InboundErrorLog> AddErrorLogAsync(InboundErrorLogViewModel inboundErrorLogViewModel)
        {
            var inboundErrorLog = _mapper.Map<InboundErrorLog>(inboundErrorLogViewModel);
            await _inboundErrorLogRepository.AddAsync(inboundErrorLog);
            return inboundErrorLog;
        }

        public async Task<IEnumerable<InboundErrorLog>> GetLatestErrorLogByErrorTypeAsync(IncidentErrorDescription  incidentErrorDescription)
        {
            var errorCode = IncidentErrorCodes.Instance.GetIncidentErrorCode(incidentErrorDescription);
            var lastIncident = await _incidentRepository.GetLastRecordedIncidentByErrorCodeAsync(errorCode);

            var inboundErrorlogs = await _inboundErrorLogRepository.GetLatestErrorLogByErrorTypeAsync(incidentErrorDescription, lastIncident == null ? DateTime.MinValue : lastIncident.CreationTime);
            
            return inboundErrorlogs;
        }

        public async Task RemoveInboundErrorLogsAsync(DateTime tillDateTime)
        {
            var errorLogsToRemove = await _inboundErrorLogRepository.GetWhereAsync(x => x.Logged < tillDateTime);
            await _inboundErrorLogRepository.RemoveRangeAsync(errorLogsToRemove);
        }

    }
}
