namespace AgileActorsApp.Validator
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
