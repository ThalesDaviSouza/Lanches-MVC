using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            // Define uma sessão
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // Obtém m serviço do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

            // Obtém ou gera o id do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            // Atribui o id do carrinho na Sessão
            session.SetString("CarrinhoId", carrinhoId);

            // Retorna o carrinho com o contexto e o Id atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public async void AdicionarAoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                i => i.Lanche.LancheId == lanche.LancheId &&
                i.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };
                await _context.CarrinhoCompraItens.AddAsync(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            await _context.SaveChangesAsync();
        }

        public async void RemoverDoCarrinho(Lanche lanche)
        {
            var item =
                _context.CarrinhoCompraItens.SingleOrDefault(
                    i => i.Lanche.LancheId == lanche.LancheId &&
                        i.CarrinhoCompraId == CarrinhoCompraId);
            if (item != null)
            {
                if (item.Quantidade > 1)
                {
                    item.Quantidade--;
                }
                else
                {
                    _context.CarrinhoCompraItens.Remove(item);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CarrinhoCompraItem>> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItens ??
                (CarrinhoCompraItens =
                    await _context.CarrinhoCompraItens
                    .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                    .Include(c => c.Lanche)
                    .ToListAsync());
        }

        public async void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens
                                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId);

            _context.RemoveRange(carrinhoItens);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetCarrinhoCompraTotal()
        {
            var total = await 
                        _context.CarrinhoCompraItens
                        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                        .Select(c => c.Lanche.Preco * c.Quantidade).SumAsync();

            return total;
        }
    }
}
