using System;
using System.Collections.Generic;
using System.Linq;
using MediFox.DojoAssistant.Enums;
using MediFox.DojoAssistant.Exceptions;

namespace MediFox.DojoAssistant
{
	public class DojoAssistant
	{
		private readonly List<string> _participants = new List<string>();
		private readonly int _roundTimeInSeconds;
		
		public IReadOnlyCollection<string> Participants => _participants.AsReadOnly();
		
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
			
			if (_participants.Count <= 1)
			{
				throw new InvalidAmountException();
			}

			DojoState = State.Active;
			IsRoundActive = true;
		}
		
		public void AddParticipant(string participantName)
		{
			if (DojoState == State.Active)
			{
				throw new InvalidOperationException();
			}

			if (string.IsNullOrWhiteSpace(participantName))
			{
				throw new ArgumentException();
			}

			if (Participants.Contains(participantName))
			{
				throw new InvalidNameException();
			}
			
			_participants.Add(participantName);
		}

		public void RemoveParticipant(string participantName)
		{
			if (DojoState == State.Active)
			{
				throw new InvalidOperationException();
			}

			if (_participants.Contains(participantName) == false)
			{
				throw new InvalidNameException();
			}
			
			_participants.Remove(participantName);
		}
		
		public void RemoveParticipants()
		{
			if (DojoState == State.Active)
			{
				throw new InvalidOperationException();
			}
			
			_participants.Clear();
		}

		public void ShuffleParticipants()
		{
			if (DojoState == State.Active)
			{
				throw new InvalidOperationException();
			}

			var participantCount = Participants.Count;
			var randomValue = new Random();

			for (var i = 0; i < participantCount; i++)
			{
				var participantToSwap = randomValue.Next(participantCount);
				var participantForSwap = randomValue.Next(participantCount);
				var participantTemp = _participants[participantToSwap];

				_participants[participantToSwap] = _participants[participantForSwap];
				_participants[participantForSwap] = participantTemp;
			}
		}
	}
}