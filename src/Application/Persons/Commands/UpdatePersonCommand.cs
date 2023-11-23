using MediatR;
using SensorFlow.Domain.Abstractions.Repositories;
using SensorFlow.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                request.personId,
                request.name,
                request.email,
                request.phone,
                DateTime.UtcNow);
        }
    }
}