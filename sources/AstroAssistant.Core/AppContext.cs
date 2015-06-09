using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroAssistant
{
    /// <summary>
    /// Contexte d'une application
    /// </summary>
    public abstract class AppContext
    {

        #region Gestion du contexte static

        static AppContext _Current;
        static Func<AppContext> _AppContextBuilder;

        /// <summary>
        /// Création d'un contexte
        /// </summary>
        static AppContext CreateAppContext()
        {
            if (_AppContextBuilder == null)
                throw new InvalidOperationException(Resources.Locales.AppContextBuilderNotDefined);
            var result = _AppContextBuilder();
            if(result==null)
                throw new InvalidOperationException(Resources.Locales.AppContextBuildFailed);
            return result;
        }

        /// <summary>
        /// Défini le constructeur d'un contexte d'application
        /// </summary>
        public static void Build(Func<AppContext> builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");
            _AppContextBuilder = builder;
        }

        /// <summary>
        /// Contexte d'application en cours
        /// </summary>
        public static AppContext Current { get { return _Current ?? (_Current = CreateAppContext()); } }

        #endregion

    }
}
