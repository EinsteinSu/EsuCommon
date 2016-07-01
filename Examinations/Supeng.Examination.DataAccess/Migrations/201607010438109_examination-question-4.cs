namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class examinationquestion4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "ExaminationQuestion_ExaminationQuestionId", "dbo.ExaminationQuestions");
            DropIndex("dbo.Questions", new[] { "ExaminationQuestion_ExaminationQuestionId" });
            AddColumn("dbo.ExaminationQuestions", "Question_QuestionId", c => c.Int());
            CreateIndex("dbo.ExaminationQuestions", "Question_QuestionId");
            AddForeignKey("dbo.ExaminationQuestions", "Question_QuestionId", "dbo.Questions", "QuestionId");
            DropColumn("dbo.Questions", "ExaminationQuestion_ExaminationQuestionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "ExaminationQuestion_ExaminationQuestionId", c => c.Int());
            DropForeignKey("dbo.ExaminationQuestions", "Question_QuestionId", "dbo.Questions");
            DropIndex("dbo.ExaminationQuestions", new[] { "Question_QuestionId" });
            DropColumn("dbo.ExaminationQuestions", "Question_QuestionId");
            CreateIndex("dbo.Questions", "ExaminationQuestion_ExaminationQuestionId");
            AddForeignKey("dbo.Questions", "ExaminationQuestion_ExaminationQuestionId", "dbo.ExaminationQuestions", "ExaminationQuestionId");
        }
    }
}
