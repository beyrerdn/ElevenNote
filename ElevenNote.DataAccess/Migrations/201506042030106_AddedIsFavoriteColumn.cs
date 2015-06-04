namespace ElevenNote.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsFavoriteColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Note", "IsFavorite", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Note", "IsFavorite");
        }
    }
}
