namespace IDM.Common.Domain
{
    /// <summary>
    /// Общее доменное исключение
    /// </summary>
    [Serializable]
    public class CorruptedInvariantException : Exception
    {
        public string Details { get; }

        public CorruptedInvariantException(string message) : base(message)
        {

        }

        public CorruptedInvariantException(string message, string details) : base(message)
        {
            Details = details;
        }
    }
}
