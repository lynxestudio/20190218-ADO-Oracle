# How to execute Oracle parameterized commands with ODP.NET (Oracle)

<p align="justify">
SQL statements can receive input-only parameters, output-only parameters, and bidirectional parameters. You can use a OracleCommand object to execute parameterized SQL statements. To execute a parameterized SQL statement use the following steps:
</p>
<p align="justify">
<ol>
<li>Open a database connection,use OracleConnection.</li>
<li>Create and initialize an OracleCommand object.</li>
<li>Create a OracleParameter object, for each input parameter required by the SQL statement. Specify the name, type size, and value for each parameter, and add it to the parameters collection of the command object.</li>
<li>Execute the command by calling the ExecuteScalar, ExecuteReader, ExecuteXmlReader, or ExecuteNonQuery method, as appropriate for the type of SQL statement.</li>
<li>Use the return value obtained by executing the command.</li>
<li>Dispose the command object.</li>
<li>Close the database connection.</li>
</ol>
</p>
<p align="justify">
The following example shows how to execute a SQL statement that updates employee by searching for employee id.
</p>
<i>Fig 1. Example Main Menu</i>
<img src="images/fig1.png" width="893" height="364" alt="">
<p align="justify">
Choosing the first option queries the database and fetch all records.
</p>
<i>Fig 2. Showing all records</i>
<img src="images/fig2.png" width="900" height="626" alt="">
<p align="justify">
With the second option, we can search for one record then updates some properties.
</p>
<img src="images/fig3.png" width="892" height="624" alt="">
<p>
Finally, the record is updated.
</p>
<img src="images/fig4.png" width="901" height="627" alt="">

