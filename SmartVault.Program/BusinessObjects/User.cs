namespace SmartVault.Program.BusinessObjects
{
    public partial class User : BaseBussinessObject
    {
        public string FullName => $"{FirstName} {LastName}";
    }
}
