/*
 * Created by SharpDevelop.
 * User: Ratzinger
 * Date: 2/17/2019
 * Time: 10:47 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Samples.UpdateCommand
{
	/// <summary>
	/// Description of Util.
	/// </summary>
	public static class Util
	{
		internal static Employee ScanEmployee()
        {
            var e = new Employee();
            string[] labels = { "Employee ID: ",
                "First name: ", "Last name: ", "Email: ", "Phone: "
                ,"Hire Date: ","Commission","Salary: "
            };
            var fields = new string[labels.Length];
            for (var i = 0; i < labels.Length; i++)
            {
                fields[i] = Scanf(labels[i]);
            }
            if(!string.IsNullOrEmpty(fields[0]))
            	e.EmployeeId = Convert.ToInt32(fields[0]);
            e.FirstName = fields[1];
            e.LastName = fields[2];
            e.Email = fields[3];
            e.PhoneNumber = fields[4];
            e.HireDate = fields[5];
            if(!string.IsNullOrEmpty(fields[6]))
            	e.Commission = Convert.ToDouble(fields[6]);
            if(!string.IsNullOrEmpty(fields[7]))
            	e.Salary = Convert.ToDecimal(fields[7]);
            return e;
        }
		
		
		internal static string Scanf(string message)
        {
            Console.Write("\t[ " + message + " ]\t");
            return Console.ReadLine();
        }
		
	}
}
