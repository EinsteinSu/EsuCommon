using System.ComponentModel.DataAnnotations;

namespace Supeng.Examination.Model
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }

        [MaxLength(2), Required]
        [Display(Name = "编号")]
        public string OrderNumber { get; set; }

        [Display(Name = "内容"), Required]
        public string Content { get; set; }
    }
}