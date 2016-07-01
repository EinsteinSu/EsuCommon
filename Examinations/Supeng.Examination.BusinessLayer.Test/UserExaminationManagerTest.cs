using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer.Test
{
    [TestClass]
    public class UserExaminationManagerTest : TestBase
    {
        UserExaminationManager manager = new UserExaminationManager();
        [TestMethod]
        public void Create()
        {
            CreateUserExamination();
        }

        [TestMethod]
        public void Answer()
        {
            const string answer = "hello";
            CreateUserExamination();


            var ue = Context.UserExaminations.FirstOrDefault();
            Assert.IsNotNull(ue);
            var ua = ue.UserAnswers.FirstOrDefault();
            Assert.IsNotNull(ua);
            var questionId = ua.QuestionId;
            var question = Context.Questions.FirstOrDefault(f => f.QuestionId == questionId);
            Assert.IsNotNull(question);
            var error = manager.AnswerQuestion(ue, questionId, answer);
            Assert.IsTrue(string.IsNullOrEmpty(error));

            //use another context that exclude cache affection
            var context = new ExaDataContext();
            ue = context.UserExaminations.Include(i => i.UserAnswers).FirstOrDefault();
            Assert.IsNotNull(ue);
            var userAnswer = ue.UserAnswers.FirstOrDefault(f => f.QuestionId == questionId);
            Assert.IsNotNull(userAnswer);
            Assert.AreEqual(userAnswer.Answer, answer);
        }

        protected void CreateUserExamination()
        {
            var user = AddUser();
            var ex = AddExaminationAndGenerate();
            var error = manager.Create(user.UserId, ex.ExaminationId);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            var list = Context.UserAnswers.ToList();
            Assert.IsTrue(list.Any());
        }

        protected UserProfile AddUser()
        {
            var manager = new UserProfileManager();
            var profile = new UserProfile
            {
                Name = "test",
                UserCode = "123456",
                Password = "123"
            };
            var error = manager.Create(profile);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            return profile;
        }

        protected Model.Examination AddExaminationAndGenerate()
        {
            var manager = new ExaminationManager();
            var ex = new Model.Examination
            {
                Name = "test",
                QuestionCount = 20,
                Date = DateTime.Now,
                Description = "description"
            };
            var error = manager.Create(ex);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            GenerateQuestions(30);
            error = manager.GenerateExamination(ex);
            return ex;
        }
    }
}
