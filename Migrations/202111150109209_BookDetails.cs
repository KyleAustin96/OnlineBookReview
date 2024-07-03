namespace OnlineBookReviews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookDetails", "AverageRating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterStoredProcedure(
                "dbo.BookDetails_Insert",
                p => new
                    {
                        ISBN = p.String(maxLength: 128),
                        BookName = p.String(),
                        Author = p.String(),
                        AddedByUserID = p.Int(),
                        AddedOn = p.DateTime(),
                        AverageRating = p.Decimal(precision: 18, scale: 2),
                    },
                body:
                    @"INSERT [dbo].[BookDetails]([ISBN], [BookName], [Author], [AddedByUserID], [AddedOn], [AverageRating])
                      VALUES (@ISBN, @BookName, @Author, @AddedByUserID, @AddedOn, @AverageRating)"
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
                        AverageRating = p.Decimal(precision: 18, scale: 2),
                    },
                body:
                    @"UPDATE [dbo].[BookDetails]
                      SET [BookName] = @BookName, [Author] = @Author, [AddedByUserID] = @AddedByUserID, [AddedOn] = @AddedOn, [AverageRating] = @AverageRating
                      WHERE ([ISBN] = @ISBN)"
            );
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookDetails", "AverageRating");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
