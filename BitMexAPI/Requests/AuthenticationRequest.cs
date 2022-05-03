using BitMexAPI.Messages;
using BitMexAPI.Utils;
using BitMexAPI.Validations;
using System.Runtime.Serialization;

namespace BitMexAPI.Requests
{
    public class AuthenticationRequest : RequestBase
    {
        private readonly string _apiKey;
        private readonly string _authSig;
        private readonly long _authNonce;
        private readonly string _authPayload;

        public AuthenticationRequest(string apiKey, string apiSecret)
        {
            BitmexValidation.ValidateInput(apiKey, nameof(apiKey));
            BitmexValidation.ValidateInput(apiSecret, nameof(apiSecret));

            _apiKey = apiKey;

            _authNonce = BitmexAuthentication.CreateAuthNonce();
            _authPayload = BitmexAuthentication.CreateAuthPayload(_authNonce);

            _authSig = BitmexAuthentication.CreateSignature(apiSecret, _authPayload);
        }

        [IgnoreDataMember]
        public override MessageType Operation => MessageType.authKeyExpires;

        public object[] Args => new object[]
        {
            _apiKey,
            _authNonce,
            _authSig
        };
    }
}