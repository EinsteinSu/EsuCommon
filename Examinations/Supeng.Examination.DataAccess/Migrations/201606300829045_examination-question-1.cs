namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class examinationquestion1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExaminationQuestions",
                c => new
                    {
                        ExaminationQuestionId = c.Int(nullable: false, identity: true),
                        Examination_ExaminationId = c.Int(),
                        Question_QuestionId = c.Int(),
                    })
                .PrimaryKey(t => t.ExaminationQuestionId)
                .ForeignKey("dbo.Examinations", t => t.Examination_ExaminationId)
                .ForeignKey("dbo.Questions", t => t.Question_QuestionId)
                .Index(t => t.Examination_ExaminationId)
                .Index(t => t.Question_QuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExaminationQuestions", "Question_QuestionId", "dbo.Questions");
            DropForeignKey("dbo.ExaminationQuestions", "Examination_ExaminationId", "dbo.Examinations");
            DropIndex("dbo.ExaminationQuestions", new[] { "Question_QuestionId" });
            DropIndex("dbo.ExaminationQuestions", new[] { "Examination_ExaminationId" });
            DropTable("dbo.ExaminationQuestions");
        }
    }
}
