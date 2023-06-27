using AutoMapper;
using ColleagueInt.RTW.Core.ServiceNow.Contracts;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.Repositories.Contracts;
using ColleagueInt.RTW.Services.Contracts;
using ColleagueInt.RTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Services
{
	public class IncidentService : IIncidentService
	{
		private readonly IIncidentRepository _incidentRepository;
		private readonly ISnowIncidentService _snowIncidentService;

		private readonly IMapper _mapper;
		private static readonly object _lock = new object();

		public IncidentService(IIncidentRepository incidentRepository, ISnowIncidentService snowIncidentService, IMapper mapper)
		{
			_incidentRepository = incidentRepository;
			_snowIncidentService = snowIncidentService;
			_mapper = mapper;
		}

		//TODO
		public async Task<IncidentViewModel> GetIncidentDetailsAsync(IncidentErrorDescription errorDescription)
		{
			IncidentViewModel incidentViewModel = new IncidentViewModel();
			var errorCode = IncidentErrorCodes.Instance.GetIncidentErrorCode(errorDescription);
			if (errorCode != null)
			{
				var requiredIncidentDetail = await _incidentRepository.GetIncidentDetailsAsync(errorCode);
				var mappedData = _mapper.Map<IncidentViewModel>(requiredIncidentDetail);
				if (mappedData != null)
				{
					mappedData.ErrorDescription = errorDescription;
					return mappedData;
				}
			}

			return incidentViewModel;
		}

		public async Task AddIncidentAsync(IncidentErrorDescription errorDescription, string errorMessage, string errorType)
		{
			var errorCode = IncidentErrorCodes.Instance.GetIncidentErrorCode(errorDescription);
			if (errorCode == string.Empty)
			{
				return;
			}

			var requiredIncidentDetail = await _incidentRepository.GetIncidentDetailsAsync(errorCode);

			if (requiredIncidentDetail == null)
				throw new Exception($"ErrorCode {errorCode} not found in incident details table for {errorDescription} in database.");

			var incidentDetailViewModel = _mapper.Map<IncidentViewModel>(requiredIncidentDetail);
			incidentDetailViewModel.ServiceNowDescription = errorMessage;
			incidentDetailViewModel.Status = IncidentStatus.Assigned;

			if (errorType == "Application")
			{
				lock (_lock)
				{
					var incidentExist = _incidentRepository.FirstOrDefaultAsync(x => x.IncidentDetailId == incidentDetailViewModel.Id
						&& x.Status == IncidentStatus.Assigned).GetAwaiter().GetResult();

					if (incidentExist == null)
					{
						string incidentNumber = CreateServiceNowIncident(incidentDetailViewModel);
						AddIncident(incidentNumber, errorMessage, requiredIncidentDetail.Id);
					}
				}
			}
			else if (errorType == "Data")
			{
				string incidentNumber = CreateServiceNowIncident(incidentDetailViewModel);
				AddIncident(incidentNumber, errorMessage, requiredIncidentDetail.Id);
			}
		}

		private void AddIncident(string incidentNumber, string description, int incidentDetailId)
		{
			if (incidentNumber == string.Empty)
			{
				throw new Exception($"Incident could not be created because the number is empty, Please check service is up and running.");
			}

			//TODO
			var incident = new Incident
			{
				Number = incidentNumber,
				Status = IncidentStatus.Assigned,
				IncidentDetailId = incidentDetailId,
				CreationTime = DateTime.Now.ToUniversalTime(),
				ServiceNowDescription = description,
			};

			_incidentRepository.AddAsync(incident).GetAwaiter().GetResult();
		}

		private string CreateServiceNowIncident(IncidentViewModel incidentDetailViewModel)
		{
			//TODO
			return _snowIncidentService.CreateIncidentAsync(incidentDetailViewModel).GetAwaiter().GetResult();
		}

		public async Task<bool> IncidentExistsAsync(IncidentErrorDescription errorDescription)
		{
			var errorCode = IncidentErrorCodes.Instance.GetIncidentErrorCode(errorDescription);
			var incidentDetail = await _incidentRepository.GetIncidentDetailsAsync(errorCode);

			var incident = await _incidentRepository.FirstOrDefaultAsync(x => x.IncidentDetailId == incidentDetail.Id
							&& x.Status == IncidentStatus.Assigned);

			return incident != null;
		}

		public async Task RemoveIncidentsAsync(DateTime tillDateTime, IncidentStatus status)
		{
			var incidentsToDelete = await _incidentRepository.GetWhereAsync(x => x.CreationTime < tillDateTime && x.Status == status);
			await _incidentRepository.RemoveRangeAsync(incidentsToDelete);
		}

		public async Task<IEnumerable<IncidentViewModel>> GetAllActiveIncidentsAsync()
		{
			var incidents = await _incidentRepository.GetWhereAsync(x => x.Status != IncidentStatus.Resolved);
			var incidentViewModel = _mapper.Map<List<IncidentViewModel>>(incidents);
			return incidentViewModel;
		}

		public async Task UpdateIncidentAsync(int id, IncidentStatus status)
		{
			var incident = await _incidentRepository.GetByIdAsync(id);
			if (incident != null)
			{
				incident.Status = status;
				await _incidentRepository.UpdateAsync(incident, id);
			}
		}
	}
}
