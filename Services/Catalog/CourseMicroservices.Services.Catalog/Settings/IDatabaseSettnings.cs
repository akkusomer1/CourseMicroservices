namespace CourseMicroservices.Services.Catalog.Settings
{
    public interface IDatabaseSettnings
    {
        string CourseCollectionName { get; set; }
        string CategoryCollectionName { get; set; }
        string ConnectionStrings { get; set; }
        string DatabaseName { get; set; }
    }
}
