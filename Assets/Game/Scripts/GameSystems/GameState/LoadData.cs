using System;
using Cysharp.Threading.Tasks;

public class LoadData
{
    public Func<UniTask> LoadedAction { get; private set; }
    public bool IsNetLoading { get; private set; }
    public string SceneName { get; private set; }

    public LoadData(Func<UniTask> loadedAction, bool isNetLoading, string sceneName)
    {
        LoadedAction = loadedAction;
        IsNetLoading = isNetLoading;
        SceneName = sceneName;
    }
}