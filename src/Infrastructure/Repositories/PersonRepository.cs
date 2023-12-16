using SensorFlow.Domain.Entities.Persons;
using SensorFlow.Domain.Abstractions.Exceptions;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Infrastructure.DbContexts;

/* Concrete implementation of the IPersonRepository */

namespace SensorFlow.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly SensorFlowDbContext _context;

        public PersonRepository(SensorFlowDbContext context)
        {
            _context = context;
        }

        public async Task<Person> AddPerson(CancellationToken cancellationToken, Person toCreate)
        {
            _context.Persons.Add(toCreate);

            await _context.SaveChangesAsync(cancellationToken);

            return toCreate;
        }

        public async Task DeletePerson(CancellationToken cancellationToken, Person toDelete)
        {
            _context.Persons.Remove(toDelete);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ICollection<Person>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Persons.OrderBy(p => p.Name).ToListAsync(cancellationToken);
        }

        public async Task<Person> GetPersonById(CancellationToken cancellationToken, Guid personId)
        {
            var person = await _context.Persons
                .Include(p => p.Id)
                .FirstOrDefaultAsync(p => p.Id == personId, cancellationToken);

            if (person is null) throw new NotFoundException();

            return person;
        }

        public async Task<Person> UpdatePerson(CancellationToken cancellationToken, Guid personId, string name, string email, string phone, DateTime lastModified)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == personId);
            person.Name = name;
            person.Email = email;
            person.Phone = phone;
            person.LastModified = lastModified;

            await _context.SaveChangesAsync(cancellationToken);

            return person;
        }
    }
}