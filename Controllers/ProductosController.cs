using Microsoft.AspNetCore.Mvc;

using FerreteriaAPI.Data;
using FerreteriaAPI.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly FerreteriaDbContext _context;

    public ProductosController(FerreteriaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
    {
        return await _context.Productos.Include(p => p.Categoria).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> GetProducto(int id)
    {
        var producto = await _context.Productos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (producto == null)
            return NotFound();

        return producto;
    }

    [HttpGet("categoria/{categoriaId}")]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductosPorCategoria(int categoriaId)
    {
        return await _context.Productos
            .Where(p => p.CategoriaId == categoriaId)
            .Include(p => p.Categoria)
            .ToListAsync();
    }
}
