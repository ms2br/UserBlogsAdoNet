using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Data;
using System.Security.Cryptography;
using UsersBlogs.Helpers;
using UsersBlogs.Models;

namespace UsersBlogs.Services
{
    public class UserService : IBaseService<User>
    {

        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        public void CreateUser()
        {
            Console.Write("Enter First Name : ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name : ");
            string lastName = Console.ReadLine();
            Console.Write("Enter UserName : ");
            string userName = Console.ReadLine();
            Console.Write("Enter User Email Address : ");
            string userEmailAddress = Console.ReadLine();
            Console.Write("Enter User Password : ");
            string password = Console.ReadLine();
            Create(new User
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                UserEmailAddress = userEmailAddress,
                Password = password
            });
        }
        public bool Login(string userName, string password)
        {
            return GetAll().Any(x => x.UserName == userName && x.Password == passWordHash(password));
        }
        public void UpdateUser()
        {
            GetAllWrite();
            Console.Write("Enter Answer : ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter First Name : ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name : ");
            string lastName = Console.ReadLine();
            Console.Write("Enter UserName : ");
            string userName = Console.ReadLine();
            Console.Write("Enter User Email Address : ");
            string userEmailAddress = Console.ReadLine();
            Console.Write("Enter User Password : ");
            string password = Console.ReadLine();

            Update(id, new User
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                UserEmailAddress = userEmailAddress,
                Password = password
            });
        }

        public int Create(User data)
        {
            string sqlQuery = $@"insert into Users(FirstName,LastName,UserName,UserEmailAddress,Password,IsDeleted)
              values('{data.FirstName}','{data.LastName}','{data.UserName}','{data.UserEmailAddress}','{passWordHash(data.Password)}','{false}')";
            return SqlHelper.Exec(sqlQuery);
        }

        public int Delete(int id)
        {
            #region Yol 1
            //string sqlQuery = $"delete Blogs where UserID = {id};delete Users where ID = {id}";
            #endregion

            #region Yol 2
            string sqlQuery = $@"update Users set IsDeleted = 1 where id = {id}";
            #endregion
            return SqlHelper.Exec(sqlQuery);
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            DataTable dataTable = SqlHelper.GetDatas("select * from Users");
            foreach (DataRow items in dataTable.Rows)
            {
                if (!(bool)items["IsDeleted"])
                    users.Add(new User
                    {
                        Id = (int)items["Id"],
                        FirstName = (string)items["FirstName"],
                        LastName = (string)items["LastName"],
                        UserName = (string)items["UserName"],
                        UserEmailAddress = (string)items["UserEmailAddress"],
                        Password = (string)items["Password"]
                    });
            }
            return users;
        }

        public void GetAllWrite()
        {
            GetAll().ForEach(x =>
            {
                Console.WriteLine(x);
            });
        }

        public User GetById(int id)
        {
            if (id > 0)
                return GetAll().FirstOrDefault(x => x.Id == id);
            return null;

        }

        public void UpdateAll()
        {
            Console.WriteLine("1.UpdateAll");
            Console.WriteLine("2.UpdateFirstName");
            Console.WriteLine("3.UpdateLastName");
            Console.WriteLine("4.UpdateUserName");
            Console.WriteLine("5.UpdateUserEmailAddress");
            Console.WriteLine("6.UpdateUserPassword");
            Console.Write("Enter Answer : ");
            int userAnswer = Convert.ToInt32(Console.ReadLine());

            switch (userAnswer)
            {
                case 1:
                    UpdateUser();
                    break;
                case 2:
                    GetAllWrite();
                    Console.Write("Enter User Id : ");
                    UpdateFirstName(Convert.ToInt32(Console.ReadLine()));
                    break;

                case 3:
                    GetAllWrite();
                    Console.Write("Enter User Id : ");
                    UpdateLastName(Convert.ToInt32(Console.ReadLine()));
                    break;

                case 4:
                    GetAllWrite();
                    Console.Write("Enter User Id : ");
                    UpdateUserName(Convert.ToInt32(Console.ReadLine()));
                    break;

                case 5:
                    GetAllWrite();
                    Console.Write("Enter User Id : ");
                    UpdateUserEmailAddress(Convert.ToInt32(Console.ReadLine()));
                    break;

                case 6:
                    GetAllWrite();
                    Console.Write("Enter User Id : ");
                    UpdateUserPassword(Convert.ToInt32(Console.ReadLine()));
                    break;
            }

        }

        public int Update(int id, User data)
        {
            string query = $@"
                update Users set FirstName = '{data.FirstName}',LastName = '{data.LastName}',UserName = '{data.UserName}', UserEmailAddress = '{data.UserEmailAddress}', Password = '{passWordHash(data.Password)}' where id = {id}";
            return SqlHelper.Exec(query);
        }

        public int UpdateFirstName(int id)
        {
            Console.Write("Enter First Name : ");
            string newFirstName = Console.ReadLine();

            string query = $@"
                update Users set FirstName = '{newFirstName}'where id = {id}";
            return SqlHelper.Exec(query);
        }

        public int UpdateLastName(int id)
        {
            Console.Write("Enter Last Name : ");
            string newLastName = Console.ReadLine();

            string query = $@"
                update Users set LastName = '{newLastName}' where id = {id}";
            return SqlHelper.Exec(query);
        }

        public int UpdateUserName(int id)
        {
            Console.Write("Enter User Name : ");
            string newUserName = Console.ReadLine();

            string query = $@"
                update Users set UserName = '{newUserName}' where id = {id}";
            return SqlHelper.Exec(query);
        }

        public int UpdateUserEmailAddress(int id)
        {
            Console.Write("Enter User Email Address : ");
            string newUserEmailAddress = Console.ReadLine();

            string query = $@"
                update Users set UserEmailAddress = '{newUserEmailAddress}' where id = {id}";
            return SqlHelper.Exec(query);
        }

        public int UpdateUserPassword(int id)
        {
            Console.Write("Enter User Email Address : ");
            string newPassword = Console.ReadLine();

            string query = $@"
                update Users set Password = '{passWordHash(newPassword)}' where id = {id}";
            return SqlHelper.Exec(query);
        }

        string passWordHash(string passWord)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passWord!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
