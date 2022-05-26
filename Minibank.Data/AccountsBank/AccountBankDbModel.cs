using System;
using Minibank.Core.Domains;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minibank.Data.Users;
using System.Collections.Generic;
using Minibank.Data.AccountsBank;
namespace Minibank.Data.AccountsBank
{
    [Table("user_accounts")]
    public class AccountBankDbModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public virtual UserDbModel User { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }
        public bool AccountPriv { get; set; }
        public DateTime OpenAccount { get; set; }
        public DateTime CloseAccount { get; set; }
        internal class Map:IEntityTypeConfiguration<AccountBankDbModel>
        {
            public void Configure(EntityTypeBuilder<AccountBankDbModel> builder)
            {
                builder.ToTable("user_accounts");
                builder.Property(it => it.Id).HasColumnName("id");
                builder.HasKey(it => it.Id);

                //builder.Property(it => it.UserId).HasColumnName("user_id");
                //builder.Property(it => it.Balance).HasColumnName("balance");
                //builder.Property(it => it.Currency).HasColumnName("currency");
                //builder.Property(it => it.AccountPriv).HasColumnName("account_priv");
            }
        }

    }
}
