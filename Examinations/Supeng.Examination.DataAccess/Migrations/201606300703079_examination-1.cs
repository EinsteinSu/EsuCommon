namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class examination1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Examinations",
                c => new
                    {
                        ExaminationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                        Examination_ExaminationId = c.Int(),
                    })
                .PrimaryKey(t => t.ExaminationId)
                .ForeignKey("dbo.Examinations", t => t.Examination_ExaminationId)
                .Index(t => t.Examination_ExaminationId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        Score = c.Int(nullable: false),
                        CorrectAnswer = c.String(maxLength: 100),
                        Examination_ExaminationId = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Examinations", t => t.Examination_ExaminationId)
                .Index(t => t.Examination_ExaminationId);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        OrderNumber = c.String(nullable: false, maxLength: 2),
                        Content = c.String(nullable: false),
                        Question_QuestionId = c.Int(),
                    })
                .PrimaryKey(t => t.AnswerId)
                .ForeignKey("dbo.Questions", t => t.Question_QuestionId)
                .Index(t => t.Question_QuestionId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserCode = c.String(),
                        Name = c.String(maxLength: 20),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "Examination_ExaminationId", "dbo.Examinations");
            DropForeignKey("dbo.Answers", "Question_QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Examinations", "Examination_ExaminationId", "dbo.Examinations");
            DropIndex("dbo.Answers", new[] { "Question_QuestionId" });
            DropIndex("dbo.Questions", new[] { "Examination_ExaminationId" });
            DropIndex("dbo.Examinations", new[] { "Examination_ExaminationId" });
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Answers");
            DropTable("dbo.Questions");
            DropTable("dbo.Examinations");
        }
    }
}
