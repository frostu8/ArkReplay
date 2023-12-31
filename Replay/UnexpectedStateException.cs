namespace ArkReplay.Replay
{
    [System.Serializable]
    public class UnexpectedStateException : System.Exception
    {
        public UnexpectedStateException() : base("Game in unexpected state")
        { }
        public UnexpectedStateException(string message) : base(message) { }
        public UnexpectedStateException(string message, System.Exception inner) : base(message, inner) { }
        protected UnexpectedStateException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}