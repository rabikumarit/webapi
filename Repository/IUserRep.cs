using WebAPI.Domain.Module;

namespace WebAPI.Repository
{
    public interface IUserRep
    {
        IEnumerable<User> GetAll();
        void insert(User obj);
        void update(User obj);
        void delete(int id);
        User Get(long id);
    }
}
