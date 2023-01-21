namespace FurnitureShop.Models
{
    public class CartTable
    {
        public int Id { get; set; }
        public FurnProduct furnProduct { get; set; }
        public FurnPayment furnPayment { get; set; }
        public FurnProductOrder furnProductOrder { get; set; }
        public FurnUserAccount furnUserAccount { get; set; }
        public FurnOrder furnOrder { get; set; }

    }
}
