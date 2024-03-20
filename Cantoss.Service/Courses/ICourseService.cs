using Cantoss.Library.Domain.Courses;

namespace Cantoss.Service.Courses
{
    public interface ICourseService
    {
        Course GetCourseById(int id);
        IList<Course> GetAll();

    }
}
