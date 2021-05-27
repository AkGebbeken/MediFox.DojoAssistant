using System;

namespace MediFox.DojoAssistant.Exceptions
{
	public class InvalidNameException : ArgumentException
	{
		public InvalidNameException()
		{
			
		}

		public InvalidNameException(string message) : base(message)
		{
			
		}
	}
}