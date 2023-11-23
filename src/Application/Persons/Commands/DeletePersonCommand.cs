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
    // Command
    public record DeletePersonCommand(Guid personId) : IRequest;

    // Command Handler
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
    {
        private readonly IPersonRepository _personRepository;

        public DeletePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPersonById(request.personId);

            // todo. Guard against not found
            await _personRepository.DeletePerson(person);
        }
    }
}