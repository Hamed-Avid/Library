using FluentMigrator;

namespace Library.Migrations
{
    [Migration(202106301743)]
    public class _202106301743_SchemaInitialized : Migration
    {
        public override void Up()
        {
            Create.Table("Categories")
                             .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                             .WithColumn("Title").AsString(100).Unique().NotNullable();

            Create.Table("Books")
                             .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                             .WithColumn("Title").AsString(100).Unique().NotNullable()
                             .WithColumn("Writer").AsString(100).NotNullable()
                             .WithColumn("MinAge").AsInt16().NotNullable()
                             .WithColumn("MaxAge").AsInt16().Nullable()
                             .WithColumn("CategoryId").AsInt32().NotNullable()
                             .ForeignKey("FK_Books_Categories", "Categories", "Id").NotNullable();

            Create.Table("People")
                             .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                             .WithColumn("FirstName").AsString(100).Unique().NotNullable()
                             .WithColumn("LastName").AsString(100).NotNullable()
                             .WithColumn("BirthDate").AsDateTime().NotNullable()
                             .WithColumn("Address").AsString().NotNullable();

            Create.Table("Trusteeship")
                             .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                             .WithColumn("ReturnDate").AsDateTime2().NotNullable()
                             .WithColumn("DeliveryDate").AsDateTime2().Nullable()
                             .WithColumn("PersonId").AsInt32().NotNullable()
                             .ForeignKey("FK_Trusteeship_People", "People", "Id").NotNullable()
                             .WithColumn("BookId").AsInt32().NotNullable()
                             .ForeignKey("FK_Trusteeship_Books", "Books", "Id").NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Trusteeship");
            Delete.Table("People");
            Delete.Table("Books");
            Delete.Table("Categories");
        }
    }
}
