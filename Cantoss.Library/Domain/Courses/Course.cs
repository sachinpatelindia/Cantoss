namespace Cantoss.Library.Domain.Courses
{
    public class Course:CommonEntity
    {
        public Course()
        {
            Id = Guid.NewGuid().ToString();
            PartitionKey = nameof(Course);
        }
        public int CourseFormatId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; } 
        public int CourseDuration { get; set; }
        public int CourseCopmletionPercent { get; set; }
        public CourseFormat CourseFormat
        {
            get => (CourseFormat)CourseFormatId;
            set => CourseFormatId = (int)value;
        }
        public bool IsActive { get; set; } 

    }
}
