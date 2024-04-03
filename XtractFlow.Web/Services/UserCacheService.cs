using XtractFlow.Web.Data;
using XTractFlow.API.Result;

namespace XtractFlow.Web.Services;

public class UserCacheService
{
    public Guid CircuitId { get; set; } = Guid.NewGuid();

    public Dictionary<UserDocument, ProcessorResult> ClassificationResults { get; } = new();

    public void AddClassification(UserDocument document, ProcessorResult result)
    {
        ClassificationResults.Add(document, result);
        Notify();
    }
    
    private List<Action> OnChangeEventHandler = new();
    public void Subscribe(Action action)
    {
        OnChangeEventHandler.Add(action);
    }

    public void Unsubscribe(Action action)
    {
        OnChangeEventHandler.Remove(action);
    }

    private void Notify()
    {
        foreach (var handler in OnChangeEventHandler)
        {
            handler?.Invoke();
        }
    }
}