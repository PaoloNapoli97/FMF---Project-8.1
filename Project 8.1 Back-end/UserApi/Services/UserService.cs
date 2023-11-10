using FileManager.Controller;
using UserModel;

namespace Service
{
    public class UserService
    {
        private const string UserDb = "UsersDb.csv";
        UserFileManager userFileManager = new();
        private List<User> users = new();
        public void CreateUserDb(){
            userFileManager.CreateFile(UserDb);
        }
        public void WriteUsers(){
            userFileManager.WriteItems(users, UserDb);
        }
        public List<User> ReadUsers(){
            users = userFileManager.ReadItems<User>(UserDb);
            return users;
        }
        public User ReadUserById(string id){
            return users.Find(x => x.Id == id);
        }

    }
}