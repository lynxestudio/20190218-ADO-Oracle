using System;
using Oracle.ManagedDataAccess.Client;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace Samples.UpdateCommand
{
	class Program
	{
		public static void Main(string[] args)
		{
			int rowAffected = 0;
			bool success = false;
			OracleTransaction transaction = null;
			Employee e = Util.ScanEmployee();
            StringBuilder buf = new StringBuilder("UPDATE employees SET ");
            buf.Append(" first_name = :prmFirstName");
            buf.Append(" ,last_name = :prmLastName");
            buf.Append(" ,email = :prmEmail");
            buf.Append(" ,phone_number = :prmPhoneNumber");
            buf.Append(" ,hire_date = :prmHireDate");
            buf.Append(" ,salary = :prmSalary");
            buf.Append(" ,commission_pct = :prmCommission");
            buf.Append(" WHERE employee_id = :prmEmployeeId ");
            List<OracleParameter> parameters = new List<OracleParameter>();
            parameters.Add(new OracleParameter("prmFirstName",OracleDbType.Varchar2,e.FirstName, ParameterDirection.Input));
            parameters.Add(new OracleParameter("prmLastName",OracleDbType.Varchar2 ,e.LastName, ParameterDirection.Input));
            parameters.Add(new OracleParameter("prmEmail", OracleDbType.Varchar2,e.Email, ParameterDirection.Input));
            parameters.Add(new OracleParameter("prmPhoneNumber",OracleDbType.Varchar2,e.PhoneNumber, ParameterDirection.Input));
            parameters.Add(new OracleParameter("prmHireDate",OracleDbType.Date,e.HireDate, ParameterDirection.Input));
            parameters.Add(new OracleParameter("prmSalary",e.Salary));
            parameters.Add(new OracleParameter("prmCommission",e.Commission));
           	parameters.Add(new OracleParameter("prmEmployeeId", OracleDbType.Int32,
                                               e.EmployeeId,
                                               ParameterDirection.Input));
            using(OracleConnection conn = ConnectionManager.GetConnection())
            {
            	transaction = conn.BeginTransaction(IsolationLevel.Serializable);
            	using(OracleCommand cmd = new OracleCommand(buf.ToString(),conn))
            	{
            		try {
            			cmd.CommandType = CommandType.Text;
            			foreach (var element in parameters) {
            				cmd.Parameters.Add(element);
            			}
            			rowAffected = cmd.ExecuteNonQuery();
            			if(rowAffected > 0)
            				success = true;
            			Console.WriteLine("{0} row(s) affected",rowAffected);
            		} catch (Exception ex) {
            			Console.WriteLine(ex.Message);
            		}
            		finally{
            			if(success)
            				transaction.Commit();
            			else
            				transaction.Rollback();
            			ConnectionManager.CloseConnection();
            		}
            	}
            }
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}