using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Supeng.Examination.Model.Test
{
    [TestClass]
    public class ExaminationTest
    {
        [TestMethod]
        public void GenerateQuestion()
        {
            Examination ex = new Examination();
            ex.QuestionCount = 5;

            var list = new List<Question>();
            for (int i = 0; i < 6; i++)
            {
                list.Add(new Question { QuestionId = i + 1, Content = "Question:" + (i + 1) });
            }

            var result = ex.GenerateQuestions(list);

            Assert.IsTrue(result.Count == 5);
            Console.WriteLine(String.Join(",", result.Select(s => s.QuestionId).ToList()));
        }
    }
}
