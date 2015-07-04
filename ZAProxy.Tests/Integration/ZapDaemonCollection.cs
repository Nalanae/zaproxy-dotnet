using Xunit;

namespace ZAProxy.Tests.Integration
{
    [CollectionDefinition("ZapDaemon")]
    public class ZapDaemonCollection : ICollectionFixture<ZapDaemonFixture>
    {
    }
}
