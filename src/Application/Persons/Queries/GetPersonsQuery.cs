using AutoMapper;
using MediatR;
using SensorFlow.Domain.Abstractions.Repositories;
using SensorFlow.Application.Persons.Models;

namespace SensorFlow.Application.Persons.Queries
{
    // Query
    public record GetPersonsQuery() : IRequest<PersonDto>;

    // Query Handler
    public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, PersonDto>
    {

        private readonly IPersonRepository _personRepository;
        protected readonly IMapper _mapper;

        public GetPersonsQueryHandler(IMapper mapper, IPersonRepository repository)
        {
            _personRepository = repository;
            _mapper = mapper;
        }

        public async Task<PersonDto> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
        {
            var persons = await _personRepository.GetAll();

            // to-do Guard against not found

            return _mapper.Map<PersonDto>(persons);
        }
    }
}