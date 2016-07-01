using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Supeng.Examination.Model
{
    public class UserExamination
    {
        public UserExamination()
        {
            StartTime = DateTime.Now;
        }

        [Key]
        public int UserExaminationId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual UserProfile User { get; set; }

        [ForeignKey("Examination")]
        public int ExaminationId { get; set; }

        public virtual Examination Examination { get; set; }

        [Required]
        public ExaminationStatus Status { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public IList<UserAnswer> UserAnswers { get; set; }

        public decimal Percent
        {
            get
            {
                //todo: display percent
                return 0;
            }
        }

        public int Score
        {
            get { return UserAnswers.Sum(s => s.Score); }
        }
    }

    public enum ExaminationStatus
    {
        Start, Done
    }
}