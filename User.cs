namespace UserModel
{
    public class User
    {
        public string? Name { get; set; }
        private string? _password { get; set; }
        public string? Id { get; set; }
        private string? _challenge = null;
        private string? _token = null;
        private bool _isEnable;
        private enum _role
        {
            User,
            Admin,
            LabAdministrator
        }
        public User(string Id, string Name, string Password)
        {
            this.Id = Id; //This is solely to fix the csv header issue I had with HTTP when reading the files.
            this.Name = Name;
            SavePassword = Password;
        }

        private bool ValidatePassword(string? paswd)
        {
            return true;
        }
        internal string? Password
        {
            get { return _password; }
            private set
            {
                if (ValidatePassword(value))
                {
                    _password = value;
                }
                else
                {
                    _password = value;
                }
            }
        }

        public string? SavePassword
        {
            get { return Password; }
            private set{
                Password = value;
            }
        }
        internal string? CreateChallenge()
        {
            _challenge = Guid.NewGuid().ToString();
            return _challenge;
        }

        private string? CalculateChallenge(string challenge)
        {
            if (_challenge == null)
            {
                return null;
            }
            return $"{challenge}<{_password}";
        }

        public bool VerifyChallenge(string challenge)
        {
            if (_challenge == null)
            {
                return false;
            }
            string? ServerChallenge = CalculateChallenge(challenge);
            return ServerChallenge == challenge;
        }

        internal string? CreateToken()
        {
            _token = $"token:<{Id}>";
            return _token;
        }

        internal bool VerifyToken(string? token)
        {
            return token == _token;
        }

    }
    // public class Credentials
    // {
    //     private List<User> _users = null;

    //     public User GetUser(User user)
    //     {
    //         return _users.Find(x => x == user);
    //     }

    //     public User GetUserId(string? Id){
    //         return _users.Find(x => x.Id == Id);
    //     }

    //     internal bool GetUserByToken(string? token)
    //     {
    //         var user = _users.Find(x => x.VerifyToken(token));
    //         return user != null;
    //     }

    //     public List<User> GetAll()
    //     {
    //         return _users;
    //     }
    // }
}
