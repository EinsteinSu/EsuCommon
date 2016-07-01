using System.Data.Entity;
using Supeng.Examination.Model;

namespace Supeng.Examination.DataAccess
{
    public class ExaDataContext : DbContext
    {
        public ExaDataContext()
            : base("name=ExaDataContext")
        {

        }

        public IDbSet<Question> Questions { get; set; }

        public IDbSet<Model.Examination> Examinations { get; set; }

        public IDbSet<UserProfile> UserProfiles { get; set; }

        public IDbSet<ExaminationQuestion> ExaminationQuestion { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public IDbSet<UserExamination> UserExaminations { get; set; }

        public IDbSet<UserAnswer> UserAnswers { get; set; }
    }
}