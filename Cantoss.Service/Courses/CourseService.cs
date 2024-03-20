using Cantoss.Library.Domain.Courses;

namespace Cantoss.Service.Courses
{
    public class CourseService : ICourseService
    {
        public IList<Course> GetAll()
        {
            return GetCourses();
        }

        public Course GetCourseById(int id)
        {
            return GetAll().FirstOrDefault(c=>c.Id==id);
        }

        private IList<Course> GetCourses()
        {
            return new List<Course>
            {
                new Course
                {
                    Id = 1,
                    AuthorId = 1,
                    Author = "SKP",
                    CourseCopmletionPercent = 1,
                    CourseDuration = 1,
                    CourseFormat = Library.CourseFormat.Offline,
                    Description = "Course description 1",
                    IsActive = true,
                    Name = "C#",
                    Title = "Learn C#"
                },
                 new Course
                {
                    Id = 2,
                    AuthorId = 2,
                    Author = "SKP",
                    CourseCopmletionPercent = 2,
                    CourseDuration = 2,
                    CourseFormat = Library.CourseFormat.VirtualClass,
                    Description = "Course description 2",
                    IsActive = true,
                    Name = "ASP.net",
                    Title = "Learn ASP.Net"
                },
                   new Course
                {
                    Id = 3,
                    AuthorId = 3,
                    Author = "SKP",
                    CourseCopmletionPercent = 3,
                    CourseDuration = 3,
                    CourseFormat = Library.CourseFormat.RecordedVideo,
                    Description = "Course description 3",
                    IsActive = true,
                    Name = ".net",
                    Title = "Learn .Net"
                }
            };
        }
    }
}
