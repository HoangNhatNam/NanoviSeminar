namespace NanoviConference.Exceptions
{
    public class NcException : Exception
    {
        public NcException()
        {

        }

        public NcException(string message) : base(message)
        {

        }

        public NcException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
