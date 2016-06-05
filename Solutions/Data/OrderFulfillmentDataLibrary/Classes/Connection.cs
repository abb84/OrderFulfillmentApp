

using System;
using System.Configuration;

namespace RepZio.Ofa.Data.OrderFulfillmentDataLibrary.Classes
{
    /// <summary>
    /// Connection Manager
    /// </summary>
    public static class Connection
    {
        #region " Properties... "
        private const string SQL_CONNECTION_KEY = "OrderFulfilmentEntities";

        private static string _connectionString;
        #endregion

        #region " Public Methods... "

        private static readonly object _lockObject = new object();

        /// <summary>0
        /// Gets the SQL connection.
        /// </summary>
        /// <value>The SQL connection.</value>
        internal static string SqlConnection
        {
            get
            {
                if (String.IsNullOrEmpty(_connectionString))
                {
                    lock (_lockObject)
                    {
                        if (String.IsNullOrEmpty(_connectionString))
                        {
                            if (ConfigurationManager.ConnectionStrings[SQL_CONNECTION_KEY] == null)
                            {
                                throw new MissingFieldException(String.Format("Connection string missing for - {0}", SQL_CONNECTION_KEY));
                            }

                            _connectionString = ConfigurationManager.ConnectionStrings[SQL_CONNECTION_KEY].ConnectionString;
                        }
                    }
                }
                return _connectionString;
            }
        }

        #endregion
    }
}
