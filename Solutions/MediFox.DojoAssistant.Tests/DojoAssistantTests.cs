using System;
using FluentAssertions;
using MediFox.DojoAssistant.Enums;
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
			
			dojoAssistant.StartRound();
			
			var dojoAction = new Action(() => dojoAssistant.StartRound());
			
			dojoAction.Should().Throw<InvalidOperationException>();
		}

		[Fact]
		public void StartRound_IsRoundActive_DojoStateShouldBeActive()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.StartRound();
			
			var currentDojoState = dojoAssistant.DojoState;

			currentDojoState.Should().Be(State.Active);
		}

		[Fact]
		public void StartRound_IsRoundInacitve_RoundShouldStart()
		{
			var dojoAssistant = new DojoAssistant(60);
			
			dojoAssistant.StartRound();
			
			dojoAssistant.IsRoundActive.Should().BeTrue();
		}
	}
}