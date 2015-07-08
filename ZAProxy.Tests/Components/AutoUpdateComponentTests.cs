using FluentAssertions;
using Moq;
using Newtonsoft.Json.Linq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;
using ZAProxy.Components;
using ZAProxy.Infrastructure;
using ZAProxy.Tests.TestUtils;

namespace ZAProxy.Tests.Components
{
    [Trait("Component", "AutoUpdate")]
    public class AutoUpdateComponentTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]AutoUpdateComponent sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("autoupdate");
        }

        #region Views

        [Theory, AutoTestData]
        public void IsLatestVersion(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool isLatestVersion)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("isLatestVersion", isLatestVersion));
            httpClientMock.SetupApiCall(sut, CallType.View, "isLatestVersion")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.IsLatestVersion();

            // ASSERT
            result.Should().Be(isLatestVersion);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetLatestVersionNumber(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            string latestVersionNumber)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("latestVersionNumber", latestVersionNumber));
            httpClientMock.SetupApiCall(sut, CallType.View, "latestVersionNumber")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetLatestVersionNumber();

            // ASSERT
            result.Should().Be(latestVersionNumber);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionCheckAddonUpdates(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool value)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("CheckAddonUpdates", value));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionCheckAddonUpdates")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionCheckAddonUpdates();

            // ASSERT
            result.Should().Be(value);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionCheckOnStart(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool value)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("CheckOnStart", value));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionCheckOnStart")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionCheckOnStart();

            // ASSERT
            result.Should().Be(value);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionDownloadNewRelease(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool value)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("DownloadNewRelease", value));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionDownloadNewRelease")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionDownloadNewRelease();

            // ASSERT
            result.Should().Be(value);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionInstallAddonUpdates(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool value)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("InstallAddonUpdates", value));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionInstallAddonUpdates")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionInstallAddonUpdates();

            // ASSERT
            result.Should().Be(value);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionInstallScannerRules(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool value)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("InstallScannerRules", value));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionInstallScannerRules")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionInstallScannerRules();

            // ASSERT
            result.Should().Be(value);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionReportAlphaAddons(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool value)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ReportAlphaAddons", value));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionReportAlphaAddons")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionReportAlphaAddons();

            // ASSERT
            result.Should().Be(value);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionReportBetaAddons(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool value)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ReportBetaAddons", value));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionReportBetaAddons")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionReportBetaAddons();

            // ASSERT
            result.Should().Be(value);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetOptionReportReleaseAddons(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool value)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("ReportReleaseAddons", value));
            httpClientMock.SetupApiCall(sut, CallType.View, "optionReportReleaseAddons")
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetOptionReportReleaseAddons();

            // ASSERT
            result.Should().Be(value);
            httpClientMock.Verify();
        }

        #endregion

        #region Actions

        [Theory, AutoTestData]
        public void DownloadLatestRelease(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "downloadLatestRelease")
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.DownloadLatestRelease();

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionCheckAddonUpdates(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "optionCheckAddonUpdates",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionCheckAddonUpdates(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionCheckOnStart(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "optionCheckOnStart",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionCheckOnStart(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionDownloadNewRelease(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "optionDownloadNewRelease",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionDownloadNewRelease(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionInstallAddonUpdates(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "optionInstallAddonUpdates",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionInstallAddonUpdates(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionInstallScannerRules(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "optionInstallScannerRules",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionInstallScannerRules(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionReportAlphaAddons(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "optionReportAlphaAddons",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionReportAlphaAddons(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionReportBetaAddons(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "optionReportBetaAddons",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionReportBetaAddons(value);

            // ASSERT
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void SetOptionReportReleaseAddons(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]AutoUpdateComponent sut,
            bool value)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, "optionReportReleaseAddons",
                new Parameters
                {
                    { "Boolean", value }
                })
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.SetOptionReportReleaseAddons(value);

            // ASSERT
            httpClientMock.Verify();
        }

        #endregion
    }
}
