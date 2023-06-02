using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace BeatSaberRPC.Installers
{
    internal class MenuPresenceInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MenuPresenceController>().FromNewComponentOnRoot().AsSingle();
        }
    }
}
