using System;
using System.Collections.Generic;
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

            public string TestOther(string method)
            {
                return CallOther(method);
            }

            public byte[] TestOtherData(string method)
            {
                return CallOtherData(method);
            }

            public IEnumerable<string> TestParseJsonListString(string input)
            {
                return ParseJsonListString(input);
            }
        }

        #endregion

        #region View

        [Theory, AutoTestData]
        public void CallView(
            [Frozen]Mock<IHttpClient> httpClientMock,
            ComponentBaseStub sut,
            string method,
            string propertyName,
            string expected)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty(propertyName, expected));
            httpClientMock.SetupApiCall(sut, CallType.View, method)
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.TestView<string>(method, propertyName);

            // ASSERT
            result.Should().Be(expected);
            httpClientMock.Verify();
        }

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
        public void CallAction(
            [Frozen]Mock<IHttpClient> httpClientMock,
            ComponentBaseStub sut,
            string method)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Action, method)
                .ReturnsOkResult()
                .Verifiable();

            // ACT
            sut.TestAction(method);

            // ASSERT
            httpClientMock.Verify();
        }

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

        #region Other

        [Theory, AutoTestData]
        public void CallOther(
            [Frozen]Mock<IHttpClient> httpClientMock,
            ComponentBaseStub sut,
            string method,
            string expected)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Other, method, null, DataType.Other)
                .Returns(expected)
                .Verifiable();

            // ACT
            var result = sut.TestOther(method);

            // ASSERT
            result.Should().Be(expected);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void CallOther_WithApiKey(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Frozen]Mock<IZapProcess> zapProcessMock,
            ComponentBaseStub sut,
            string method,
            string apiKey,
            string expected)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Other, method, null, DataType.Other, apiKey)
                .Returns(expected)
                .Verifiable();
            zapProcessMock.SetupGet(m => m.ApiKey)
                .Returns(apiKey)
                .Verifiable();

            // ACT
            var result = sut.TestOther(method);

            // ASSERT
            result.Should().Be(expected);
            httpClientMock.Verify();
            zapProcessMock.Verify();
        }

        [Theory, AutoTestData]
        public void CallOther_NoResult(
            [Frozen]Mock<IHttpClient> httpClientMock,
            ComponentBaseStub sut,
            string method,
            string expected)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Other, method, null, DataType.Other)
                .ReturnsNull()
                .Verifiable();

            // ACT
            Action act = () => sut.TestOther(method);

            // ASSERT
            act.ShouldThrow<ZapException>().WithMessage(Resources.ResultFromServerWasEmpty);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void CallOtherData(
            [Frozen]Mock<IHttpClient> httpClientMock,
            ComponentBaseStub sut,
            string method,
            byte[] expected)
        {
            // ARRANGE
            httpClientMock.SetupOtherDataApiCall(sut, method)
                .Returns(expected)
                .Verifiable();

            // ACT
            var result = sut.TestOtherData(method);

            // ASSERT
            result.Should().Equal(expected);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void CallOtherData_NoResult(
            [Frozen]Mock<IHttpClient> httpClientMock,
            ComponentBaseStub sut,
            string method)
        {
            // ARRANGE
            httpClientMock.SetupOtherDataApiCall(sut, method)
                .Returns(new byte[0])
                .Verifiable();

            // ACT
            Action act = () => sut.TestOtherData(method);

            // ASSERT
            act.ShouldThrow<ZapException>().WithMessage(Resources.ResultFromServerWasEmpty);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void CallOtherData_WithApiKey(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Frozen]Mock<IZapProcess> zapProcessMock,
            ComponentBaseStub sut,
            string method,
            string apiKey,
            byte[] expected)
        {
            // ARRANGE
            httpClientMock.SetupOtherDataApiCall(sut, method, null, apiKey)
                .Returns(expected)
                .Verifiable();
            zapProcessMock.SetupGet(m => m.ApiKey)
                .Returns(apiKey)
                .Verifiable();

            // ACT
            var result = sut.TestOtherData(method);

            // ASSERT
            result.Should().Equal(expected);
            httpClientMock.Verify();
            zapProcessMock.Verify();
        }

        #endregion
    }
}
