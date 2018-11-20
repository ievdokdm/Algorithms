using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;
using Unity;

namespace DIFactory
{
    class Program
    {
        static readonly IUnityContainer container = GetUnityContainerViaConfig();

        static IUnityContainer GetUnityContainerViaConfig()
        {
            IUnityContainer container = new UnityContainer();
            UnityConfigurationSection section =
                (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            if (section != null && section.Containers.Count > 0)
                section.Configure(container);
            return container;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            var thingFactory = container.Resolve<ThingFactory>();
            foreach (var arg in args)
            {
                var thing = thingFactory.CreateThing(arg);
                Console.WriteLine(thing.Description);
            }
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}
