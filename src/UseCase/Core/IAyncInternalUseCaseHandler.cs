namespace UseCase.Core;

internal interface IAsyncInternalUseCaseHandler<in TInputData, TOutputData>
    where TInputData : class, IInputData where TOutputData : class, IOutputData
{
    ValueTask<TOutputData> HandleAsync(TInputData inputData);
}