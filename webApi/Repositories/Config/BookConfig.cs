using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using webApi.Models;

namespace webApi.Repositories.Config;

public class BookConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasData(
            new Book { Id = 1, Title = "The Hitchhiker's Guide to the Galaxy", Price = 42 },
            new Book { Id = 2, Title = "The Restaurant at the End of the Universe", Price = 45 },
            new Book { Id = 3, Title = "Life, the Universe and Everything", Price = 67 },
            new Book { Id = 4, Title = "So Long, and Thanks for All the Fish", Price = 34 },
            new Book { Id = 5, Title = "Mostly Harmless", Price = 28 }
        );
    }
}