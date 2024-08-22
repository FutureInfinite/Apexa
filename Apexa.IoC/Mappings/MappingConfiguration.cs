using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC = Apexa.DataContracts;
using Entity = Apexa.DataAccess.Context.Entities;

namespace Apexa.IoC.Mappings
{
    public static class MappingConfiguration
    {

        #region Properties&Attributes
        private static bool Compiled = false;
        #endregion Properties&Attributes

        #region Operations
        public static void AddMappings()
        {
            if (!Compiled)
            {
                Compiled = true;

                #region Advisor
                TypeAdapterConfig.GlobalSettings.ForType<DC.Advisor, Entity.Advisor>();
                #endregion Advisor
            }
        }
        #endregion Operations
    }
}
