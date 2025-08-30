using PMApp.Data.Entities;
using PMApp.Models.DTO.Requests;
using PMApp.Models.DTO.Responses;

namespace PMApp.WEB.Mappers
{
    public static class WorkLogMapper
    {
        public static WorkLogResponse MapLog(WorkLog workLog)
        {
            var workLogResponse = new WorkLogResponse()
            {
                Id = workLog.Id,
                WorkingPeriod = workLog.WorkingPeriod,
                WorkingHours = workLog.WorkingHours,
            };
            return workLogResponse;
        }

        public static WorkLog MapWorkLogRequest(WorkLogRequest logRequest)
        {
            var workLog = new WorkLog()
            {
                WorkingPeriod = logRequest.WorkingPeriod,
                WorkingHours = logRequest.WorkingHours,
            };
            return workLog;
        }
    }
}
