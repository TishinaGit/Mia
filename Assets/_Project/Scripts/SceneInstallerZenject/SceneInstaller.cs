using _Project.Scripts.LevelSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.SceneInstallerZenject
{
    public class SceneInstaller :  MonoInstaller
    {
        public ScrollRect ScrollRect;
        public RectTransform Content;  
        public ScrollBoundaryHandler ScrollBoundaryHandler;
    
        public override void InstallBindings()
        {
            ScrollRectObject();
            ContentObject();
            ScrollBoundaryHandlerCs();
        }

        private void ScrollRectObject() => Container.Bind<ScrollRect>().FromInstance(ScrollRect).AsSingle(); 
    
        private void ContentObject() => Container.Bind<RectTransform>().FromInstance(Content).AsSingle(); 
    
        private void ScrollBoundaryHandlerCs() => Container.Bind<ScrollBoundaryHandler>().FromInstance(ScrollBoundaryHandler).AsSingle();  
    }
}
