using Cantoss.Library.Domain.Courses;

namespace Cantoss.Service.Courses
{
    public class CourseService : ICourseService
    {
        public IList<Course> GetAll()
        {
            return GetCourses();
        }

        public Course GetCourseById(string id)
        {
            return GetAll().FirstOrDefault(c=>c.Id==id);
        }

        private IList<Course> GetCourses()
        {
            return new List<Course>
            {
                new Course
                {   Id = "a1888f63-9e97-4a6c-aa8d-920a19fc34a3",
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
                    Id="a1888f63-9e97-4a6c-aa8d-920a19fc34a2",
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
                    Id="a1888f63-9e97-4a6c-aa8d-920a19fc34a1",
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
