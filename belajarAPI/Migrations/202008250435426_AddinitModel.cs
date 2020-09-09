namespace belajarAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddinitModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tb_M_Departement",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Tb-M_Division",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        Department_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Tb_M_Departement", t => t.Department_Id, cascadeDelete: true)
                .Index(t => t.Department_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tb-M_Division", "Department_Id", "dbo.Tb_M_Departement");
            DropIndex("dbo.Tb-M_Division", new[] { "Department_Id" });
            DropTable("dbo.Tb-M_Division");
            DropTable("dbo.Tb_M_Departement");
        }
    }
}
