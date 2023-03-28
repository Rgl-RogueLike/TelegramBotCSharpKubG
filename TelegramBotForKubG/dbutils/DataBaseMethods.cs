using Microsoft.EntityFrameworkCore;

namespace TelegramBotForKubG.dbutils
{
    internal class DataBaseMethods
    {
        public static async Task GetLogin(string login)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var existingStudent = await context.Students.FirstOrDefaultAsync(x => x.Login == login);
                if (existingStudent != null)
                {
                    Console.WriteLine("Пользователь с таким именем уже существует!");
                }
                else
                {
                    var newStudent = new Students { Login = login };
                    await context.Students.AddAsync(newStudent);
                    await context.SaveChangesAsync();
                    Console.WriteLine("User create successfully.");
                }
            }
        }

        public static async Task AddCodes(string facultyName, string code)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var faculty = await context.Faculties.FirstOrDefaultAsync(f => f.Name == facultyName);
                if(faculty != null)
                {
                    var newCode = new Codes { Faculty_Id = faculty.Id, Code = code };
                    await context.AddAsync(newCode);
                    await context.SaveChangesAsync();
                    Console.WriteLine("Code added successfully.");
                }
                else
                {
                    Console.WriteLine("Faculty not found.");
                }
            }
        }

        public static async Task AddFaculties(string nameFaculty)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var faculty = new Faculties { Name = nameFaculty };
                context.Faculties.Add(faculty);
                await context.SaveChangesAsync();
            }
        }

        public static async Task InputCode(string login, int code)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var studentCode = await context.Students.FirstOrDefaultAsync(x => x.Login == login);
                if(studentCode != null)
                {
                    studentCode.Code_Id = code;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Error. User not found");
                }
            }
        }
    }
}
