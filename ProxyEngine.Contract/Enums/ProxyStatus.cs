namespace ProxyEngine.Contract.Enums
{
    public enum ProxyStatus : short
    {
        Success = 1,
        Failure = 0,
        Retry = -1,
        Cancel = -2
    }
}
