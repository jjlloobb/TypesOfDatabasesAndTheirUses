using CBTW.Microservices.UI.Domain.Models;

namespace CBTW.Microservices.UI.Application.Providers;

public interface IDocumentaryDbProvider
{
	public Task<List<CallCenterLog>> GetCallCenterLogAsync();

	public Task<CallCenterLog> GetCallCenterLogAsync(string id);

	public Task CreateCallCenterLogAsync(CallCenterLog newCallCenterLog);

	public Task UpdateCallCenterLogAsync(string id, CallCenterLog updatedCallCenterLog);

	public Task RemoveCallCenterLogAsync(string id);
}