using UnityEngine;

public class SceneContext : MonoBehaviour
{
    [Header("Сюда вставлять то, что с сцены резолвится")] [SerializeField]
    private Installer[] installers;


    private void Awake()
    {
        Resolve();
    }

    public void Resolve()
    {
        foreach (var inst in installers)
        {
            inst.Install(AllServices.Container);
        }
    }
}