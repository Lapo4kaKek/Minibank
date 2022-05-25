
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minibank.Core.Domains.Users;

namespace Minibank.Data.Users
{
    [Table("user")]
    public class UserDbModel
    {
        public string Id { get; set; }
        [Column("login")]
        public string Login { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("is_active")]
        public bool isActive { get; set; }

        internal class Map : IEntityTypeConfiguration<UserDbModel>
        {
            public void Configure(EntityTypeBuilder<UserDbModel> builder)
            {
                builder.Property(it => it.Id)
                    .HasColumnName("id");
                builder.HasKey(it => it.Id).HasName("pk_id");
            }
        }
    }
}
