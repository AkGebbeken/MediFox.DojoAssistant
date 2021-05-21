using System;
using MediFox.DojoAssistant.Enums;

namespace MediFox.DojoAssistant
{
	public class DojoAssistant
	{
		private readonly int _roundTimeInSeconds;
		
		public State DojoState { get; private set; }
		
		public bool IsRoundActive { get; private set; }
		
		public DojoAssistant(int roundTimeInSeconds)
		{
			_roundTimeInSeconds = roundTimeInSeconds;
			DojoState = State.Idle;
		}

		public void StartRound()
		{
			if (IsRoundActive)
			{
				throw new InvalidOperationException();
			}

			DojoState = State.Active;
			IsRoundActive = true;
		}
	}
}