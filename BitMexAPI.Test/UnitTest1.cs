using BitMexAPI.Client;
using BitMexAPI.Requests;
using BitMexAPI.Responses;
using BitMexAPI.Utils;
using BitMexAPI.Websocket;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BitMexAPI.Test
{
    public class UnitTest1
    {
        private static readonly string API_KEY = "Pzyl8N6APkJAYh1595Tj";
        private static readonly string API_SECRET = "QLMXw_AxUkJduLRycPmvBn_JPYBOzv_-TrA0nDuHGGmdWgBX";

        [Fact]
        public async Task PingPong()
        {
            var url = BitmexValues.ApiWebsocketUrl;
            using (var communicator = new BitmexWebsocketCommunicator(url))
            {
                PongResponse received = null;
                var receivedEvent = new ManualResetEvent(false);

                using (var client = new BitmexWebsocketClient(communicator))
                {

                    client.Streams.PongStream.Subscribe(pong =>
                    {
                        received = pong;
                        receivedEvent.Set();
                    });

                    await communicator.Start();

                    await client.Send(new PingRequest());

                    receivedEvent.WaitOne(TimeSpan.FromSeconds(30));

                    Assert.NotNull(received);
                }
            }
        }

        [Fact]
        public void CreateSignature_ShouldReturnCorrectString()
        {
            var nonce = BitmexAuthentication.CreateAuthNonce(123456);
            var payload = BitmexAuthentication.CreateAuthPayload(nonce);
            var signature = BitmexAuthentication.CreateSignature(payload, "api_secret");

            Assert.Equal("7657aa8b00b54ee7d58ed0ed42b6cad6d8b1e008bee4617b70d11cd87dbbc1e6", signature);
        }

        [Fact]
        public async Task Authentication()
        {
            //Skip.If(string.IsNullOrWhiteSpace(API_SECRET));

            var url = BitmexValues.ApiWebsocketUrl;
            using (var communicator = new BitmexWebsocketCommunicator(url))
            {
                AuthenticationResponse received = null;
                var receivedEvent = new ManualResetEvent(false);

                using (var client = new BitmexWebsocketClient(communicator))
                {

                    client.Streams.AuthenticationStream.Subscribe(auth =>
                    {
                        received = auth;
                        receivedEvent.Set();
                    });

                    await communicator.Start();

                    await client.Authenticate(API_KEY, API_SECRET);

                    receivedEvent.WaitOne(TimeSpan.FromSeconds(30));

                    Assert.NotNull(received);
                    Assert.True(received.Success);
                }
            }
        }

    }
}
