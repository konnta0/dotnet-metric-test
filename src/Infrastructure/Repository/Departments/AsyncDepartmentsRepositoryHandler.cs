using Infrastructure.Core.Instrumentation.Repository;
using Infrastructure.Repository.Departments.FindAll;
using MessagePipe;

namespace Infrastructure.Repository.Departments;

[AsyncRequestHandlerFilter(typeof(AsyncFindAllDepartmentsHandlerFilter))]
[AsyncRequestHandlerFilter(typeof(AsyncRepositoryInstrumentationHandlerFilter<IDepartmentsInputData, IDepartmentsOutputData>))]
// ReSharper disable once UnusedType.Global
public partial class AsyncDepartmentsRepositoryHandler
{
}