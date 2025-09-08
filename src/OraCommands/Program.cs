using System;
using System.Data;
using Helpers;
using Oracle.ManagedDataAccess.Client;
using System.Text;


int option = 0;
string? soption = "0";
do
{
    try
    {
        string[] options = { "Query employees", "Update employee" };
        Utilities.ShowMenu("Oracle ADO.NET Examples", options);
        soption = Utilities.Scanf("Please enter your choice: ");
        Int32.TryParse(soption, out option);
        if (string.IsNullOrEmpty(soption))
            soption = "0";
        else
        {
            if (soption.Equals("0"))
                System.Environment.Exit(0);
        }
        switch (soption)
        {
            case "1":
                QueryEmployee();
                break;
            case "2":
                UpdateEmployee();
                break;
            default:
                System.Console.WriteLine("\tPlease choose an option!");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Utilities.Pause();
    }
} while (!soption.Equals("0"));

static void QueryEmployeeById()
{
    Employee employee = new Employee();
    int id = 0;
    string? sid = null;
    do
    {
        sid = Utilities.Scanf("Enter the employee ID");
    } while (sid == null);
    Int32.TryParse(sid, out id);
    
    string commandText = @"select EMPLOYEE_ID,FIRST_NAME, LAST_NAME, EMAIL, JOB_ID, SALARY, DEPARTMENT_ID from employees WHERE EMPLOYEE_ID = :prmEmployeeId";
    using (OracleConnection conn = ConnectionManager.GetConnection())
    {
        using (OracleCommand cmd = new OracleCommand(commandText, conn))
        {
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new OracleParameter("prmEmployeeId", id));

            using (OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
            {

                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        employee.Id = reader.GetInt32(0);
                        employee.FirstName = reader.GetString(1) != null ? reader.GetString(1) : "NULL";
                        employee.LastName = reader.GetString(2) != null ? reader.GetString(2) : "NULL";
                        employee.Email = reader.GetString(3) != null ? reader.GetString(3) : "NULL";
                        employee.JobId = reader.GetString(4) != null ? reader.GetString(4) : "NULL";
                        employee.Salary = reader.GetString(5) != null ? reader.GetString(5) : "NULL";
                        if (reader.GetValue(6) == DBNull.Value)
                            employee.DepartmentId = "NULL";
                        else
                            employee.DepartmentId = reader.GetString(6);
                    }
                    else
                        Utilities.PrintMessage("No records found!");
                }
            }
        }
    }
    Console.WriteLine("\t{0} | {1} | {2} | {3} | {4} | {5} | {6}", employee.Id,
        employee.FirstName,
        employee.LastName,
        employee.Email,
        employee.JobId,
        employee.Salary,
        employee.DepartmentId);

}

static void QueryEmployee()
{
    System.Console.WriteLine("Fetching data...");
    List<Employee> employees = new List<Employee>();

    string commandText = @"select EMPLOYEE_ID, FIRST_NAME, LAST_NAME, EMAIL, JOB_ID, SALARY, DEPARTMENT_ID
from employees";
    using (OracleConnection conn = ConnectionManager.GetConnection())
    {
        using (OracleCommand cmd = new OracleCommand(commandText, conn))
        {
            cmd.CommandType = CommandType.Text;

            using (OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
            {
                while (reader.Read())
                {
                    Employee employee = new Employee();
                    employee.Id = reader.GetInt32(0);
                    employee.FirstName = reader.GetString(1) != null ? reader.GetString(1) : "NULL";
                    employee.LastName = reader.GetString(2) != null ? reader.GetString(2) : "NULL";
                    employee.Email = reader.GetString(3) != null ? reader.GetString(3) : "NULL";
                    employee.JobId = reader.GetString(4) != null ? reader.GetString(4) : "NULL";
                    employee.Salary = reader.GetString(5) != null ? reader.GetString(5) : "NULL";
                    if (reader.GetValue(6) == DBNull.Value)
                        employee.DepartmentId = "NULL";
                    else
                        employee.DepartmentId = reader.GetString(6);
                    employees.Add(employee);

                }

            }
        }
    }
    Console.WriteLine("\t[ ID ] [ FIRST NAME ] [ LAST NAME ] [ EMAIL ] [JOB ID] [ SALARY ] [DEPARTMENT]");

    //printing results
    foreach (var item in employees)
    {
        Console.WriteLine("\t{0} | {1} {2} {3} {4} {5} {6}", item.Id,
        item.FirstName,
        item.LastName,
        item.Email,
        item.JobId,
        item.Salary,
        item.DepartmentId);
    }
    Utilities.Pause();
}

static void UpdateEmployee()
{
    QueryEmployeeById();
    Utilities.PrintMessage("Update an employee");
    int rowAffected = 0;
    bool success = false;
    Employee e = Utilities.ScanEmployee();
    StringBuilder buf = new StringBuilder("UPDATE employees SET ");
    buf.Append(" first_name = :prmFirstName");
    buf.Append(" ,last_name = :prmLastName");
    buf.Append(" ,email = :prmEmail");
    buf.Append(" ,salary = :prmSalary");
    buf.Append(" WHERE employee_id = :prmEmployeeId ");
    List<OracleParameter> parameters = new List<OracleParameter>();
    parameters.Add(new OracleParameter("prmFirstName", OracleDbType.Varchar2, e.FirstName, ParameterDirection.Input));
    parameters.Add(new OracleParameter("prmLastName", OracleDbType.Varchar2, e.LastName, ParameterDirection.Input));
    parameters.Add(new OracleParameter("prmEmail", OracleDbType.Varchar2, e.Email, ParameterDirection.Input));
    parameters.Add(new OracleParameter("prmSalary", OracleDbType.Int32, e.Salary, ParameterDirection.Input));
    parameters.Add(new OracleParameter("prmEmployeeId", OracleDbType.Int32,
                                       e.Id,
                                       ParameterDirection.Input));
    using (OracleConnection conn = ConnectionManager.GetConnection())
    {
        OracleTransaction transaction = conn.BeginTransaction(IsolationLevel.Serializable);
        using (OracleCommand cmd = new OracleCommand(buf.ToString(), conn))
        {
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddRange(parameters.ToArray());
                rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                    success = true;
                Utilities.PrintMessage(rowAffected + " row(s) affected");
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
    Utilities.Pause();
}

