using BeautyBe.Core.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeautyBe.DataAccess.Configurations.Auth
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserId).UseIdentityColumn();

            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.PasswordCrypt).IsRequired();

            builder.ToTable("Users");
        }
    }
}
