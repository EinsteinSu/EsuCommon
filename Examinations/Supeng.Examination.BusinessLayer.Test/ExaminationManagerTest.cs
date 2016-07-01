using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer.Test
{
    [TestClass]
    public class ExaminationManagerTest : TestBase
    {
        private readonly ExaminationManager _manager = new ExaminationManager();

        [TestMethod]
        public void Select()
        {
            Add();
            var list = _manager.SelectList();
            Assert.IsTrue(list.Any());
        }

        [TestMethod]
        public void Create()
        {
            var ex = Add();
            Assert.IsTrue(ex.ExaminationId > 0);
        }

        [TestMethod]
        public void Modify()
        {
            const string modify = "Test1";
            var ex = Add();
            ex.Name = modify;
            _manager.Modify(ex);
            ex = Context.Examinations.FirstOrDefault(f => f.ExaminationId == ex.ExaminationId);
            Assert.IsNotNull(ex);
            Assert.AreEqual(modify, ex.Name);
        }

        [TestMethod]
        public void Delete()
        {
            var ex = Add();
            _manager.Delete(ex.ExaminationId);
            Assert.IsNull(Context.Examinations.FirstOrDefault(f => f.ExaminationId == ex.ExaminationId));
        }

        [TestMethod]
        public void Generate()
        {
            var ex = Add();
            GenerateQuestions(30);
            var error = _manager.GenerateExamination(ex);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            Assert.IsTrue(Context.ExaminationQuestion.Any(a => a.Examination.ExaminationId == ex.ExaminationId));
        }

        /// <summary>
        ///     expect get error when question less than examination wanted
        /// </summary>
        [TestMethod]
        public void GenerateWithError()
        {
            var ex = Add();
            var error = _manager.GenerateExamination(ex, new List<Question>());
            Assert.IsTrue(!string.IsNullOrEmpty(error));
        }

        protected Model.Examination Add()
        {
            var ex = new Model.Examination
            {
                Name = "test",
                QuestionCount = 20,
                Date = DateTime.Now,
                Description = "description"
            };
            var error = _manager.Create(ex);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            return ex;
        }
    }
}