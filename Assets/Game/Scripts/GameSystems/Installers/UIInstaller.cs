using UnityEngine;

public class UIInstaller : Installer
{
    [SerializeField] private UIController _uiController;

    public override void Install(AllServices allServices)
    {
        allServices.RegisterSingle(_uiController);
    }
}