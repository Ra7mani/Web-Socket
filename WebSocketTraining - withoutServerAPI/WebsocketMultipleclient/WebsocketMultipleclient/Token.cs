using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebsocketMultipleclient.TokenServer
{
    public class Token
    {
        public Token(string value, string expiryDate)
        {
            Value = value;
            ExpiryDate = expiryDate;
        }
        [Required]
        public string Value { get; set; }
        [Required]
        public String ExpiryDate { get; set; }
    }
}
