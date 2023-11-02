/*
 * Created by SharpDevelop.
 * User: Ratzinger
 * Date: 2/17/2019
 * Time: 1:03 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Configuration;

namespace Samples.UpdateCommand
{
	/// <summary>
	/// Description of ConnectionManager.
	/// </summary>
	internal static class ConnectionManager
	{
		static OracleConnection connection = null;
		public static OracleConnection GetConnection()
        {
            string connectionString = null;
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["Oracle"].ConnectionString;
                connection = new OracleConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (ConfigurationException configex)
            {
                throw new ApplicationException("Cannot find a connectionString in config", configex);
            }
            catch (OracleException ex)
            {
                throw new ApplicationException("Cannot connect to datasource: [{connectionString}]", ex);
            }
        }
        public static void CloseConnection()
        {
            if (connection != null)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection = null;
                }
            }
        }
	}
}
