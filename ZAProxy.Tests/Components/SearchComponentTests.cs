using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;
using ZAProxy.Components;
using ZAProxy.Infrastructure;
using ZAProxy.Tests.TestUtils;
using FluentAssertions;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ZAProxy.Schema;
using HttpArchive;

namespace ZAProxy.Tests.Components
{
    [Trait("Component", "Search")]
    public class SearchComponentTests
    {
        [Theory, AutoTestData]
        public void ComponentName(
            [Greedy]SearchComponent sut)
        {
            // ACT
            var result = sut.ComponentName;

            // ASSERT
            result.Should().Be("search");
        }

        #region Views

        [Theory, AutoTestData]
        public void GetMessagesByHeader(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SearchComponent sut,
            string regex,
            string baseUrl,
            int start,
            int count,
            IEnumerable<Message> messages)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("messagesByHeaderRegex", JArray.FromObject(messages)));
            httpClientMock.SetupApiCall(sut, CallType.View, "messagesByHeaderRegex",
                new Parameters
                {
                    { "regex", regex },
                    { "baseurl", baseUrl },
                    { "start", start },
                    { "count", count }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetMessagesByHeader(regex, baseUrl, start, count);

            // ASSERT
            result.ShouldBeEquivalentTo(messages);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetMessagesByRequest(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SearchComponent sut,
            string regex,
            string baseUrl,
            int start,
            int count,
            IEnumerable<Message> messages)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("messagesByRequestRegex", JArray.FromObject(messages)));
            httpClientMock.SetupApiCall(sut, CallType.View, "messagesByRequestRegex",
                new Parameters
                {
                    { "regex", regex },
                    { "baseurl", baseUrl },
                    { "start", start },
                    { "count", count }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetMessagesByRequest(regex, baseUrl, start, count);

            // ASSERT
            result.ShouldBeEquivalentTo(messages);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetMessagesByResponse(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SearchComponent sut,
            string regex,
            string baseUrl,
            int start,
            int count,
            IEnumerable<Message> messages)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("messagesByResponseRegex", JArray.FromObject(messages)));
            httpClientMock.SetupApiCall(sut, CallType.View, "messagesByResponseRegex",
                new Parameters
                {
                    { "regex", regex },
                    { "baseurl", baseUrl },
                    { "start", start },
                    { "count", count }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetMessagesByResponse(regex, baseUrl, start, count);

            // ASSERT
            result.ShouldBeEquivalentTo(messages);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetMessagesByUrl(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SearchComponent sut,
            string regex,
            string baseUrl,
            int start,
            int count,
            IEnumerable<Message> messages)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("messagesByUrlRegex", JArray.FromObject(messages)));
            httpClientMock.SetupApiCall(sut, CallType.View, "messagesByUrlRegex",
                new Parameters
                {
                    { "regex", regex },
                    { "baseurl", baseUrl },
                    { "start", start },
                    { "count", count }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetMessagesByUrl(regex, baseUrl, start, count);

            // ASSERT
            result.ShouldBeEquivalentTo(messages);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetUrlsByHeader(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SearchComponent sut,
            string regex,
            string baseUrl,
            int start,
            int count,
            IEnumerable<MessageUrl> urls)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("urlsByHeaderRegex", JArray.FromObject(urls)));
            httpClientMock.SetupApiCall(sut, CallType.View, "urlsByHeaderRegex",
                new Parameters
                {
                    { "regex", regex },
                    { "baseurl", baseUrl },
                    { "start", start },
                    { "count", count }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetUrlsByHeader(regex, baseUrl, start, count);

            // ASSERT
            result.ShouldBeEquivalentTo(urls);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetUrlsByRequest(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SearchComponent sut,
            string regex,
            string baseUrl,
            int start,
            int count,
            IEnumerable<MessageUrl> urls)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("urlsByRequestRegex", JArray.FromObject(urls)));
            httpClientMock.SetupApiCall(sut, CallType.View, "urlsByRequestRegex",
                new Parameters
                {
                    { "regex", regex },
                    { "baseurl", baseUrl },
                    { "start", start },
                    { "count", count }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetUrlsByRequest(regex, baseUrl, start, count);

            // ASSERT
            result.ShouldBeEquivalentTo(urls);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetUrlsByResponse(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SearchComponent sut,
            string regex,
            string baseUrl,
            int start,
            int count,
            IEnumerable<MessageUrl> urls)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("urlsByResponseRegex", JArray.FromObject(urls)));
            httpClientMock.SetupApiCall(sut, CallType.View, "urlsByResponseRegex",
                new Parameters
                {
                    { "regex", regex },
                    { "baseurl", baseUrl },
                    { "start", start },
                    { "count", count }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetUrlsByResponse(regex, baseUrl, start, count);

            // ASSERT
            result.ShouldBeEquivalentTo(urls);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetUrlsByUrl(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SearchComponent sut,
            string regex,
            string baseUrl,
            int start,
            int count,
            IEnumerable<MessageUrl> urls)
        {
            // ARRANGE
            var json = new JObject(
                new JProperty("urlsByUrlRegex", JArray.FromObject(urls)));
            httpClientMock.SetupApiCall(sut, CallType.View, "urlsByUrlRegex",
                new Parameters
                {
                    { "regex", regex },
                    { "baseurl", baseUrl },
                    { "start", start },
                    { "count", count }
                })
                .Returns(json.ToString())
                .Verifiable();

            // ACT
            var result = sut.GetUrlsByUrl(regex, baseUrl, start, count);

            // ASSERT
            result.ShouldBeEquivalentTo(urls);
            httpClientMock.Verify();
        }

        #endregion

        #region Others

        [Theory, AutoTestData]
        public void GetHarByHeader(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SearchComponent sut,
            string regex,
            string baseUrl,
            int start,
            int count,
            Har har)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Other, "harByHeaderRegex",
                new Parameters
                {
                    { "regex", regex },
                    { "baseurl", baseUrl },
                    { "start", start },
                    { "count", count }
                }, DataType.Other)
                .Returns(Har.Serialize(har))
                .Verifiable();

            // ACT
            var result = sut.GetHarByHeader(regex, baseUrl, start, count);

            // ASSERT
            result.ShouldBeEquivalentTo(har);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetHarByRequest(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SearchComponent sut,
            string regex,
            string baseUrl,
            int start,
            int count,
            Har har)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Other, "harByRequestRegex",
                new Parameters
                {
                    { "regex", regex },
                    { "baseurl", baseUrl },
                    { "start", start },
                    { "count", count }
                }, DataType.Other)
                .Returns(Har.Serialize(har))
                .Verifiable();

            // ACT
            var result = sut.GetHarByRequest(regex, baseUrl, start, count);

            // ASSERT
            result.ShouldBeEquivalentTo(har);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetHarByResponse(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SearchComponent sut,
            string regex,
            string baseUrl,
            int start,
            int count,
            Har har)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Other, "harByResponseRegex",
                new Parameters
                {
                    { "regex", regex },
                    { "baseurl", baseUrl },
                    { "start", start },
                    { "count", count }
                }, DataType.Other)
                .Returns(Har.Serialize(har))
                .Verifiable();

            // ACT
            var result = sut.GetHarByResponse(regex, baseUrl, start, count);

            // ASSERT
            result.ShouldBeEquivalentTo(har);
            httpClientMock.Verify();
        }

        [Theory, AutoTestData]
        public void GetHarByUrl(
            [Frozen]Mock<IHttpClient> httpClientMock,
            [Greedy]SearchComponent sut,
            string regex,
            string baseUrl,
            int start,
            int count,
            Har har)
        {
            // ARRANGE
            httpClientMock.SetupApiCall(sut, CallType.Other, "harByUrlRegex",
                new Parameters
                {
                    { "regex", regex },
                    { "baseurl", baseUrl },
                    { "start", start },
                    { "count", count }
                }, DataType.Other)
                .Returns(Har.Serialize(har))
                .Verifiable();

            // ACT
            var result = sut.GetHarByUrl(regex, baseUrl, start, count);

            // ASSERT
            result.ShouldBeEquivalentTo(har);
            httpClientMock.Verify();
        }

        #endregion
    }
}
