using SensorFlow.Domain.Entities.Persons;

/* This interface will be used by the API as an abstraction of the repository whose implementation resides in the Infrastructure project. */
namespace SensorFlow.Application.Common.Interfaces
{
    public interface IPersonRepository
    {
        Task<ICollection<Person>> GetAll(CancellationToken cancellationToken);

        Task<Person> GetPersonById(CancellationToken cancellationToken, Guid personId);

        Task<Person> AddPerson(CancellationToken cancellationToken, Person toCreate);

        Task<Person> UpdatePerson(CancellationToken cancellationToken, Guid personId, string name, string email, string phone, DateTime lastModified);

        Task DeletePerson(CancellationToken cancellationToken, Person toDelete);
    }
}