using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AstroAssistant
{

    /// <summary>
    /// Contexte de l'application
    /// </summary>
    public class ApplicationContext : AppContext
    {
        IContainer _CurrentContainer;

        /// <summary>
        /// Création d'un nouveau contexte
        /// </summary>
        public ApplicationContext()
        {
            BuildContainer();
        }

        /// <summary>
        /// Construction du conteneur IoC
        /// </summary>
        void BuildContainer()
        {
            var builder = new ContainerBuilder();
            var asm = new Assembly[] { this.GetType().Assembly, typeof(AppContext).Assembly };
            // Enregistrement des services
            builder
                .RegisterAssemblyTypes(asm)
                .Where(tp => tp.Name.EndsWith("Service", StringComparison.OrdinalIgnoreCase))
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance()
                ;
            // Enregistrement des ViewModels
            builder
                .RegisterAssemblyTypes(asm)
                .Where(tp => tp.Name.EndsWith("ViewModel", StringComparison.OrdinalIgnoreCase))
                .AsSelf()
                .InstancePerDependency()
                ;
            // Création ou modification du conteneur
            if (_CurrentContainer == null)
                _CurrentContainer = builder.Build();
            else
                builder.Update(_CurrentContainer);
        }

        /// <summary>
        /// Résoud un service
        /// </summary>
        public override T GetService<T>()
        {
            return _CurrentContainer.Resolve<T>();
        }

        /// <summary>
        /// Résoud un viewmodel
        /// </summary>
        public override T CreateViewModel<T>()
        {
            return _CurrentContainer.Resolve<T>();
        }
    }

}
