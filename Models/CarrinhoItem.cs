namespace InduMovel.Models
{
    public class CarrinhoItem
    {
        public int CarrinhoItemId { get; set; }
        public Movel Movel { get; set; }
        public int Quantidade { get; set; }
        public string CarrinhoId { get; set; }
    }
}