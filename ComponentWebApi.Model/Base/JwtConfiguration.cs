namespace ComponentWebApi.Model.Base
{
    /// <summary>
    /// JWT Token配置
    /// </summary>
    public class JwtConfiguration
    {
        /// <summary>
        ///     AccessToken密钥
        /// </summary>
        // [JsonProperty("accessSecret")]
        public string AccessSecret { get; set; }

        /// <summary>
        ///     RefreshToken密钥
        /// </summary>
        // [JsonProperty("refreshSecret")]
        public string RefreshSecret { get; set; }

        /// <summary>
        ///     签发人
        /// </summary>
        // [JsonProperty("issuer")]
        public string Issuer { get; set; }

        /// <summary>
        ///     被签发人
        /// </summary>
        // [JsonProperty("audience")]
        public string Audience { get; set; }

        /// <summary>
        ///     有效时长
        /// </summary>
        // [JsonProperty("accessExpiration")]
        public int AccessExpiration { get; set; }

        /// <summary>
        ///     refreshToken的有效时长
        /// </summary>
        // [JsonProperty("refreshExpiration")]
        public int RefreshExpiration { get; set; }

        /// <summary>
        ///     允许的时差
        /// </summary>
        // [JsonProperty("clockSkew")]
        public int ClockSkew { get; set; }
    }
}