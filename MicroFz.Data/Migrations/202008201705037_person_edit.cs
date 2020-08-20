namespace MicroFz.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class person_edit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "FirstName", c => c.String());
            AddColumn("dbo.People", "LastName", c => c.String());
            AddColumn("dbo.People", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.People", "NationalCode", c => c.String());
            DropColumn("dbo.People", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Name", c => c.String());
            DropColumn("dbo.People", "NationalCode");
            DropColumn("dbo.People", "Age");
            DropColumn("dbo.People", "LastName");
            DropColumn("dbo.People", "FirstName");
        }
    }
}
