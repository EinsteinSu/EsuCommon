using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Supeng.Examination.BusinessLayer.Interfaces;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer
{
    public interface IUserProfileManager : ICrud<UserProfile>, IDisposable
    {
        UserProfile Logon(string userName, string password);

        string ChangePassword(string userName, string password, string newPassword);
    }

    public class UserProfileManager : IUserProfileManager
    {
        private readonly ExaDataContext _context = new ExaDataContext();

        public IList<UserProfile> SelectList()
        {
            return _context.UserProfiles.ToList();
        }

        public UserProfile SelectById(int id)
        {
            return _context.UserProfiles.FirstOrDefault(f => f.UserId == id);
        }

        public string Create(UserProfile data)
        {
            try
            {
                data.EncryptPassword();
                _context.UserProfiles.Add(data);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Modify(UserProfile profile)
        {
            try
            {
                var data = SelectById(profile.UserId);
                if (data == null)
                {
                    throw new KeyNotFoundException("Not found");
                }
                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Delete(int id)
        {
            try
            {
                var data = SelectById(id);
                if (data == null)
                {
                    throw new KeyNotFoundException("Not found");
                }
                _context.UserProfiles.Remove(data);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public UserProfile Logon(string userName, string password)
        {
            var list = _context.UserProfiles.Where(f => f.Name.Equals(userName)).ToList();
            return list.FirstOrDefault(f => f.ComparePassword(password));
        }

        public string ChangePassword(string userName, string password, string newPassword)
        {
            try
            {
                var user = Logon(userName, password);
                if (user == null)
                {
                    return "Logon Fialed.";
                }
                user.Password = newPassword;
                user.EncryptPassword();
                _context.Entry(user).Property(p => p.Password).IsModified = true;
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}