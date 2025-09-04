using System;
using System.Data;
using Helpers;
using Oracle.ManagedDataAccess.Client;
using System.Text;

try
{
    QueryEmployee();
    UpdateEmployee();
}
catch (Exception ex)
{
    Console.WriteLine();
}
Console.Write("Press any key to continue...");
Console.ReadKey(true);

static void QueryEmployee()
{
        List<Employee> employees = new List<Employee>();
            Employee employee = null;
            string commandText = "Select * from employees";
                using (OracleConnection conn = ConnectionManager.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand(commandText, conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        using (OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            employee = new Employee();
                            employee.Id = reader.GetInt32(0);
                            employee.FirstName = reader.GetString(1);
                            employee.LastName = reader.GetString(2);
                            employee.Email = reader.GetString(3);
                            employee.PhoneNumber = reader.GetString(4);
                            employee.HireDate = reader.GetString(5);
                            employee.JobId = reader.GetString(6);
                            employee.Salary = reader.GetString(7);
                            employee.CommissionPct = reader.GetString(8);
                            employee.ManagerId = reader.GetString(9);
                            employee.DepartmentId = reader.GetString(10);
                            employees.Add(employee);
                        }
                    }
                }
            
            //printing results
            foreach (var item in employees)
            {
                Console.WriteLine("[ ID ]\t[ FIRST NAME ]\t[ LAST NAME ]\t[ EMAIL ]\t[ SALARY ]");
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",item.Id,
                    item.FirstName,
                    item.LastName,
                    item.Email,
                    item.Salary);
            }
}

static void UpdateEmployee()
{
    int rowAffected = 0;
            bool success = false;
            OracleTransaction transaction = null;
            Employee e = Utilities.ScanEmployee();
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
            parameters.Add(new OracleParameter("prmFirstName", OracleDbType.Varchar2, e.FirstName, ParameterDirection.Input));
            parameters.Add(new OracleParameter("prmLastName", OracleDbType.Varchar2, e.LastName, ParameterDirection.Input));
            parameters.Add(new OracleParameter("prmEmail", OracleDbType.Varchar2, e.Email, ParameterDirection.Input));
            parameters.Add(new OracleParameter("prmPhoneNumber", OracleDbType.Varchar2, e.PhoneNumber, ParameterDirection.Input));
            parameters.Add(new OracleParameter("prmHireDate", OracleDbType.Date, e.HireDate, ParameterDirection.Input));
            parameters.Add(new OracleParameter("prmSalary", e.Salary));
            parameters.Add(new OracleParameter("prmCommission", e.CommissionPct));
            parameters.Add(new OracleParameter("prmEmployeeId", OracleDbType.Int32,
                                               e.Id,
                                               ParameterDirection.Input));
            using (OracleConnection conn = ConnectionManager.GetConnection())
            {
                transaction = conn.BeginTransaction(IsolationLevel.Serializable);
                using (OracleCommand cmd = new OracleCommand(buf.ToString(), conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.Text;
                        foreach (var element in parameters)
                        {
                            cmd.Parameters.Add(element);
                        }
                        rowAffected = cmd.ExecuteNonQuery();
                        if (rowAffected > 0)
                            success = true;
                        Console.WriteLine("{0} row(s) affected", rowAffected);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        if (success)
                            transaction.Commit();
                        else
                            transaction.Rollback();
                        ConnectionManager.CloseConnection();
                    }
                }
            }
}

