using System;
using FluentAssertions;
using MediFox.DojoAssistant.Enums;
using MediFox.DojoAssistant.Exceptions;
using Xunit;

namespace MediFox.DojoAssistant.Tests
{
	public class DojoAssistantTests
	{
		[Fact]
		public void Constructor_CreateNewDojoAssistant_DojoStateShouldBeIdle()
		{
			var dojoAssistant = new DojoAssistant(60);
			var currentDojoState = dojoAssistant.DojoState;

			currentDojoState.Should().Be(State.Idle);
		}

		[Fact]
		public void StartRound_IsRoundActive_ThrowInvalidOperationException()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			dojoAssistant.AddParticipant("Jane Doe");
			dojoAssistant.StartRound();
			
			var dojoAction = new Action(() => dojoAssistant.StartRound());
			
			dojoAction.Should().Throw<InvalidOperationException>();
		}

		[Fact]
		public void StartRound_IsRoundActive_DojoStateShouldBeActive()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			dojoAssistant.AddParticipant("Jane Doe");
			dojoAssistant.StartRound();
			
			var currentDojoState = dojoAssistant.DojoState;

			currentDojoState.Should().Be(State.Active);
		}
		
		[Fact]
		public void StartRound_IsRoundInacitve_RoundShouldStart()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			dojoAssistant.AddParticipant("Jane Doe");
			
			dojoAssistant.StartRound();
			
			dojoAssistant.IsRoundActive.Should().BeTrue();
		}
		
		[Fact]
		public void StartRound_IsParticipantCountLessThanTwo_ThrowInvalidAmountException()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			
			var dojoAction = new Action(() => dojoAssistant.StartRound());
			
			dojoAction.Should().Throw<InvalidAmountException>();
		}

		[Fact]
		public void GetRemainingTimeInSeconds_IsRoundInactive_RemainingTimeShouldBeZero()
		{
			var dojoAssistant = new DojoAssistant(60);

			var isRemainingTimeZero = dojoAssistant.GetRemainingTimeInSeconds() == 0;

			isRemainingTimeZero.Should().BeTrue();
		}

		[Fact]
		public void GetRemainingTimeInSeconds_IsRoundActive_RemainingTimeShouldBeLessThanStartTime()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			dojoAssistant.AddParticipant("JaneDoe");
			dojoAssistant.StartRound();

			var remainingTimeInSeconds = dojoAssistant.GetRemainingTimeInSeconds();

			remainingTimeInSeconds.Should().BeInRange(1, 60);
		}
	}
}