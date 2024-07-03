namespace OnlineBookReviews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Description : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookDetails", "Description", c => c.String());
            AlterStoredProcedure(
                "dbo.BookDetails_Insert",
                p => new
                    {
                        ISBN = p.String(maxLength: 128),
                        BookName = p.String(),
                        Author = p.String(),
                        AddedByUserID = p.Int(),
                        AddedOn = p.DateTime(),
                        Description = p.String(),
                        AverageRating = p.Decimal(precision: 18, scale: 2),
                    },
                body:
                    @"INSERT [dbo].[BookDetails]([ISBN], [BookName], [Author], [AddedByUserID], [AddedOn], [Description], [AverageRating])
                      VALUES (@ISBN, @BookName, @Author, @AddedByUserID, @AddedOn, @Description, @AverageRating)"
            );
            
            AlterStoredProcedure(
                "dbo.BookDetails_Update",
                p => new
                    {
                        ISBN = p.String(maxLength: 128),
                        BookName = p.String(),
                        Author = p.String(),
                        AddedByUserID = p.Int(),
                        AddedOn = p.DateTime(),
                        Description = p.String(),
                        AverageRating = p.Decimal(precision: 18, scale: 2),
                    },
                body:
                    @"UPDATE [dbo].[BookDetails]
                      SET [BookName] = @BookName, [Author] = @Author, [AddedByUserID] = @AddedByUserID, [AddedOn] = @AddedOn, [Description] = @Description, [AverageRating] = @AverageRating
                      WHERE ([ISBN] = @ISBN)"
            );
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookDetails", "Description");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
