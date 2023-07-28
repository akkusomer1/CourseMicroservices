namespace CourseMicroservices.Services.Catalog.Settings
{
    public class DatabaseSettnings : IDatabaseSettnings
    {
        public string CourseCollectionName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ConnectionStrings { get; set; }
        public string DatabaseName { get; set; }
    }
}
