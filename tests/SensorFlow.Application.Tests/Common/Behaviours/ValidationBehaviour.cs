using MediatR;
using FluentValidation.Results;
using SensorFlow.Application.Dashboards.Commands;

namespace SensorFlow.Application.Tests.Common.Behaviours
{
    public class ValidationBehaviourTests
    {
        private readonly IMediator _mediator;
        public ValidationBehaviourTests(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Fact]
        public async Task InvokeValidationBehaviour_WhenValidationResultIsValid_ShouldInvokeNext()
        {
            // Create Dashboard
            var result = await _mediator.Send(new CreateDashboardCommand("SensorFlow Dashboard", Guid.NewGuid().ToString()));

            Assert.False(result.IsError);
        }
    }
}
