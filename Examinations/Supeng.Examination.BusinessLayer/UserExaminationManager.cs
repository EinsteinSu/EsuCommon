using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer
{
    public interface IUserExaminationManager : IDisposable
    {
        bool Exists(int userId, int examinationId);

        bool CanReExamination(int userId, int examinationId);

        string Create(int userId, int examinationId);

        string AnswerQuestion(UserExamination examination, int questionId, string answer);

        string ExaminationDone(UserExamination examination);
    }

    public class UserExaminationManager : IUserExaminationManager
    {
        private readonly ExaDataContext _context = new ExaDataContext();
        public bool Exists(int userId, int examinationId)
        {
            return
                _context.UserExaminations.Any(
                    a => a.User.UserId == userId && a.Examination.ExaminationId == examinationId);
        }

        public bool CanReExamination(int userId, int examinationId)
        {
            return _context.UserExaminations.Any(
                  a => a.User.UserId == userId && a.Examination.ExaminationId == examinationId && a.Status == 0);
        }

        public string Create(int userId, int examinationId)
        {
            try
            {
                //todo: change it to a transaction
                var ue = new UserExamination
                {
                    UserId = userId,
                    ExaminationId = examinationId,
                    UserAnswers = new List<UserAnswer>()
                };
                foreach (var id in _context.ExaminationQuestion.Where(w => w.Examination.ExaminationId == examinationId)
                    .Select(s => s.Question.QuestionId))
                {
                    ue.UserAnswers.Add(new UserAnswer
                    {
                        QuestionId = id
                    });
                }
                _context.UserExaminations.Add(ue);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string AnswerQuestion(UserExamination examination, int questionId, string answer)
        {
            try
            {
                _context.Database.ExecuteSqlCommand("UpdateAnswer @p0, @p1, @p2",
                    examination.UserExaminationId, questionId, answer);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ExaminationDone(UserExamination examination)
        {
            try
            {
                examination.Status = ExaminationStatus.Done;
                _context.Entry(examination).State = EntityState.Modified;
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
