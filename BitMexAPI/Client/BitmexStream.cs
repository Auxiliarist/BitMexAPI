using BitMexAPI.Responses;
using BitMexAPI.Responses.Books;
using BitMexAPI.Responses.Liquidation;
using BitMexAPI.Responses.Orders;
using BitMexAPI.Responses.Positions;
using BitMexAPI.Responses.Quotes;
using BitMexAPI.Responses.TradeBins;
using BitMexAPI.Responses.Trades;
using BitMexAPI.Responses.Wallets;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace BitMexAPI.Client
{
    public class BitmexStream
    {
        internal readonly Subject<ErrorResponse> ErrorSubject = new Subject<ErrorResponse>();
        internal readonly Subject<InfoResponse> InfoSubject = new Subject<InfoResponse>();
        internal readonly Subject<PongResponse> PongSubject = new Subject<PongResponse>();
        internal readonly Subject<SubscribeResponse> SubscribeSubject = new Subject<SubscribeResponse>();
        internal readonly Subject<AuthenticationResponse> AuthenticationSubject = new Subject<AuthenticationResponse>();

        internal readonly Subject<TradeResponse> TradesSubject = new Subject<TradeResponse>();
        internal readonly Subject<TradeBinResponse> TradeBinSubject = new Subject<TradeBinResponse>();
        internal readonly Subject<BookResponse> BookSubject = new Subject<BookResponse>();
        internal readonly Subject<QuoteResponse> QuoteSubject = new Subject<QuoteResponse>();
        internal readonly Subject<LiquidationResponse> LiquidationSubject = new Subject<LiquidationResponse>();

        internal readonly Subject<WalletResponse> WalletSubject = new Subject<WalletResponse>();
        internal readonly Subject<OrderResponse> OrderSubject = new Subject<OrderResponse>();
        internal readonly Subject<PositionResponse> PositionSubject = new Subject<PositionResponse>();

        public IObservable<ErrorResponse> ErrorStream => ErrorSubject.AsObservable();
        public IObservable<InfoResponse> InfoStream => InfoSubject.AsObservable();
        public IObservable<PongResponse> PongStream => PongSubject.AsObservable();
        public IObservable<SubscribeResponse> SubscribeStream => SubscribeSubject.AsObservable();
        public IObservable<AuthenticationResponse> AuthenticationStream => AuthenticationSubject.AsObservable();

        public IObservable<TradeResponse> TradesStream => TradesSubject.AsObservable();
        public IObservable<TradeBinResponse> TradeBinStream => TradeBinSubject.AsObservable();
        public IObservable<BookResponse> BookStream => BookSubject.AsObservable();
        public IObservable<QuoteResponse> QuoteStream => QuoteSubject.AsObservable();
        public IObservable<LiquidationResponse> LiquidationStream => LiquidationSubject.AsObservable();

        /// <summary> Stream for every wallet balance update </summary>
        public IObservable<WalletResponse> WalletStream => WalletSubject.AsObservable();

        /// <summary> Stream of all active orders </summary>
        public IObservable<OrderResponse> OrderStream => OrderSubject.AsObservable();

        /// <summary> Stream of all active positions </summary>
        public IObservable<PositionResponse> PositionStream => PositionSubject.AsObservable();
    }
}