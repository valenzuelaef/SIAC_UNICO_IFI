using Claro.Data;

namespace Claro.SIACU.Data.IFI.Configuration
{
    internal struct DbConnectionConfiguration : IDbConnectionConfiguration
    {
        public static readonly IDbConnectionConfiguration SIAC_POST_BSCS = Create("SIAC_POST_BSCS");
        public static readonly IDbConnectionConfiguration SIAC_POST_CLARIFY = Create("SIAC_POST_CLARIFY");
        public static readonly IDbConnectionConfiguration SIAC_POST_DB = Create("SIAC_POST_DB");
        public static readonly IDbConnectionConfiguration SIAC_POST_EAIAVM = Create("SIAC_POST_EAIAVM");
        public static readonly IDbConnectionConfiguration SIAC_POST_TIMEAI = Create("SIAC_POST_TIMEAI");
        public static readonly IDbConnectionConfiguration SIAC_POST_GWP = Create("SIAC_POST_GWP");
        public static readonly IDbConnectionConfiguration SIAC_POST_PVU = Create("SIAC_POST_PVU");
        public static readonly IDbConnectionConfiguration SIAC_POST_SGA = Create("SIAC_POST_SGA");
        public static readonly IDbConnectionConfiguration SIAC_POST_SIGA = Create("SIAC_POST_SIGA");        
        public static readonly IDbConnectionConfiguration SIAC_POST_DBTO = Create("SIAC_POST_DBTO");
        public static readonly IDbConnectionConfiguration SIAC_POST_CAE = Create("SIAC_POST_CAE");



        #region [Fields]

        private string m_name;

        #endregion

        #region [ Properties ]

        #region Name

        public string Name
        {
            get
            {
                return this.m_name;
            }
        }

        #endregion

        #endregion

        #region SetName

        private void SetName(string name)
        {
            this.m_name = name;
        }

        #endregion

        private static IDbConnectionConfiguration Create(string name)
        {
            DbConnectionConfiguration helper = new DbConnectionConfiguration();

            helper.SetName(name);

            return helper;
        }
    }
}
