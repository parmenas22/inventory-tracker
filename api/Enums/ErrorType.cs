using System.Runtime.Serialization;

namespace api.Enums
{
    public enum ErrorType
    {
        [EnumMember(Value = "Login Error")]
        LOGIN,
        [EnumMember(Value = "Unknown Error")]
        UNKNOWN
    }
}