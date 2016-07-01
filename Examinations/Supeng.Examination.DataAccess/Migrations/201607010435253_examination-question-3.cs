namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class examinationquestion3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExaminationQuestions", "Question_QuestionId", "dbo.Questions");
            DropIndex("dbo.ExaminationQuestions", new[] { "Question_QuestionId" });
            AddColumn("dbo.Examinations", "QuestionCount", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "ExaminationQuestion_ExaminationQuestionId", c => c.Int());
            CreateIndex("dbo.Questions", "ExaminationQuestion_ExaminationQuestionId");
            AddForeignKey("dbo.Questions", "ExaminationQuestion_ExaminationQuestionId", "dbo.ExaminationQuestions", "ExaminationQuestionId");
            DropColumn("dbo.ExaminationQuestions", "Question_QuestionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExaminationQuestions", "Question_QuestionId", c => c.Int());
            DropForeignKey("dbo.Questions", "ExaminationQuestion_ExaminationQuestionId", "dbo.ExaminationQuestions");
            DropIndex("dbo.Questions", new[] { "ExaminationQuestion_ExaminationQuestionId" });
            DropColumn("dbo.Questions", "ExaminationQuestion_ExaminationQuestionId");
            DropColumn("dbo.Examinations", "QuestionCount");
            CreateIndex("dbo.ExaminationQuestions", "Question_QuestionId");
            AddForeignKey("dbo.ExaminationQuestions", "Question_QuestionId", "dbo.Questions", "QuestionId");
        }
    }
}
