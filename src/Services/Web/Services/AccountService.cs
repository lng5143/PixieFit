using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PixieFit.Web.Business;
using PixieFit.Web.Business.Models;
using PixieFit.Web.Business.Entities;
using Isopoh.Cryptography.Argon2;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using PixieFit.Web.Business.Enums;
using IdentityModel.Client;
using System.Text.Json;

namespace PixieFit.Web.Services;

public interface IAccountService
{
    Task CreateAccount(SignUpRequest request);
}

public class AccountService 
{
    private readonly PFContext _dbContext;

    public AccountService(PFContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAccount(SignUpRequest request)
    {
        try
        {
            // validation 
            if (string.IsNullOrEmpty(request.Email))
                throw new Exception(ErrorCode.EMAIL_REQUIRED.ToString());

            if (string.IsNullOrEmpty(request.Password))
                throw new Exception(ErrorCode.PASSWORD_REQUIRED.ToString());

            if (string.IsNullOrEmpty(request.Username))
                throw new Exception(ErrorCode.USERNAME_REQUIRED.ToString());

            // hashing and salting password 
            var salt = RandomNumberGenerator.GetBytes(16).ToB64String();
            var passwordHash = Argon2.Hash(request.Password, salt);

            var user = new User
            {
                Email = request.Email,
                Username = request.Username,
                PasswordHash = passwordHash,
                Salt = salt,
                CreditAmount = 0
            };

            await _dbContext.Users.AddAsync(user);
            // await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<string> Login(LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Email))
            throw new Exception(ErrorCode.EMAIL_REQUIRED.ToString());
        
        if (string.IsNullOrEmpty(request.Password))
            throw new Exception(ErrorCode.PASSWORD_REQUIRED.ToString());

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        
        if (user == null)
            throw new Exception("User not found");

        var hashToVerify = Argon2.Hash(request.Password, user.Salt);
        if (Argon2.Verify(hashToVerify, user.PasswordHash))
        {
            // TODO: create JWT token
            return await IssueUserToken(user);
        }
        else
        {
            throw new Exception("Invalid password");
        }
    }

    private async Task<string> IssueUserToken(User user)
    {
        var client = new HttpClient();
        var tokenResponse = await client.RequestPasswordTokenAsync(
            new PasswordTokenRequest
            {
                UserName = user.Username,
                Password = user.PasswordHash,
                Scope = "resize"
            }
        );

        if (tokenResponse.IsError)
        {
            throw new Exception(tokenResponse.Error);
        }

        var result = JsonSerializer.Serialize(tokenResponse);

        return result;
    }
}