using SensorFlow.Domain.Entities.Persons;

namespace SensorFlow.Domain.Abstractions.Repositories;

/* This interface will be used by the API as an abstraction of the repository whose implementation resides in the Infrastructure project. */

public interface IPersonRepository
{
    Task<ICollection<Person>> GetAll();

    Task<Person> GetPersonById(Guid personId);

    Task<Person> AddPerson(Person toCreate);

    Task<Person> UpdatePerson(Guid personId, string name, string email, string phone, DateTime lastModified);

    Task DeletePerson(Person toDelete);
}