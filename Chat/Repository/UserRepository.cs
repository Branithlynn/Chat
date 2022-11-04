using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Chat.Entity;

namespace Chat.Repository
{
    class UserRepository
    {
        private readonly string filePath;

        public UserRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
         
            while (!sr.EndOfStream)
            {
                User user = new User();
                user.Id = Convert.ToInt32(sr.ReadLine());
                user.Username = sr.ReadLine();
                user.Password = sr.ReadLine();
                user.IsAdmin = Convert.ToBoolean(sr.ReadLine());
                if (user.Username == username && user.Password == password)
                {
                    sr.Close();
                    fs.Close();
                    return user;
                }
            }
            sr.Close();
            fs.Close();
            
            return null;
        }
        public int GetNextId()
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader reader = new StreamReader(fs);

            int id = 1;
            
            while (!reader.EndOfStream)
            {
                User user = new User();
                user.Id = Convert.ToInt32(reader.ReadLine());
                user.Username = reader.ReadLine();
                user.Password = reader.ReadLine();
                user.IsAdmin = Convert.ToBoolean(reader.ReadLine());

                if (id <= user.Id)
                {
                    id = user.Id + 1;
                }
            }


            reader.Close();
            fs.Close();
            return id;
        }
        public User GetById(int id)
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
          
            while (!sr.EndOfStream)
            {
                User user = new User();
                user.Id = Convert.ToInt32(sr.ReadLine());
                user.Username = sr.ReadLine();
                user.Password = sr.ReadLine();
                user.IsAdmin = Convert.ToBoolean(sr.ReadLine());

                if (user.Id == id)
                {
                    
                    sr.Close();
                    fs.Close();
                    return user;
                }
            }

          
            sr.Close();
            fs.Close();
            return null;

        }
        public User GetByName(string name)
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            while (!sr.EndOfStream)
            {
                User user = new User();
                user.Id = Convert.ToInt32(sr.ReadLine());
                user.Username = sr.ReadLine();
                user.Password = sr.ReadLine();
                user.IsAdmin = Convert.ToBoolean(sr.ReadLine());

                if (user.Username == name)
                {

                    sr.Close();
                    fs.Close();
                    return user;
                }
            }


            sr.Close();
            fs.Close();
            return null;

        }
        public List<User> GetAll()
        {
            List<User> result = new List<User>();

            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    User user = new User();
                    user.Id = Convert.ToInt32(sr.ReadLine());
                    user.Username = sr.ReadLine();
                    user.Password = sr.ReadLine();
                    user.IsAdmin = Convert.ToBoolean(sr.ReadLine());

                    result.Add(user);
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return result;
        }
        public void Save(User item)
        {
            if (item.Id > 0)
            {
                Update(item);
            }
            else
            {
                Insert(item);
            }

        }
        public void Delete(User deletedUser)
        {
            string tempFilePath = "temp." + filePath;

            FileStream ffs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ffs);

            FileStream sfs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(sfs);
        
            while (!sr.EndOfStream)
            {
                User user = new User();
                user.Id = Convert.ToInt32(sr.ReadLine());
                user.Username = sr.ReadLine();
                user.Password = sr.ReadLine();
                user.IsAdmin = Convert.ToBoolean(sr.ReadLine());

                if (user.Id != deletedUser.Id)
                {
                    sw.WriteLine(user.Id);
                    sw.WriteLine(user.Username);
                    sw.WriteLine(user.Password);
                    sw.WriteLine(user.IsAdmin);
                }
                
            }
            sw.Close();
            sfs.Close();
            sr.Close();
            ffs.Close();

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

    
        public void Insert(User item)
        {
            item.Id = GetNextId();

            FileStream fs = new FileStream(filePath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                sw.WriteLine(item.Id);
                sw.WriteLine(item.Username);
                sw.WriteLine(item.Password);
                sw.WriteLine(item.IsAdmin);
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        private void Update(User item)
        {
            string tempFilePath = "temp." + filePath;

            FileStream ffs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ffs);

            FileStream sfs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(sfs);
          
            while (!sr.EndOfStream)
            {
                User user = new User();
                user.Id = Convert.ToInt32(sr.ReadLine());
                user.Username = sr.ReadLine();
                user.Password = sr.ReadLine();
                user.IsAdmin = Convert.ToBoolean(sr.ReadLine());

                if (user.Id != item.Id)
                {
                    sw.WriteLine(user.Id);
                    sw.WriteLine(user.Username);
                    sw.WriteLine(user.Password);
                    sw.WriteLine(user.IsAdmin);
                }
                else
                {
                    sw.WriteLine(item.Id);
                    sw.WriteLine(item.Username);
                    sw.WriteLine(item.Password);
                    sw.WriteLine(item.IsAdmin);
                }
            }
            sw.Close();
            sfs.Close();
            sr.Close();
            ffs.Close();

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }
    }
}
