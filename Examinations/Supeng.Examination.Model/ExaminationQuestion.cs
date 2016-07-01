using System.ComponentModel.DataAnnotations;

namespace Supeng.Examination.Model
{
    public class ExaminationQuestion
    {
        [Key]
        public int ExaminationQuestionId { get; set; }

        public virtual Examination Examination { get; set; }

        public virtual Question Question { get; set; }
    }
}