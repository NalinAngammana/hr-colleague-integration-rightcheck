using AutoMapper;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.Repositories.Contracts;
using ColleagueInt.RTW.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Services
{
    public class ErrorService : IErrorService
    {
        private readonly IErrorRepository _errorRepository;
        private int errorCountIndex = 0;

        public ErrorService(IErrorRepository errorRepository, IMapper mapper)
           : base()
        {
            _errorRepository = errorRepository;
        }

        public async Task AddErrorAsync(Exception ex)
        {
            try
            {
                errorCountIndex++;
                var baseException = ex.GetBaseException();

                var machineName = $"[{System.Environment.MachineName.ToUpper()}]";
                var baseMessage = !string.IsNullOrWhiteSpace(baseException.Message)
                    ? baseException.Message
                    : "No Errors Message";

                var message = machineName + "--" + baseMessage;

                // ToDO
                Error error = new Error
                {
                    Message = message,
                    Type = baseException.GetType().AssemblyQualifiedName,
                    Logged = DateTime.UtcNow,
                    StackTrace = GetStackTrace(ex)
                };

                await _errorRepository.AddAsync(error);
            }
            catch (Exception exception)
            {
                if (errorCountIndex != 3)
                {
                    // Stop from going in loop more than 3 times.
                    _ = AddErrorAsync(exception);
                }
            }
            finally
            {
                errorCountIndex = 0;
            }
        }

        public async Task RemoveErrorsAsync(DateTime tillDateTime)
        {
            var errorsToRemove = await _errorRepository.GetWhereAsync(x => x.Logged < tillDateTime);
            await _errorRepository.RemoveRangeAsync(errorsToRemove);
        }

        private string GetStackTrace(Exception ex)
        {
            var stackTrace = new StringBuilder();
            var stack = new Stack<Exception>();

            while (ex != null)
            {
                stack.Push(ex);
                ex = ex.InnerException;
            }

            foreach (var stackEx in stack)
            {
                stackTrace.AppendFormat("[{0}] {1}", stackEx.GetType().FullName, stackEx.Message);
                stackTrace.AppendLine();
                stackTrace.Append(stackEx.StackTrace);
                stackTrace.AppendLine();
                stackTrace.AppendLine();
            }

            return stackTrace.ToString();
        }
    }
}
