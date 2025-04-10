using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Commons.Identity;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Infrastructure.UnitTest.Repositories;

public class IdentityRepositoryTests
{
    private readonly UserManager<User> _userManager;
    private readonly IUserStore<User> _userStore;
    private readonly SignInManager<User> _signInManager;
    private readonly IdentityRepository _sut;
    public IdentityRepositoryTests()
    {
        _userStore = Substitute.For<IUserStore<User>>();
        _userManager = Substitute.For<UserManager<User>>
            (_userStore,null,null,null,null,null,null,null,null);
        var contextAccessor = Substitute.For<IHttpContextAccessor>();
        var claimFactory = Substitute.For<IUserClaimsPrincipalFactory<User>>();
        
        _signInManager = Substitute.For<SignInManager<User>>
            (_userManager,contextAccessor, claimFactory,null,null,null,null);

        _sut = new IdentityRepository(_userManager, _signInManager);
    }
    
    [Theory]
    [InlineData("test@gmail",Roles.Employer, "AValidPassword1@")]
    [InlineData("test1@gmail",Roles.JobSeeker, "AValidPassword2@")]
    public async void RegisterUser_ShouldReturnSuccess_If_UserAndPasswordAreValid(string email, Roles role, string password)
    {

        // ARRANGE
        var user = new User(email, role);
         _userManager.CreateAsync(user, password)
            .Returns(IdentityResult.Success);
        // ACT
        var result = await _sut.RegisterUser(user, password);
        // ASSERT
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
        Assert.IsType<User>(result.Data);
    }
    [Theory]
    [InlineData("test@gmail",Roles.Employer, "asimplepassword")]
    [InlineData("test@gmail",Roles.JobSeeker, "asimplepassword")]

    public async void RegisterUser_ShouldFailWithMessage_If_PasswordIsInvalid(string email, Roles role, string password)
    {
        // ARRANGE
        var user = new User(email, role);
        _userManager.CreateAsync(user, password)
            .Returns(IdentityResult.Failed());
        // ACT
        var result = await _sut.RegisterUser(user, password);
        // ASSERT
        Assert.False(result.IsSuccess);
        Assert.IsType<List<string>>(result.Errors);
    }

    [Theory]
    [InlineData("test@gmail", Roles.Employer, "asimplepassword")]
    [InlineData("test@gmail", Roles.JobSeeker, "asimplepassword")]
    public async void LoginUser_ShouldFail_If_UserEmailDoesNotExist(string email, Roles role, string password)
    {

        // ARRANGE
        var user = new User(email, role);
        _userManager.FindByEmailAsync(email).ReturnsNull();
        // ACT
        var result = await _sut.LoginUser(user, password);
        
        // ASSERT
        
        Assert.False(result.IsSuccess);
        Assert.Single(result.Errors);
        Assert.Equal("user or password is wrong.", result.Errors.FirstOrDefault());
    }
    [Theory]
    [InlineData("test@gmail", Roles.Employer, "asimplepassword")]
    [InlineData("test@gmail", Roles.JobSeeker, "asimplepassword")]
    public async void LoginUser_ShouldFail_If_UserEmailExistButPasswordIsNotCorrect(string email, Roles role, string password)
    {

        // ARRANGE
        var user = new User(email, role);
        _userManager.FindByEmailAsync(email).Returns(user);
        _signInManager.CheckPasswordSignInAsync(user, password, false).Returns(SignInResult.Failed);
        // ACT
        var result = await _sut.LoginUser(user, password);
        
        // ASSERT
        
        Assert.False(result.IsSuccess);
        Assert.Single(result.Errors);
        Assert.Equal("user or password is wrong.", result.Errors.FirstOrDefault());
    }
    [Theory]
    [InlineData("test@gmail", Roles.Employer, "asimplepassword")]
    [InlineData("test1@gmail", Roles.JobSeeker, "asimplepassword")]
    public async void LoginUser_ShouldSuccess_If_UserEmailExistAndPasswordIsCorrect(string email, Roles role, string password)
    {

        // ARRANGE
        var user = new User(email, role);
        _userManager.FindByEmailAsync(email).Returns(user);
        _signInManager.CheckPasswordSignInAsync(user, password,false).Returns(SignInResult.Success);
        // ACT
        var result = await _sut.LoginUser(user, password);
        
        // ASSERT
        
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);

    }
    
}