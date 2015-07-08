using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Newtonsoft.Json.Linq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;
using ZAProxy.Components;
using ZAProxy.Infrastructure;
using ZAProxy.Schema;
using ZAProxy.Tests.TestUtils;

namespace ZAProxy.Tests.Components
{
    [Trait("Component", "PassiveScanner")]
    public class PassiveScannerComponentTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]PassiveScannerComponent sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("pscan");
        }

        #region Views

        [Theory, AutoTestData]
        public void GetRecordsToScan(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]PassiveScannerComponent sut,
            int recordsToScan)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("recordsToScan", recordsToScan));
            httpClientMock.SetupApiCall(sut, CallType.View, "recordsToScan")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetRecordsToScan();

            // ASSERT
            result.Should().Be(recordsToScan);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetScanners(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]PassiveScannerComponent sut,
            IEnumerable<PassiveScanner> scanners)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("scanners", JArray.FromObject(scanners)));
            httpClientMock.SetupApiCall(sut, CallType.View, "scanners")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetScanners();

            // ASSERT
            result.ShouldBeEquivalentTo(scanners);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        [Theory, AutoTestData]
        public void DisableAllScanners(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]PassiveScannerComponent sut)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "disableAllScanners")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.DisableAllScanners();

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void DisableScanners(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]PassiveScannerComponent sut,
            int[] ids)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "disableScanners",
                new Parameters
                {
                    { "ids", string.Join(",", ids) }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.DisableScanners(ids);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void EnableAllScanners(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]PassiveScannerComponent sut)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "enableAllScanners")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.EnableAllScanners();

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void EnableScanners(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]PassiveScannerComponent sut,
            int[] ids)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "enableScanners",
                new Parameters
                {
                    { "ids", string.Join(",", ids) }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.EnableScanners(ids);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetEnabled(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]PassiveScannerComponent sut,
            bool enabled)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setEnabled",
                new Parameters
                {
                    { "enabled", enabled }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetEnabled(enabled);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetScannerAlertThreshold(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]PassiveScannerComponent sut,
            int id,
            AlertThreshold alertThreshold)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "setScannerAlertThreshold",
                new Parameters
                {
                    { "id", id },
                    { "alertThreshold", alertThreshold }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetScannerAlertThreshold(id, alertThreshold);

            // ASSERT
            httpClientMock.Verify();
        }

        #endregion
    }
}
