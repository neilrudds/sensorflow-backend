using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Persons.Models;

namespace SensorFlow.Application.Persons.Queries
{
    // Query
    public record GetPersonsQuery() : IRequest<PersonDTO>;

    // Query Handler
    public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, PersonDTO>
    {

        private readonly IPersonRepository _personRepository;
        protected readonly IMapper _mapper;

        public GetPersonsQueryHandler(IMapper mapper, IPersonRepository repository)
        {
            _personRepository = repository;
            _mapper = mapper;
        }

        public async Task<PersonDTO> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
        {
            var persons = await _personRepository.GetAll(cancellationToken);

            // to-do Guard against not found

            return _mapper.Map<PersonDTO>(persons);
        }
    }
}