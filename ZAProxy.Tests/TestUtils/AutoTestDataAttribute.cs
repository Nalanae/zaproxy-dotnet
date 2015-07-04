using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;
using ZAProxy.Tests.TestUtils.FixtureSpecimenBuilders;

namespace ZAProxy.Tests.TestUtils
{
    public class AutoTestDataAttribute : AutoDataAttribute
    {
        public AutoTestDataAttribute()
            : base(new Fixture()
                  .Customize(new AutoMoqCustomization())
                  .Customize(new HarCustomization()))
        { }
    }
}
