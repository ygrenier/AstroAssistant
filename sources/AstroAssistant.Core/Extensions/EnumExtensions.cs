using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAssistant
{
    /// <summary>
    /// Extensions sur les énumérés
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Calcul le libellé d'un énuméré
        /// </summary>
        public static String GetCaptionString<T>(this T value) where T : struct, IComparable, IFormattable
        {
            var result = AstroAssistant.Resources.Locales.ResourceManager.GetString(String.Format("{0}_{1}_Caption", typeof(T).Name, value.ToString()));
            if (String.IsNullOrWhiteSpace(result))
                result = value.ToString();
            return result;
        }
    }
}
