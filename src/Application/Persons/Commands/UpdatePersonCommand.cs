using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Entities.Persons;

namespace SensorFlow.Application.Persons.Commands
{
    public record UpdatePersonCommand(Guid personId, string name, string email, string phone) : IRequest<Person>;

    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Person>
    {

        private readonly IPersonRepository _personRepository;

        public UpdatePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            return await _personRepository.UpdatePerson(
                cancellationToken,
                request.personId,
                request.name,
                request.email,
                request.phone,
                DateTime.UtcNow);
        }
    }
}