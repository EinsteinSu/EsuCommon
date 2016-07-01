using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer.Test
{
    [TestClass]
    public class QuestionManagerTest : TestBase
    {
        private readonly QuestionManager _manager = new QuestionManager();

        [TestMethod]
        public void SelectTest()
        {
            var list = _manager.SelectList();
            Assert.IsTrue(list.Count == 0);
        }

        [TestMethod]
        public void Add()
        {
            var id = AddQuestion();

            Assert.IsTrue(id > 0);
        }

        [TestMethod]
        public void Modify()
        {
            var id = AddQuestion();
            var question = _manager.SelectById(id);
            Assert.IsNotNull(question);
            question.Answers[0].Content = "Test";
            var str = _manager.Modify(question);
            Assert.IsTrue(string.IsNullOrEmpty(str));
        }

        [TestMethod]
        public void Delete()
        {
            var id = AddQuestion();
            _manager.Delete(id);
            Assert.IsTrue(!Context.Questions.Any(a => a.QuestionId == id));
        }

        private int AddQuestion()
        {
            var question = new Question
            {
                Content = "Test content",
                Type = QuestionType.单选,
                CorrectAnswer = "A",
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        OrderNumber = "A",
                        Content = "A is a correct answer."
                    },
                    new Answer
                    {
                        OrderNumber = "B",
                        Content = "B is a correct answer."
                    }
                }
            };
            _manager.Create(question);
            return question.QuestionId;
        }
    }
}