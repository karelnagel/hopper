using AutoFixture;
using Hopper.Models;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Tests
{
    public abstract class ControllerTestBase : ContextOptionsTestBase
    {
        protected readonly Fixture Fixture = new Fixture().OmitRecursion();
        protected readonly Mock<UserManager<ApplicationUser>> MockUserManager = GetMockUserManager<ApplicationUser>();

        private static Mock<UserManager<TUser>> GetMockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
            return mgr;
        }
    }
}