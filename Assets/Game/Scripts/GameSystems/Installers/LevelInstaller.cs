using UnityEngine;

public class LevelInstaller : Installer
{
    [SerializeField] private PlayersController _playersController;
    [SerializeField] private LevelController _levelController;

    public override void Install(AllServices allServices)
    {
        allServices.RegisterSingle(_playersController);
        allServices.RegisterSingle(_levelController);
    }
}