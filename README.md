# Oracle Recipe #5 How to execute Oracle parameterized commands with ODP.NET

SQL statements can receive input-only parameters, output-only parameters, and bidirectional parameters. You can use a OracleCommand object to execute parameterized SQL statements. To execute a parameterized SQL statement use the following steps:

Open a database connection,use OracleConnection.
Create and initialize an OracleCommand object.
Create a OracleParameter object, for each input parameter required by the SQL statement. Specify the name, type size, and value for each parameter, and add it to the parameters collection of the command object.
Execute the command by calling the ExecuteScalar, ExecuteReader, ExecuteXmlReader, or ExecuteNonQuery method, as appropriate for the type of SQL statement.
Use the return value obtained by executing the command.
Dispose the command object.
Close the database connection.
The following example shows how to execute a SQL statement that updates employee by employee id (please, check this post for further information).
The SQL statement requires the following parameters: prmFirstName , prmLastName, prmEmail, prmPhoneNumber, prmHireDate, prmSalary,prmCommission and prmEmployeeId.

Fig 1. The application code.


Download example source code.
