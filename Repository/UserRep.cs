using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Collections.Generic;
using WebAPI.Domain.Database;
using WebAPI.Domain.Module;
namespace WebAPI.Repository
{
    public class UserRep : IUserRep
    {
        private readonly APIdbcontext _aPIdbcontext;
        DbSet<User> _tblusers;
        public UserRep(APIdbcontext aPIdbcontext)
        {
            _aPIdbcontext = aPIdbcontext;
            _tblusers= _aPIdbcontext.Set<User>();
        }
        void IUserRep.delete(int id)
        {
            var user = _tblusers.Find(id);
            _tblusers.Remove(user);
            _aPIdbcontext.SaveChanges();
        }
        User IUserRep.Get(long _id)
        {
            return _tblusers.SingleOrDefault(s=>s.Id==_id);
        }
        IEnumerable<User> IUserRep.GetAll()
        {
            return _tblusers.ToList();

        }
        void IUserRep.insert(User obj)
        {
            _aPIdbcontext.Add(obj);
            _aPIdbcontext.SaveChanges();
        }
        void IUserRep.update(User updatedUser)
        {
            var existingUser = _tblusers.Find(updatedUser.Id);
           
            // Update properties
            existingUser.Name = updatedUser.Name;
            existingUser.Mobile = updatedUser.Mobile;
            existingUser.Age = updatedUser.Age;

            _aPIdbcontext.SaveChanges();

        }
    }
}
