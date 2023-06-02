using Zenject;

namespace BeatSaberRPC.Installers
{
    internal class GamePresenceInstaller: Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GamePresenceController>().AsSingle();
        }
    }
}
