namespace Evolent.Model
{
    public class UserDetail
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public bool IsAdminUser { get; set; }
        public bool IsReadOnlyUser { get; set; }
        public bool IsAuthorized { get; set; }

    }
   
}
