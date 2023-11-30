using MediatR;
using SensorFlow.Domain.Entities.Persons;
using SensorFlow.Application.Common.Interfaces;

namespace SensorFlow.Application.Persons.Commands
{
    // Command
    public record CreatePersonCommand(string name, string email, string phone) : IRequest<Guid>;

    // Command Handler
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Guid>
    {
        private readonly IPersonRepository _personRepository;

        public CreatePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Guid> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = Person.CreatePerson(
                Guid.NewGuid(),
                request.name,
                request.email,
                request.phone,
                DateTime.UtcNow,
                DateTime.UtcNow
            );

            await _personRepository.AddPerson(cancellationToken, person);
            return person.Id;
        }
    }
}