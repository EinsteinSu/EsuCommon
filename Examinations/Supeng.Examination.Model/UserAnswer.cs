using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supeng.Examination.Model
{
    public class UserAnswer
    {
        [Key]
        public int UserAnswerId { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public string Answer { get; set; }

        public int Score
        {
            get
            {
                if (!string.IsNullOrEmpty(Answer))
                {
                    return Answer == Question.CorrectAnswer ? Question.Score : 0;
                }
                return 0;
            }
        }

    }
}