using FundHub.Data.Data.Models;
using FundHub.Data.Services.PasswordHash;
using Microsoft.EntityFrameworkCore;

namespace FundHub.Data.Services.Seed;

public static class Seed
{
    public static void SeedDatabase(ModelBuilder modelBuilder, IPasswordHash passwordHash)
    {
        modelBuilder.Entity<News>().HasData(
            new News { Id = Guid.Parse("0f97ea1d-e247-4cf5-a6d9-5f9d3265e220"), Title = "Innovative Breakthroughs: College Students Secure Funding for Groundbreaking Projects", Subtitle = "", Description = "Desc Test", Published = new DateOnly(2024,1,1), Imagecovername = "newscover.jpg"},
            new News { Id = Guid.Parse("1a55b12e-65b8-4542-b4c1-6676c30311e7"), Title = "Empowering Tomorrow's Leaders: College Projects Receive Major Funding Boost", Subtitle = "", Description = "Desc Test", Published = new DateOnly(2024,1,1), Imagecovername = "newscover.jpg" },
            new News { Id = Guid.Parse("93097c20-6558-4ed9-a27e-8bf07fb59b8a"), Title = "From Campus to Capital: Student-Led Ventures Garner Investment for Impactful Initiatives", Subtitle = "", Description = "Desc Test", Published = new DateOnly(2024,1,1), Imagecovername = "newscover.jpg" },
            new News { Id = Guid.Parse("598004de-bc37-4300-8271-3c1c0bb5c430"), Title = "Shaping the Future: College Students' Ambitious Projects Win Substantial Funding", Subtitle = "", Description = "Desc Test", Published = new DateOnly(2024,1,1), Imagecovername = "newscover.jpg" }
        );
        
        modelBuilder.Entity<Category>().HasData(
            new Category {Id = Guid.Parse("4a858ba2-cc64-4752-973a-2b1acba5d78d") , Name = "product"},
            new Category {Id = Guid.Parse("fafaad46-3fbe-40ac-ad63-c311829668a4") , Name = "society"},
            new Category {Id = Guid.Parse("59cb7c8b-8e33-45d6-b066-214f3145a3c0") , Name = "environment"}
        );
        
        modelBuilder.Entity<Project>().HasData(
            new Project { Id = Guid.Parse("7e4788cd-77a9-4b03-9412-385a482cf489"), Title = "Greener Egypt", Subtitle = "Subtitle Test", Description = "Description Test", CategoryId = Guid.Parse("59cb7c8b-8e33-45d6-b066-214f3145a3c0"), UserId = Guid.Parse("c0c343f3-a9d0-4ae6-93e4-0d1923b04e60"), Currentfund = 500, Totalfundrequired = 2000,Imagesnames = new string[] {"1.jpg", "2.jpg" }, Facebook = "", X = "", Instagram = ""},
            new Project { Id = Guid.Parse("694d6683-d3e6-4bc1-ab5d-f2f67f887332"), Title = "My Super Awesome Health Machine Research Paper", Subtitle = "Subtitle Test", Description = "Description Test", CategoryId = Guid.Parse("fafaad46-3fbe-40ac-ad63-c311829668a4"), Currentfund = 500, Totalfundrequired = 1000000, UserId = Guid.Parse("913eedbd-a304-478e-beee-4c8db66bd86a"), Imagesnames = new string[] {"1.jpg", "2.jpg" }, Facebook = "", X = "", Instagram = "" },
            new Project { Id = Guid.Parse("a9437a37-1d37-4a9b-adbd-a18ef0490942"), Title = "Electric Koshary Machine", Subtitle = "Subtitle Test", Description = "Description Test", CategoryId = Guid.Parse("4a858ba2-cc64-4752-973a-2b1acba5d78d"), Currentfund = 500, Totalfundrequired = 120000, UserId = Guid.Parse("2e445054-8f22-4812-adb7-38cd849c976b"), Imagesnames = new string[] {"1.jpg", "2.jpg" }, Facebook = "", X = "", Instagram = "" },
            new Project { Id = Guid.Parse("e9c8eccf-76aa-42d6-be67-803d8622c951"), Title = "Indie Egyptian Game", Subtitle = "Subtitle Test", Description = "Description Test", CategoryId = Guid.Parse("4a858ba2-cc64-4752-973a-2b1acba5d78d"), UserId = Guid.Parse("a5379337-e6a4-4222-aa88-233358bda6e9"), Currentfund = 500, Totalfundrequired = 60000, Imagesnames = new string[] {"1.jpg", "2.jpg" }, Facebook = "", X = "", Instagram = ""}
        );

        modelBuilder.Entity<User>().HasData(
            new User { Id = Guid.Parse("c0c343f3-a9d0-4ae6-93e4-0d1923b04e60"), Username = "testuser1", Hashedpassword = passwordHash.CreateHashedPassword("1234"), Usertype = "user", Phonenumber = 123456789, Email = "test@gmail.com", Facebook = "", Instagram = "", X = "",Profileimage = "profile.jpg"},
            new User { Id = Guid.Parse("913eedbd-a304-478e-beee-4c8db66bd86a"), Username = "testuser2", Hashedpassword = passwordHash.CreateHashedPassword("1234"), Usertype = "user", Phonenumber = 123456789, Email = "test@gmail.com", Facebook = "", Instagram = "", X = "",Profileimage = "profile.jpg"},
            new User { Id = Guid.Parse("2e445054-8f22-4812-adb7-38cd849c976b"), Username = "testuser3", Hashedpassword = passwordHash.CreateHashedPassword("1234"), Usertype = "user", Phonenumber = 123456789, Email = "test@gmail.com", Facebook = "", Instagram = "", X = "",Profileimage = "profile.jpg"},
            new User { Id = Guid.Parse("a5379337-e6a4-4222-aa88-233358bda6e9"), Username = "testuser4", Hashedpassword = passwordHash.CreateHashedPassword("1234"), Usertype = "user", Phonenumber = 123456789, Email = "test@gmail.com", Facebook = "", Instagram = "", X = "",Profileimage = "profile.jpg"},
            new User { Id = Guid.Parse("9bdfe044-4b02-40a7-ade7-4570e68af19c"), Username = "testuser5", Hashedpassword = passwordHash.CreateHashedPassword("1234"), Usertype = "user", Phonenumber = 123456789, Email = "test@gmail.com", Facebook = "", Instagram = "", X = "",Profileimage = "profile.jpg"},
            new User { Id = Guid.Parse("c8b590f1-c920-4c1b-9237-852bc0b43518"), Username = "testadmin", Hashedpassword = passwordHash.CreateHashedPassword("1234"), Usertype = "admin", Phonenumber = 123456789, Email = "test@gmail.com", Facebook = "", Instagram = "", X = "",Profileimage = "profile.jpg"}
            );
    }
}