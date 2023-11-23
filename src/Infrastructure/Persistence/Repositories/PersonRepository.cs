using SensorFlow.Domain.Entities.Persons;
using SensorFlow.Domain.Abstractions.Exceptions;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Domain.Abstractions.Repositories;

namespace SensorFlow.Infrastructure.Persistence.Repositories;

/* Concrete implementation of the IPersonRepository */

public class PersonRepository : IPersonRepository
{
    private readonly SensorFlowDbContext _context;

    public PersonRepository(SensorFlowDbContext context)
    {
        _context = context;
    }

    public async Task<Person> AddPerson(Person toCreate)
    {
        _context.Persons.Add(toCreate);

        await _context.SaveChangesAsync();

        return toCreate;
    }

    public async Task DeletePerson(Person toDelete)
    {
        _context.Persons.Remove(toDelete);

        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Person>> GetAll()
    {
        return await _context.Persons.OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<Person> GetPersonById(Guid personId)
    {
        var person = await _context.Persons
            .Include(p => p.Id)
            .FirstOrDefaultAsync(p => p.Id == personId);

        if (person is null) throw new NotFoundException();

        return person;
    }

    public async Task<Person> UpdatePerson(Guid personId, string name, string email, string phone, DateTime lastModified)
    {
        var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == personId);
        person.Name = name;
        person.Email = email;
        person.Phone = phone;
        person.LastModified = lastModified;
        
        await _context.SaveChangesAsync();

        return person;
    }
}