namespace Cafemobile_Cafeteria.Models.System
{
    public class SoldMeal
    {
        public int MealId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public int sold_units { get; set; }
        public double sales => price * sold_units;
        public bool pending { get; set; } = true;
        public bool cooking { get; set; } = false;
        public bool prepared { get; set; } = false;
    }

}
