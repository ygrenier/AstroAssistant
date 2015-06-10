using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astro
{

    /// <summary>
    /// Outils de sérialisation d'un thème
    /// </summary>
    public class NatalChartSerializer
    {
        /// <summary>
        /// Sérialisation d'une définition de thème
        /// </summary>
        public Task Serialize(NatalChartDefinition definition, Stream stream)
        {
            if (definition == null) throw new ArgumentNullException("definition");
            if (stream == null) throw new ArgumentNullException("stream");
            return Task.Factory.StartNew(() => {
                throw new NotImplementedException();
            });
        }
    }

}
