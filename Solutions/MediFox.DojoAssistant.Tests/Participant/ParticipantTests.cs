using System;
using System.Linq;
using FluentAssertions;
using MediFox.DojoAssistant.Exceptions;
using Xunit;

namespace MediFox.DojoAssistant.Tests.Participant
{
	public class ParticipantTests
	{
		[Fact]
		public void AddParticipant_IfDojoStateIsActive_ThrowInvalidOperationException()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			dojoAssistant.AddParticipant("Jane Doe");
			dojoAssistant.StartRound();

			var dojoAction = new Action(() => dojoAssistant.AddParticipant("Richard Doe"));

			dojoAction.Should().Throw<InvalidOperationException>();
		}
		
		[Fact]
		public void AddParticipant_IfDojoStateIsIdle_ParticipantShouldBeAdded()
		{
			var dojoAssistant = new DojoAssistant(60);
			var participantName = "John Doe";
			
			dojoAssistant.AddParticipant(participantName);
			var dojoContainsParticipant = dojoAssistant.Participants.Contains(participantName);

			dojoContainsParticipant.Should().BeTrue();
		}

		[Fact]
		public void AddParticipant_IfParticipantNameIsWhitespace_ThrowArgumentException()
		{
			var dojoAssistant = new DojoAssistant(60);

			var dojoAction = new Action(() => dojoAssistant.AddParticipant(" "));

			dojoAction.Should().Throw<ArgumentException>();
		}
		
		[Fact]
		public void AddParticipant_IfParticipantNameIsAlreadyAssigned_ThrowInvalidNameException()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");

			var dojoAction = new Action(() => dojoAssistant.AddParticipant("John Doe"));

			dojoAction.Should().Throw<InvalidNameException>();
		}

		[Fact]
		public void RemoveParticipants_IfDojoStateIsActive_ThrowInvalidOperationException()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			dojoAssistant.AddParticipant("Jane Doe");
			dojoAssistant.StartRound();

			var dojoAction = new Action(() => dojoAssistant.RemoveParticipants());

			dojoAction.Should().Throw<InvalidOperationException>();
		}

		[Fact]
		public void RemoveParticipants_IfDojoStateIsIdle_ParticipantsShouldBeRemoved()
		{
			var dojoAssistant = new DojoAssistant(60);
			dojoAssistant.AddParticipant("John Doe");
			dojoAssistant.AddParticipant("Jane Doe");
			
			dojoAssistant.RemoveParticipants();

			dojoAssistant.Participants.Should().BeEmpty();
		}
	}
}