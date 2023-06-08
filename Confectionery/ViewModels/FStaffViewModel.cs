namespace Confectionery.ViewModels
{
    public class FStaffViewModel
    {
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public DateTime DateDeliver { get; set; }
        public float Weight { get; set; }
        public float Price { get; set; }
        public float Calories { get; set; }
        public string Classification { get; set; }
        public string Comapny { get; set; }

        public FStaffViewModel(int staffId, string staffName, DateTime dateDeliver, float weight, float price, float calories, string classification, string comapny)
        {
            StaffId = staffId;
            StaffName = staffName;
            DateDeliver = dateDeliver;
            Weight = weight;
            Price = price;
            Calories = calories;
            Classification = classification;
            Comapny = comapny;
        }

        public FStaffViewModel(int staffId)
        {
            StaffId = staffId;
        }

        public FStaffViewModel()
        {
        }
    }
}
