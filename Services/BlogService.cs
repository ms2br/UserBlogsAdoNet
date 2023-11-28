using System.Data;
using UsersBlogs.Helpers;
using UsersBlogs.Models;

namespace UsersBlogs.Services
{
    internal class BlogService : IBaseService<Blog>
    {
        public int UserBlogId { get; set; }

        public BlogService(int userId)
        {
            UserBlogId = userId;
        }

        public void CreateBlog()
        {
            Console.Write("Enter Title : ");
            string title = Console.ReadLine();
            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            Create(new Blog
            {
                Title = title,
                Description = description,
                UserId = UserBlogId
            });
        }

        public void UpdateUser()
        {
            GetAllWrite();
            Console.Write("Enter Blog Id : ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Title : ");
            string title = Console.ReadLine();
            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            Update(id, new Blog
            {
                Title = title,
                Description = description,
            });
        }

        public int Create(Blog data)
        {
            string sqlQuery = $@"insert into Blogs(Title,Description,UserId,IsDeleted)
              values('{data.Title}','{data.Description}','{data.UserId}','{false}')";
            return SqlHelper.Exec(sqlQuery);
        }

        public int Delete(int id)
        {
            #region Yol 1
            //string sqlQuery = $"delete Blogs where ID = {id}";
            #endregion

            #region Yol 2
            string sqlQuery = $@"update Blogs set IsDeleted = 1 where id = {id}";
            #endregion
            return SqlHelper.Exec(sqlQuery);
        }

        public List<Blog> GetAll()
        {
            List<Blog> blogs = new List<Blog>();
            DataTable dataTable = SqlHelper.GetDatas("select * from Blogs");
            foreach (DataRow items in dataTable.Rows)
            {
                if (!(bool)items["IsDeleted"])
                    blogs.Add(new Blog
                    {
                        Id = (int)items["Id"],
                        Title = (string)items["Title"],
                        Description = (string)items["Description"],
                        UserId = (int)items["UserId"]
                    });
            }
            return blogs;
        }

        public void GetAllWrite()
        {
            GetAll().ForEach(x =>
            {
                Console.WriteLine(x);
            });
        }

        public Blog GetById(int id)
        {
            if (id > 0)
                return GetAll().FirstOrDefault(x => x.Id == id);
            return null;

        }

        public void UpdateAll()
        {
            Console.WriteLine("1.UpdateAll");
            Console.WriteLine("2.UpdateTitle");
            Console.WriteLine("3.UpdateDescription");
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
                    UpdateTitle(Convert.ToInt32(Console.ReadLine()));
                    break;

                case 3:
                    GetAllWrite();
                    Console.Write("Enter User Id : ");
                    UpdateDescription(Convert.ToInt32(Console.ReadLine()));
                    break;
            }

        }

        public int Update(int id, Blog data)
        {
            string query = $@"update Blogs set Title = '{data.Title}',Description = '{data.Description}' where id = {id}";
            return SqlHelper.Exec(query);
        }

        public int UpdateTitle(int id)
        {
            Console.Write("Enter Title: ");
            string newTitle = Console.ReadLine();

            string query = $@"
                update Blogs set Title = '{newTitle}'where id = {id}";
            return SqlHelper.Exec(query);
        }

        public int UpdateDescription(int id)
        {
            Console.Write("Enter Last Name : ");
            string newDescription = Console.ReadLine();

            string query = $@"
                update Blogs set Description = '{newDescription}' where id = {id}";
            return SqlHelper.Exec(query);
        }
    }
}
