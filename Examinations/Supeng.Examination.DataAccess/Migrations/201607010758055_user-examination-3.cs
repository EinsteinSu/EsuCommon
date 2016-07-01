namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userexamination3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserExaminations", "EndTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserExaminations", "EndTime", c => c.DateTime(nullable: false));
        }
    }
}
