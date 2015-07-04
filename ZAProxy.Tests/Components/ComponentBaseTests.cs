using System;
using FluentAssertions;
using Moq;
using Newtonsoft.Json.Linq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;
using ZAProxy.Components;
using ZAProxy.Infrastructure;

namespace ZAProxy.Tests.Components
{
    public class ComponentBaseTests
    {
        #region Stub

        public class ComponentBaseStub : ComponentBase
        {
            public ComponentBaseStub(IHttpClient httpClient, IZapProcess zapProcess)
            : base(httpClient, zapProcess, "ascan")
            { }

            public T TestView<T>(string method, string takeValueFromProperty = null)
            {
                return CallView<T>(method, takeValueFromProperty);
            }

            public void TestAction(string method)
            {
                CallAction(method);
            }
        }

        #endregion

        #region View

        [Theory, AutoTestData]
        public void CallView_NoResult(
            [Frozen]Mock<IHttpClient> httpClientMock,
            ComponentBaseStub sut,
            string method)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.View, method)
                .ReturnsNull()
                .Verifiable();

            // ACT
            Action act = () => sut.TestView<object>(method);

            // ASSERT
            act.ShouldThrow<ZapException>().WithMessage(Resources.ResultFromServerWasEmpty);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void CallView_UnknownResult(
            [Frozen]Mock<IHttpClient> httpClientMock,
            ComponentBaseStub sut,
            string method)
        {
            // ARRANGE
            var json = new JObject(
                    new JProperty("AlternativeProperty", "Value"));
            httpClientMock.SetupApiCall(sut, CallType.View, method)
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            Action act = () => sut.TestView<object>(method, "Property");

            // ASSERT
            act.ShouldThrow<ZapException>().WithMessage(Resources.CallViewUnknownResult);
            httpClientMock.Verify();
        }

        #endregion

        #region Action

        [Theory, AutoTestData]
        public void CallAction_WithApiKey(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Frozen]Mock<IZapProcess> zapProcessMock,
            ComponentBaseStub sut,
            string method,
            string apiKey)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, method, null, null, apiKey)
                .ReturnsOkResult()
                .Verifiable();
            zapProcessMock.SetupGet(m => m.ApiKey)
                .Returns(apiKey)
                .Verifiable();

            // ACT
            sut.TestAction(method);

            // ASSERT
            httpClientMock.Verify();
            zapProcessMock.Verify();
        }

        [Theory, AutoTestData]
        public void CallAction_NoResult(
            [Frozen]Mock<IHttpClient> httpClientMock,
            ComponentBaseStub sut,
            string method)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, method)
                .ReturnsNull()
                .Verifiable();

            // ACT
            Action act = () => sut.TestAction(method);

            // ASSERT
            act.ShouldThrow<ZapException>().WithMessage(Resources.ResultFromServerWasEmpty);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void CallAction_FailedResult(
            [Frozen]Mock<IHttpClient> httpClientMock,
            ComponentBaseStub sut,
            string method)
        {
            // ARRANGE
            var json = new JObject(
                    new JProperty("Result", "FAIL"));
            httpClientMock.SetupApiCall(sut, CallType.Action, method)
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            Action act = () => sut.TestAction(method);

            // ASSERT
            act.ShouldThrow<ZapException>().WithMessage(Resources.CallActionFailedResult);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void CallAction_UnknownResult_PropertyMissing(
            [Frozen]Mock<IHttpClient> httpClientMock,
            ComponentBaseStub sut,
            string method)
        {
            // ARRANGE
            var json = new JObject(
                    new JProperty("AlternativeResult", "OK"));
            httpClientMock.SetupApiCall(sut, CallType.Action, method)
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            Action act = () => sut.TestAction(method);

            // ASSERT
            act.ShouldThrow<ZapException>().WithMessage(Resources.CallActionUnknownResult);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void CallAction_UnknownResult_PropertyValue(
            [Frozen]Mock<IHttpClient> httpClientMock,
            ComponentBaseStub sut,
            string method)
        {
            // ARRANGE
            var json = new JObject(
                    new JProperty("Result", "ALTERNATIVE"));
            httpClientMock.SetupApiCall(sut, CallType.Action, method)
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            Action act = () => sut.TestAction(method);

            // ASSERT
            act.ShouldThrow<ZapException>().WithMessage(Resources.CallActionUnknownResult);
            httpClientMock.Verify();
        }

        #endregion
    }
}
