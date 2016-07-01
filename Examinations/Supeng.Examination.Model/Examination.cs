using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Supeng.Examination.Model
{
    public class Examination
    {
        public Examination()
        {
            QuestionCount = 20;
        }

        [Key]
        public int ExaminationId { get; set; }

        [MaxLength(20), Required]
        [Display(Name = "试卷名称")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "日期")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "试题数量")]
        public int QuestionCount { get; set; }


        [Display(Name = "备注")]
        public string Description { get; set; }

        public IList<Question> GenerateQuestions(IList<Question> questions)
        {
            if (questions.Count <= QuestionCount)
            {
                return new List<Question>();
            }
            Random random = new Random();
            IList<Question> result = new List<Question>(QuestionCount);
            while (result.Count < QuestionCount)
            {
                int value = random.Next(0, questions.Count - 1);
                var item = questions[value];
                if (result.All(a => a.QuestionId != item.QuestionId))
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}