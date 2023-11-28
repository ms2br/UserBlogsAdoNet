using UsersBlogs.Services;

namespace UsersBlogs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserService userService = new UserService();
            userService.CreateUser();
            Console.WriteLine("1.Register");
            Console.WriteLine("2.Login");
            Console.Write("Enter User Answer : ");
            int answer = Convert.ToInt32(Console.ReadLine());
            bool loginController = false;

            switch (answer)
            {
                case 1:
                    userService.CreateUser();
                    break;

                case 2:
                    Console.Write("Enter UserName : ");
                    string userName = Console.ReadLine();

                    Console.Write("Enter User Password : ");
                    string password = Console.ReadLine();

                    loginController = userService.Login(userName, password);
                    break;
            }

            if (loginController)
            {
                Console.WriteLine("Login Olundu");
            }
            else
            {
                Console.WriteLine("Login Ugursuz Oldu");
            }
        }
    }
}