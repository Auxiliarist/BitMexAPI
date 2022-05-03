using BitMexAPI.Client;
using BitMexAPI.Requests;
using BitMexAPI.Utils;
using BitMexAPI.Websocket;
using Serilog;
using Serilog.Events;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BitMexAPI.WPF
{
    /// <summary> Interaction logic for MainWindow.xaml </summary>
    public partial class MainWindow : Window
    {
        private static readonly string API_KEY = "Pzyl8N6APkJAYh1595Tj-K5R";
        private static readonly string API_SECRET = "QLMXw_AxUkJduLRycPmvBn_JPYBOzv_-TrA0nDuHGGmdWgBX";

        public MainWindow()
        {
            InitializeComponent();
            InitLogging();

            var url = BitmexValues.ApiWebsocketUrl;

            var communicator = new BitmexWebsocketCommunicator(url);

            communicator.ReconnectTimeoutMs = (int)TimeSpan.FromSeconds(30).TotalMilliseconds;

            communicator.ReconnectionHappened.Subscribe(type =>
                Log.Information($"Reconnection happened, type: {type}"));

            var client = new BitmexWebsocketClient(communicator);

            client.Streams.InfoStream.Subscribe(info =>
            {
                Log.Information($"Reconnection happened, Message: {info.Info}, Version: {info.Version:D}");
                SendSubscriptionRequests(client).Wait();
            });

            SubscribeToStreams(client);

            communicator.Start();
        }

        private static async Task SendSubscriptionRequests(BitmexWebsocketClient client)
        {
            //await client.Send(new PingRequest());
            //await client.Send(new BookSubscribeRequest("XBTUSD"));
            //await client.Send(new TradesSubscribeRequest("XBTUSD"));
            //await client.Send(new TradeBinSubscribeRequest("1m", "XBTUSD"));
            //await client.Send(new TradeBinSubscribeRequest("5m", "XBTUSD"));
            //await client.Send(new QuoteSubscribeRequest("XBTUSD"));
            //await client.Send(new LiquidationSubscribeRequest());

            if (!string.IsNullOrWhiteSpace(API_SECRET))
                await client.Authenticate(API_KEY, API_SECRET);
        }

        private static void SubscribeToStreams(BitmexWebsocketClient client)
        {
            client.Streams.ErrorStream.Subscribe(x =>
                Log.Warning($"Error received, message: {x.Error}, status: {x.Status}"));

            client.Streams.AuthenticationStream.Subscribe(async x =>
            {
                Log.Information($"Authentication happened, success: {x.Success}");
                if (x.Success)
                {
                    await client.Send(new WalletSubscribeRequest());
                    //client.Send(new OrderSubscribeRequest()).Wait();
                    //client.Send(new PositionSubscribeRequest()).Wait();
                }
            });

            client.Streams.SubscribeStream.Subscribe(x =>
                Log.Information($"Subscribed ({x.Success}) to {x.Subscribe}"));

            client.Streams.PongStream.Subscribe(x =>
                Log.Information($"Pong received ({x.Message})"));

            client.Streams.WalletStream.Subscribe(y =>
                y.Data.ToList().ForEach(x =>
                    Log.Information($"Wallet {x.Account}, {x.Currency} amount: {x.BalanceBtc}"))
            );

            client.Streams.OrderStream.Subscribe(y =>
                y.Data.ToList().ForEach(x =>
                    Log.Information(
                        $"Order {x.Symbol} updated. Time: {x.Timestamp:HH:mm:ss.fff}, Amount: {x.OrderQty}, " +
                        $"Price: {x.Price}, Direction: {x.Side}, Working: {x.WorkingIndicator}, Status: {x.OrdStatus}"))
            );

            client.Streams.PositionStream.Subscribe(y =>
                y.Data.ToList().ForEach(x =>
                    Log.Information(
                        $"Position {x.Symbol}, {x.Currency} updated. Time: {x.Timestamp:HH:mm:ss.fff}, Amount: {x.CurrentQty}, " +
                        $"Price: {x.LastPrice}, PNL: {x.UnrealisedPnl}"))
            );

            client.Streams.TradesStream.Subscribe(y =>
                y.Data.ToList().ForEach(x =>
                    Log.Information($"Trade {x.Symbol} executed. Time: {x.Timestamp:mm:ss.fff}, Amount: {x.Size}, " +
                                    $"Price: {x.Price}, Direction: {x.TickDirection}"))
            );

            client.Streams.BookStream.Subscribe(book =>
                book.Data.Take(100).ToList().ForEach(x => Log.Information(
                    $"Book | {book.Action} pair: {x.Symbol}, price: {x.Price}, amount {x.Size}, side: {x.Side}"))
            );

            client.Streams.QuoteStream.Subscribe(y =>
                y.Data.ToList().ForEach(x =>
                    Log.Information($"Quote {x.Symbol}. Bid: {x.BidPrice} - {x.BidSize} Ask: {x.AskPrice} - {x.AskSize}"))
            );

            client.Streams.LiquidationStream.Subscribe(y =>
                y.Data.ToList().ForEach(x =>
                    Log.Information(
                        $"Liquadation Action:{y.Action} OrderID:{x.OrderID} Symbol:{x.Symbol} Side:{x.Side} Price:{x.Price} leavesQty:{x.leavesQty}"))
            );

            client.Streams.TradeBinStream.Subscribe(y =>
                y.Data.ToList().ForEach(x =>
                Log.Information($"TradeBin Table:{y.Table} {x.Symbol} executed. Time: {x.Timestamp:mm:ss.fff}, Open: {x.Open}, " +
                        $"Close: {x.Close}, Volume: {x.Volume}"))
            );
        }

        private static void InitLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(LogEventLevel.Information)
                .CreateLogger();
        }
    }
}