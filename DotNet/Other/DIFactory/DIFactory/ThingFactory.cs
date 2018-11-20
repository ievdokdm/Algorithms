using System;
using Unity;

namespace DIFactory
{
    public class ThingFactory
    {
        private readonly IUnityContainer container;

        /*public Func<IUnityContainer> FuncUnityContainer { get; set; }

        protected IUnityContainer Container
        {
            get
            {
                if (container is null && FuncUnityContainer != null)
                    container = FuncUnityContainer();
                return container;
            }
        }*/

        public ThingFactory(IUnityContainer container)
        {
            this.container = container;
        }

        public IThing CreateThing(string name)
        {
            IThing thing = null;
            if(container.IsRegistered<IThing>(name))
                thing = container.Resolve<IThing>(name);

            return thing;
        }
    }
}
