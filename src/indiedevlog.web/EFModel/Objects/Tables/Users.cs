namespace indiedevlog.web.EFModel.Objects.Tables
{
    public class Users : BaseTable
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }

        public bool IsConfirmed { get; set; }        
    }
}