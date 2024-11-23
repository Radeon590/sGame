
using UnityEngine.Events;

public interface IInitializable
{
    public bool IsInitializationOnStartRequired { get; }

    public UnityEvent OnInitialized { get; }

    public void Initialize();
}
