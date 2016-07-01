namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userprofile1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfiles", "UserCode", c => c.String(maxLength: 6));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfiles", "UserCode", c => c.String());
        }
    }
}
