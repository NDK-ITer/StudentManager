using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFacultyRepository:IGenericRepository<Faculty>
    {
        void Restore(Faculty faculty);
    }
}
