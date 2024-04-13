using Cantoss.Library.Domain.Courses;

namespace Cantoss.Service.Courses
{
    public interface ICourseService
    {
        Course GetCourseById(string id);
        IList<Course> GetAll();

    }
}
