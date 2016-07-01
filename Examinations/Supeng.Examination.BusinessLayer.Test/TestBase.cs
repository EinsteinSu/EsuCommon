using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer.Test
{
    [TestClass]
    public class TestBase
    {
        protected readonly ExaDataContext Context = new ExaDataContext();

        [TestInitialize]
        public void Initialize()
        {
        }

        protected void GenerateQuestions(int count)
        {
            for (var i = 0; i < count; i++)
            {
                Context.Questions.Add(new Question {Content = "Question: " + (i + 1), Type = QuestionType.多选, Score = 2});
            }
            Context.SaveChanges();
        }

        [TestCleanup]
        public void Clean()
        {
            Context.Database.ExecuteSqlCommand("Delete from ExaminationQuestions");
            Context.Database.ExecuteSqlCommand("Delete from UserAnswers");
            Context.Database.ExecuteSqlCommand("Delete from UserExaminations");
            Context.Database.ExecuteSqlCommand("Delete from examinations");
            Context.Database.ExecuteSqlCommand("Delete from answers");
            Context.Database.ExecuteSqlCommand("Delete from questions");
            Context.Database.ExecuteSqlCommand("Delete from Userprofiles");
        }
    }
}