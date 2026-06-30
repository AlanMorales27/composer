using System.Security.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Server.DTOs;

namespace Server.Tests;

public class AuthServiceTest
{
    // Password is stored hashed and not in plain text
    [Fact]
    public async Task RegisterAsync_SamePassword_StoredDifferentHashedPasswords()
    {
        int callCount = 0;
        var hasherMock = new Mock<IPasswordHasher<Restaurant>>();
        hasherMock
            .Setup(h => h.HashPassword(It.IsAny<Restaurant>(), It.IsAny<string>()))
            .Returns(() => $"HASHED_PASSWORD_{++callCount}");

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        AppDbContext context = new AppDbContext(options);

        AuthService service = new AuthService(context, hasherMock.Object);
        var sharedPassword = "SamePassword123!";

        var dto1 = new RegisterDto
        {
            Name = "Restaurant One",
            Email = "one@example.com",
            Password = sharedPassword,
            Phone = "111111111"
        };

        var dto2 = new RegisterDto
        {
            Name = "Restaurant Two",
            Email = "two@example.com",
            Password = sharedPassword,
            Phone = "222222222"
        };

        await service.RegisterAsync(dto1);
        await service.RegisterAsync(dto2);

        var restaurants = await context.Restaurants.OrderBy(r => r.Name).ToListAsync();

        Assert.Equal(2, restaurants.Count);
        Assert.NotEqual(restaurants[0].Password, restaurants[1].Password);
        Assert.NotEqual(sharedPassword, restaurants[0].Password);
        Assert.NotEqual(sharedPassword, restaurants[1].Password);
    }

    [Fact]
    public async Task RegisterAsync_StoredHashedPassword_NotPlainText()
    {
        var hasherMock = new Mock<IPasswordHasher<Restaurant>>();
        hasherMock
            .Setup(h => h.HashPassword(It.IsAny<Restaurant>(), It.IsAny<string>()))
            .Returns("HASHED_PASSWORD");

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        AppDbContext context = new AppDbContext(options);

        var dto = new RegisterDto
        {
            Name = "Test Restaurant",
            Email = "test@example.com",
            Password = "MyPlainTextPassword123!",
            Phone = "123456789"
        };

        AuthService service = new AuthService(context, hasherMock.Object);

        await service.RegisterAsync(dto);
        
        var savedRestaurant = await context.Restaurants.SingleOrDefaultAsync(); 

        //Test
        Assert.NotNull(savedRestaurant);
        Assert.Equal("HASHED_PASSWORD", savedRestaurant.Password);
        Assert.NotEqual(dto.Password, savedRestaurant.Password);     
    }

    [Fact]
    public async Task LoginAsync_IncorrectPassword_ThrowsAuthenticationException()
    {
        var hasherMock = new Mock<IPasswordHasher<Restaurant>>();
        hasherMock
            .Setup(h => h.HashPassword(It.IsAny<Restaurant>(), It.IsAny<string>()))
            .Returns("HASHED_PASSWORD");
        hasherMock
            .Setup(h => h.VerifyHashedPassword(It.IsAny<Restaurant>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(PasswordVerificationResult.Failed);

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        AppDbContext context = new AppDbContext(options);

        var registerDto = new RegisterDto
        {
            Name = "Test Restaurant",
            Email = "test@example.com",
            Password = "CorrectPassword123!",
            Phone = "123456789"
        };

        AuthService service = new AuthService(context, hasherMock.Object);

        await service.RegisterAsync(registerDto);

        var loginDto = new LoginTerminalDto
        {
            Email = "test@example.com",
            Password = "WrongPassword456!"
        };

        var ex = await Assert.ThrowsAsync<AuthenticationException>(() => service.LoginTerminalAsync(loginDto));
        Assert.Equal("Invalid credentials", ex.Message);
    }

}
