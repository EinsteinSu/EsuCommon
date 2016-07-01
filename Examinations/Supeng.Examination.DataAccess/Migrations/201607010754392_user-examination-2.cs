namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userexamination2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserAnswers", "Question_QuestionId", "dbo.Questions");
            DropIndex("dbo.UserAnswers", new[] { "Question_QuestionId" });
            RenameColumn(table: "dbo.UserExaminations", name: "Examination_ExaminationId", newName: "ExaminationId");
            RenameColumn(table: "dbo.UserExaminations", name: "User_UserId", newName: "UserId");
            RenameColumn(table: "dbo.UserAnswers", name: "Question_QuestionId", newName: "QuestionId");
            RenameIndex(table: "dbo.UserExaminations", name: "IX_User_UserId", newName: "IX_UserId");
            RenameIndex(table: "dbo.UserExaminations", name: "IX_Examination_ExaminationId", newName: "IX_ExaminationId");
            AlterColumn("dbo.UserAnswers", "QuestionId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserAnswers", "QuestionId");
            AddForeignKey("dbo.UserAnswers", "QuestionId", "dbo.Questions", "QuestionId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAnswers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.UserAnswers", new[] { "QuestionId" });
            AlterColumn("dbo.UserAnswers", "QuestionId", c => c.Int());
            RenameIndex(table: "dbo.UserExaminations", name: "IX_ExaminationId", newName: "IX_Examination_ExaminationId");
            RenameIndex(table: "dbo.UserExaminations", name: "IX_UserId", newName: "IX_User_UserId");
            RenameColumn(table: "dbo.UserAnswers", name: "QuestionId", newName: "Question_QuestionId");
            RenameColumn(table: "dbo.UserExaminations", name: "UserId", newName: "User_UserId");
            RenameColumn(table: "dbo.UserExaminations", name: "ExaminationId", newName: "Examination_ExaminationId");
            CreateIndex("dbo.UserAnswers", "Question_QuestionId");
            AddForeignKey("dbo.UserAnswers", "Question_QuestionId", "dbo.Questions", "QuestionId");
        }
    }
}
