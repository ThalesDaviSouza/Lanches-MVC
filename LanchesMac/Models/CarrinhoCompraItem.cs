namespace LanchesMac.Models
{
    public class CarrinhoCompraItem
    {
        public int CarrinhoCompraItemID { get; set; }
        public Lanche Lanche { get; set; }
        public int Quantidade { get; set; }
        public string CarrinhoCompraId { get; set; }
    }
}
