using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Drawing.Slicer.Style;
using OfficeOpenXml.Drawing.Style.Fill;

namespace TelegramBotForKubG.dbutils
{
    internal class DataBaseMethods
    {
        public static async Task<Students> GetLogin(string login)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var existingStudent = await context.Students.FirstOrDefaultAsync(x => x.Login == login);
                if (existingStudent != null)
                {
                    return null;
                }
                else
                {
                    var newStudent = new Students { Login = login };
                    await context.Students.AddAsync(newStudent);
                    await context.SaveChangesAsync();
                    return newStudent;
                }
            }
        }


        public static async Task GetCode(string facultyName)
        {
            using (ApplicationContext context = new ApplicationContext()) 
            {
                var faculty = await context.Faculties.FirstOrDefaultAsync(x => x.Name == facultyName);
                if (faculty != null)
                {
                    Console.WriteLine("faculty_id:{1}\tName:{0}", faculty.Name, faculty.Id);
                    var codeFaculty = await context.Codes.FirstOrDefaultAsync(c => c.Faculty_Id == faculty.Id);
                    if (codeFaculty != null)
                    {
                        Console.WriteLine("id: {0}\tcode:{1}\tfaculty_id:{2}", codeFaculty.Id, codeFaculty.Code, codeFaculty.Faculty_Id);
                    }
                }
            }
        }
/*        public static async Task CodeStudent(string facultyName, string login)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var student = await context.Students.FirstOrDefaultAsync(x => x.Login == login);
                if (student != null)
                {
                    var faculty = await context.Faculties.FirstOrDefaultAsync(f => f.Name == facultyName);
                    if (faculty != null)
                    {
                        var newCode = await context.Codes.FirstOrDefaultAsync(c => c.)
                    }
                }
            }
        }*/

/*        public static async Task GetCode(string login, string code_id, int faculty_id)
        {
            using (ApplicationContext context = new ApplicationContext()) 
            {
                var student = await context.Students.FirstOrDefaultAsync(x => x.Login == login);
                if (student != null)
                {
                    var newStudent = new Students { Code_Id}
                }
            }
        }*/

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
                Console.WriteLine("{0} add", nameFaculty);
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

        public static async Task AddStudent(long idSudent)
        {
            using (ApplicationContext context = new ApplicationContext()) 
            {
                var student = await context.Students.FirstOrDefaultAsync(x => x.Chat_Id == idSudent);
                if(student is null)
                {
                    int codeId = 0;
                    int stageDialog = 0;
                    Students newStudent = new Students { Chat_Id = idSudent, Code_Id = codeId, StageDialog = stageDialog};
                    var result = context.Students.AddAsync(newStudent);
                    await context.SaveChangesAsync();
                }
            }
        }

        public static async Task AddStudentLogin(long chatId, string login)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var student = await context.Students.FirstOrDefaultAsync(x => x.Chat_Id == chatId);
                if (student is not null)
                {
                    student.Login = login;
                    student.StageDialog = 1;
                    context.Students.Update(student);
                    context.SaveChanges();
                }
            }
        }

        public static async Task<int> GetStatus(long chatId)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var status = await context.Students.FirstOrDefaultAsync(x => x.Chat_Id == chatId);
                if (status is not null)
                {
                    return status.StageDialog;
                }
                else
                {
                    return 404;
                }
            }
        }

        public static async Task AddStudentCode(long chatId, string facultyName)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var student = await context.Students.FirstOrDefaultAsync(s => s.Chat_Id == chatId);
                if (student is not null)
                {
                    var faculty = await context.Faculties.FirstOrDefaultAsync(f => f.Name == facultyName);
                    if (faculty is not null)
                    {
                        var code = await context.Codes.FirstOrDefaultAsync(c => c.Faculty_Id == faculty.Id);
                        if (code is not null)
                        {
                            student.Code_Id = code.Id;
                            student.StageDialog = 2;
                            context.Students.Update(student);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }

        public static async Task<string> GetCode(long chatId)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var student = await context.Students.FirstOrDefaultAsync(s => s.Chat_Id == chatId);
                if (student is not null)
                {
                    var code = await context.Codes.FirstOrDefaultAsync(c => c.Id == student.Code_Id);
                    if (code is not null)
                    {
                        return code.Code;
                    }
                    else
                    {
                        return "Failed: not found code";
                    }
                }
                else
                {
                    return "Failed: not found student";
                }
            }
        }

        public static async Task DeleteUsingCode(int codeId)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var record = await context.Codes.FirstOrDefaultAsync(x => x.Id == codeId);
                if (record is not null)
                {
                    context.Codes.Remove(record);
                    context.SaveChanges();
                }
            }
        }
    }
}
