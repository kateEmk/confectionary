namespace LibraryDatabaseCoffe.Models.DB.Tables
{
    public class Company
    {
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Owner { get; set; }
        public string Telephone { get; set; }
        public long BankingAccount { get; set; }
        public Company(int? company_id, string company_name, string owner, string telephone, long banking_account)
        {
            CompanyId = company_id;
            CompanyName = company_name;
            Owner = owner;
            Telephone = telephone;
            BankingAccount = banking_account;
        }
        public Company(int company_id, string company_name, string owner, string telephone, long banking_account)
        {
            CompanyId = company_id;
            CompanyName = company_name;
            Owner = owner;
            Telephone = telephone;
            BankingAccount = banking_account;
        }
        public List<SweetStaff> Staffs { get; set; } = new List<SweetStaff>();
    }
}
