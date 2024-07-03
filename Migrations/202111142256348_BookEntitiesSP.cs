namespace OnlineBookReviews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookEntitiesSP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookDetails",
                c => new
                    {
                        ISBN = c.String(nullable: false, maxLength: 128),
                        BookName = c.String(),
                        Author = c.String(),
                        AddedByUserID = c.Int(nullable: false),
                        AddedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ISBN);
            
            CreateTable(
                "dbo.BookRatings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ISBN = c.String(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        Surname = c.String(),
                        EmailAddress = c.String(),
                        IDNumber = c.String(),
                        ContactNumber = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateStoredProcedure(
                "dbo.BookDetails_Insert",
                p => new
                    {
                        ISBN = p.String(maxLength: 128),
                        BookName = p.String(),
                        Author = p.String(),
                        AddedByUserID = p.Int(),
                        AddedOn = p.DateTime(),
                    },
                body:
                    @"INSERT [dbo].[BookDetails]([ISBN], [BookName], [Author], [AddedByUserID], [AddedOn])
                      VALUES (@ISBN, @BookName, @Author, @AddedByUserID, @AddedOn)"
            );
            
            CreateStoredProcedure(
                "dbo.BookDetails_Update",
                p => new
                    {
                        ISBN = p.String(maxLength: 128),
                        BookName = p.String(),
                        Author = p.String(),
                        AddedByUserID = p.Int(),
                        AddedOn = p.DateTime(),
                    },
                body:
                    @"UPDATE [dbo].[BookDetails]
                      SET [BookName] = @BookName, [Author] = @Author, [AddedByUserID] = @AddedByUserID, [AddedOn] = @AddedOn
                      WHERE ([ISBN] = @ISBN)"
            );
            
            CreateStoredProcedure(
                "dbo.BookDetails_Delete",
                p => new
                    {
                        ISBN = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[BookDetails]
                      WHERE ([ISBN] = @ISBN)"
            );
            
            CreateStoredProcedure(
                "dbo.BookRatings_Insert",
                p => new
                    {
                        UserID = p.Int(),
                        ISBN = p.String(),
                        Rating = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[BookRatings]([UserID], [ISBN], [Rating])
                      VALUES (@UserID, @ISBN, @Rating)
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[BookRatings]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID]
                      FROM [dbo].[BookRatings] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
                "dbo.BookRatings_Update",
                p => new
                    {
                        ID = p.Int(),
                        UserID = p.Int(),
                        ISBN = p.String(),
                        Rating = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[BookRatings]
                      SET [UserID] = @UserID, [ISBN] = @ISBN, [Rating] = @Rating
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.BookRatings_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[BookRatings]
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.UserDetails_Insert",
                p => new
                    {
                        FirstName = p.String(),
                        Surname = p.String(),
                        EmailAddress = p.String(),
                        IDNumber = p.String(),
                        ContactNumber = p.Int(),
                        CreatedOn = p.DateTime(),
                        Password = p.String(),
                    },
                body:
                    @"INSERT [dbo].[UserDetails]([FirstName], [Surname], [EmailAddress], [IDNumber], [ContactNumber], [CreatedOn], [Password])
                      VALUES (@FirstName, @Surname, @EmailAddress, @IDNumber, @ContactNumber, @CreatedOn, @Password)
                      
                      DECLARE @UserID int
                      SELECT @UserID = [UserID]
                      FROM [dbo].[UserDetails]
                      WHERE @@ROWCOUNT > 0 AND [UserID] = scope_identity()
                      
                      SELECT t0.[UserID]
                      FROM [dbo].[UserDetails] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[UserID] = @UserID"
            );
            
            CreateStoredProcedure(
                "dbo.UserDetails_Update",
                p => new
                    {
                        UserID = p.Int(),
                        FirstName = p.String(),
                        Surname = p.String(),
                        EmailAddress = p.String(),
                        IDNumber = p.String(),
                        ContactNumber = p.Int(),
                        CreatedOn = p.DateTime(),
                        Password = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[UserDetails]
                      SET [FirstName] = @FirstName, [Surname] = @Surname, [EmailAddress] = @EmailAddress, [IDNumber] = @IDNumber, [ContactNumber] = @ContactNumber, [CreatedOn] = @CreatedOn, [Password] = @Password
                      WHERE ([UserID] = @UserID)"
            );
            
            CreateStoredProcedure(
                "dbo.UserDetails_Delete",
                p => new
                    {
                        UserID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[UserDetails]
                      WHERE ([UserID] = @UserID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.UserDetails_Delete");
            DropStoredProcedure("dbo.UserDetails_Update");
            DropStoredProcedure("dbo.UserDetails_Insert");
            DropStoredProcedure("dbo.BookRatings_Delete");
            DropStoredProcedure("dbo.BookRatings_Update");
            DropStoredProcedure("dbo.BookRatings_Insert");
            DropStoredProcedure("dbo.BookDetails_Delete");
            DropStoredProcedure("dbo.BookDetails_Update");
            DropStoredProcedure("dbo.BookDetails_Insert");
            DropTable("dbo.UserDetails");
            DropTable("dbo.BookRatings");
            DropTable("dbo.BookDetails");
        }
    }
}
