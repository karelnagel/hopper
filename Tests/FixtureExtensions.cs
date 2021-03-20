using System.Linq;
using AutoFixture;

namespace Tests
{
    public static class FixtureExtensions
    {
        public static Fixture OmitRecursion(this Fixture fixture)
        {
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }
    }
}