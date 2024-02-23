using Zenject;

public class LayerIntallers : MonoInstaller
{
    public override void InstallBindings()
    {
        InfLayerInstaller.Install(Container);
        AppLayerInstaller.Install(Container);
        PreLayerInstaller.Install(Container);
    }
}