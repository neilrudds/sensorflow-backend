using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Persons.Models;

namespace SensorFlow.Application.Persons.Queries
{
    // Query
    public record GetPersonQuery(Guid personId) : IRequest<PersonDTO>;

    // Query Handler
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonDTO>
    {

        private readonly IPersonRepository _personRepository;
        protected readonly IMapper _mapper;

        public GetPersonQueryHandler(IMapper mapper, IPersonRepository repository)
        {
            _personRepository = repository;
            _mapper = mapper;
        }

        public async Task<PersonDTO> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPersonById(cancellationToken, request.personId);

            // to-do Guard against not found

            return _mapper.Map<PersonDTO>(person);
        }
    }
}