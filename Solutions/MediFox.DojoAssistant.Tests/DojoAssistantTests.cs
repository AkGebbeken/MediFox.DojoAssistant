using System;
using System.Threading;
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
		public void StartRound_IsRoundActive_PilotShouldBeSet()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			dojoAssistant.AddParticipant("Jane Doe");

			dojoAssistant.StartRound();
			var pilot = dojoAssistant.Pilot;

			pilot.Should().Be("John Doe");
		}
		
		[Fact]
		public void StartRound_IsRoundActive_CoPilotShouldBeSet()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			dojoAssistant.AddParticipant("Jane Doe");

			dojoAssistant.StartRound();
			var pilot = dojoAssistant.CoPilot;

			pilot.Should().Be("Jane Doe");
		}

		[Fact]
		public void StartRound_IsRoundInacitve_RoundShouldStart()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			dojoAssistant.AddParticipant("Jane Doe");
			
			dojoAssistant.StartRound();
			var isRoundAcitve = dojoAssistant.IsRoundActive;
			
			isRoundAcitve.Should().BeTrue();
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
		public void PauseRound_IsRoundInactive_ThrowInvalidOperationException()
		{
			var dojoAssistant = new DojoAssistant(60);

			var dojoAction = new Action(() => dojoAssistant.PauseRound());

			dojoAction.Should().Throw<InvalidOperationException>();
		}

		[Fact]
		public void PauseRound_IsRoundActive_IsRoundPausedShouldBeTrue()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			dojoAssistant.AddParticipant("Jane Doe");
			dojoAssistant.StartRound();

			dojoAssistant.PauseRound();
			var isRoundPaused = dojoAssistant.IsRoundPaused;
			
			isRoundPaused.Should().BeTrue();
		}

		[Fact]
		public void PauseRound_IsRoundPaused_RemainingTimeInSecondsShouldNotBeZero()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			dojoAssistant.AddParticipant("Jane Doe");
			dojoAssistant.StartRound();

			dojoAssistant.PauseRound();
			var remainingTimeInSeconds = dojoAssistant.RemainingTimeInSeconds;

			remainingTimeInSeconds.Should().NotBe(0);
		}

		[Fact]
		public void PauseRound_IsRoundPaused_TimerShouldBeStopped()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			dojoAssistant.AddParticipant("Jane Doe");
			dojoAssistant.StartRound();
			
			dojoAssistant.PauseRound();
			var isTimerStopped = dojoAssistant.GetRemainingTimeInSeconds() == 0;

			isTimerStopped.Should().BeTrue();
		}

		[Fact]
		public void ResumeRound_IsRoundNotPaused_ThrowInvalidOperationException()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			dojoAssistant.AddParticipant("Jane Doe");
			dojoAssistant.StartRound();

			var dojoAction = new Action(() => dojoAssistant.ResumeRound());

			dojoAction.Should().Throw<InvalidOperationException>();
		}

		[Fact]
		public void ResumeRound_IsRoundPaused_RoundShouldStart()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			dojoAssistant.AddParticipant("Jane Doe");
			dojoAssistant.StartRound();
			dojoAssistant.PauseRound();
			
			dojoAssistant.ResumeRound();
			var isRoundActive = dojoAssistant.IsRoundActive;

			isRoundActive.Should().BeTrue();
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
			dojoAssistant.AddParticipant("Jane Doe");
			dojoAssistant.StartRound();

			var remainingTimeInSeconds = dojoAssistant.GetRemainingTimeInSeconds();

			remainingTimeInSeconds.Should().BeInRange(1, 60);
		}

		[Fact]
		public void IsRoundActive_IsRoundOver_IsRoundActiveShouldBeFalse()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			dojoAssistant.AddParticipant("Jane Doe");
			dojoAssistant.StartRound();

			Thread.Sleep(70000);
			var isRoundActive = dojoAssistant.IsRoundActive;

			isRoundActive.Should().BeFalse();
		}
	}
}