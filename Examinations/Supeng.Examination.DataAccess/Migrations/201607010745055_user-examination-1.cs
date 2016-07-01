namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userexamination1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserExaminations",
                c => new
                    {
                        UserExaminationId = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Examination_ExaminationId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserExaminationId)
                .ForeignKey("dbo.Examinations", t => t.Examination_ExaminationId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfiles", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Examination_ExaminationId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.UserAnswers",
                c => new
                    {
                        UserAnswerId = c.Int(nullable: false, identity: true),
                        Answer = c.String(),
                        Question_QuestionId = c.Int(),
                        UserExamination_UserExaminationId = c.Int(),
                    })
                .PrimaryKey(t => t.UserAnswerId)
                .ForeignKey("dbo.Questions", t => t.Question_QuestionId)
                .ForeignKey("dbo.UserExaminations", t => t.UserExamination_UserExaminationId)
                .Index(t => t.Question_QuestionId)
                .Index(t => t.UserExamination_UserExaminationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAnswers", "UserExamination_UserExaminationId", "dbo.UserExaminations");
            DropForeignKey("dbo.UserAnswers", "Question_QuestionId", "dbo.Questions");
            DropForeignKey("dbo.UserExaminations", "User_UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.UserExaminations", "Examination_ExaminationId", "dbo.Examinations");
            DropIndex("dbo.UserAnswers", new[] { "UserExamination_UserExaminationId" });
            DropIndex("dbo.UserAnswers", new[] { "Question_QuestionId" });
            DropIndex("dbo.UserExaminations", new[] { "User_UserId" });
            DropIndex("dbo.UserExaminations", new[] { "Examination_ExaminationId" });
            DropTable("dbo.UserAnswers");
            DropTable("dbo.UserExaminations");
        }
    }
}
