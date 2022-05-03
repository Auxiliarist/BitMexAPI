using BitMexAPI.Json;
using BitMexAPI.Requests;
using BitMexAPI.Responses;
using BitMexAPI.Responses.Books;
using BitMexAPI.Responses.Liquidation;
using BitMexAPI.Responses.Orders;
using BitMexAPI.Responses.Positions;
using BitMexAPI.Responses.Quotes;
using BitMexAPI.Responses.TradeBins;
using BitMexAPI.Responses.Trades;
using BitMexAPI.Responses.Wallets;
using BitMexAPI.Validations;
using BitMexAPI.Websocket;
using Serilog;
using ServiceStack.Text;
using System;
using System.Threading.Tasks;

namespace BitMexAPI.Client
{
    public class BitmexWebsocketClient : IDisposable
    {
        private readonly BitmexWebsocketCommunicator _communicator;
        private readonly IDisposable _messageReceivedSubsciption;

        /// <summary> Provided message streams </summary>
        public BitmexStream Streams { get; } = new BitmexStream();

        public BitmexWebsocketClient(BitmexWebsocketCommunicator communicator)
        {
            BitmexValidation.ValidateInput(communicator, nameof(communicator));

            _communicator = communicator;
            _messageReceivedSubsciption = _communicator.MessageReceived.Subscribe(HandleMessage);
        }

        /// <summary> 
        ///    Serializes request and sends message via websocket communicator. It logs and re-throws
        ///    every exception.
        /// </summary>
        /// <param name="request"> Request/message to be sent </param>
        public async Task Send<T>(T request) where T : RequestBase
        {
            try
            {
                BitmexValidation.ValidateInput(request, nameof(request));

                var serialized = request.IsRaw ? request.OperationString : BitmexJsonSerializer.Serialize(request);
                await _communicator.Send(serialized);
            }
            catch (Exception e)
            {
                Log.Error(e, L($"Exception while sending message '{request}'. Error: {e.Message}"));
                throw;
            }
        }

        /// <summary> Sends authentication request via websocket communicator </summary>
        /// <param name="apiKey"> Your API key </param>
        /// <param name="apiSecret"> Your API secret </param>
        public Task Authenticate(string apiKey, string apiSecret)
        {
            return Send(new AuthenticationRequest(apiKey, apiSecret));
        }

        private void HandleMessage(string message)
        {
            try
            {
                bool handled;
                var messageSafe = (message ?? string.Empty).Trim();

                if (messageSafe.StartsWith("{"))
                {
                    handled = HandleObjectMessage(messageSafe);
                    if (handled)
                        return;
                }

                handled = HandleRawMessage(messageSafe);
                if (handled)
                    return;

                Log.Warning(L($"Unhandled response:  '{messageSafe}'"));
            }
            catch (Exception e)
            {
                Log.Error(e, L("Exception while receiving message"));
            }
        }

        private bool HandleRawMessage(string msg)
        {
            // ******************** ADD RAW HANDLERS BELOW ********************

            return
                PongResponse.TryHandle(msg, Streams.PongSubject);
        }

        private bool HandleObjectMessage(string msg)
        {
            var response = BitmexJsonSerializer.Deserialize<JsonObject>(msg);

            // ******************** ADD OBJECT HANDLERS BELOW ********************

            return

                TradeResponse.TryHandle(response, Streams.TradesSubject) ||
                TradeBinResponse.TryHandle(response, Streams.TradeBinSubject) ||
                BookResponse.TryHandle(response, Streams.BookSubject) ||
                QuoteResponse.TryHandle(response, Streams.QuoteSubject) ||
                LiquidationResponse.TryHandle(response, Streams.LiquidationSubject) ||
                PositionResponse.TryHandle(response, Streams.PositionSubject) ||
                OrderResponse.TryHandle(response, Streams.OrderSubject) ||
                WalletResponse.TryHandle(response, Streams.WalletSubject) ||

                ErrorResponse.TryHandle(response, Streams.ErrorSubject) ||
                SubscribeResponse.TryHandle(response, Streams.SubscribeSubject) ||
                InfoResponse.TryHandle(response, Streams.InfoSubject) ||
                AuthenticationResponse.TryHandle(response, Streams.AuthenticationSubject);
        }

        private string L(string msg)
        {
            return $"[BMX WEBSOCKET CLIENT] {msg}";
        }

        /// <summary> Cleanup everything </summary>
        public void Dispose()
        {
            _messageReceivedSubsciption?.Dispose();
        }
    }
}