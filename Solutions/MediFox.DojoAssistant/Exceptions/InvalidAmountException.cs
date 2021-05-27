using System;

namespace MediFox.DojoAssistant.Exceptions
{
	public class InvalidAmountException : InvalidOperationException
	{
		public InvalidAmountException()
		{
			
		}

		public InvalidAmountException(string message) : base(message)
		{
			
		}
	}
}