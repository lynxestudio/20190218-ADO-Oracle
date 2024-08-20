/*
 * Created by SharpDevelop.
 * User: Ratzinger
 * Date: 2/17/2019
 * Time: 12:53 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Samples.UpdateCommand
{
	/// <summary>
	/// Description of Employee.
	/// </summary>
	public class Employee
	{
		public int? EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string HireDate { get; set; }
        public decimal? Salary { get; set; }
        public double? Commission { get; set; }
	}
}
