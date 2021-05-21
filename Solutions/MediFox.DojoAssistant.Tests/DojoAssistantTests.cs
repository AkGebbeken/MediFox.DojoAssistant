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
	}
}