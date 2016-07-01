using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Supeng.Examination.Model
{
    public class Question
    {
        //public Question()
        //{
        //    Answers = new List<Answer>();
        //}

        [Key]
        public int QuestionId { get; set; }

        [Display(Name = "类型"), Required]
        public QuestionType Type { get; set; }

        [Display(Name = "内容"), Required]
        public string Content { get; set; }

        [Display(Name = "分数"), Required]
        public int Score { get; set; }

        [MaxLength(100)]
        [Display(Name = "正确答案")]
        public string CorrectAnswer { get; set; }
        public virtual IList<Answer> Answers { get; set; }
    }

    public enum QuestionType
    {
        单选, 多选
    }
}