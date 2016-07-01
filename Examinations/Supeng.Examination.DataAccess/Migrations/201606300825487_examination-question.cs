namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class examinationquestion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Examinations", "Examination_ExaminationId", "dbo.Examinations");
            DropForeignKey("dbo.Questions", "Examination_ExaminationId", "dbo.Examinations");
            DropIndex("dbo.Examinations", new[] { "Examination_ExaminationId" });
            DropIndex("dbo.Questions", new[] { "Examination_ExaminationId" });
            DropColumn("dbo.Examinations", "Examination_ExaminationId");
            DropColumn("dbo.Questions", "Examination_ExaminationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Examination_ExaminationId", c => c.Int());
            AddColumn("dbo.Examinations", "Examination_ExaminationId", c => c.Int());
            CreateIndex("dbo.Questions", "Examination_ExaminationId");
            CreateIndex("dbo.Examinations", "Examination_ExaminationId");
            AddForeignKey("dbo.Questions", "Examination_ExaminationId", "dbo.Examinations", "ExaminationId");
            AddForeignKey("dbo.Examinations", "Examination_ExaminationId", "dbo.Examinations", "ExaminationId");
        }
    }
}
