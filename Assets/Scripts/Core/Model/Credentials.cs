namespace Core.Model
{
    public struct Credentials
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Credentials(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}