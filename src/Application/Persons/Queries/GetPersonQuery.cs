using AutoMapper;
using MediatR;
using SensorFlow.Domain.Abstractions.Repositories;
using SensorFlow.Domain.Entities.Persons;
using SensorFlow.Application.Persons.Models;

namespace SensorFlow.Application.Persons.Queries
{
    // Query
    public record GetPersonQuery(Guid personId) : IRequest<PersonDto>;

    // Query Handler
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonDto>
    {

        private readonly IPersonRepository _personRepository;
        protected readonly IMapper _mapper;

        public GetPersonQueryHandler(IMapper mapper, IPersonRepository repository)
        {
            _personRepository = repository;
            _mapper = mapper;
        }

        public async Task<PersonDto> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPersonById(request.personId);

            // to-do Guard against not found

            return _mapper.Map<PersonDto>(person);
        }
    }
}