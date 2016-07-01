using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Supeng.Examination.Model
{
    public class UserProfile
    {
        [Key]
        public int UserId { get; set; }


        [Display(Name = "用户代码"), MaxLength(6)]
        public string UserCode { get; set; }

        [MaxLength(20)]
        [Display(Name = "用户名")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public void EncryptPassword()
        {
            Password = Encrypt(Password);
        }

        public bool ComparePassword(string password)
        {
            return Password == Encrypt(password);
        }

        protected virtual string Encrypt(string input)
        {
            MD5 md5 = new MD5Cng();
            byte[] buffer = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            foreach (var b in buffer)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}