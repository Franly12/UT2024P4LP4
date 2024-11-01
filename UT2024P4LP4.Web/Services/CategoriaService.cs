namespace UT2024P4LP4.Web.Services;

using Microsoft.EntityFrameworkCore;
using UT2024P4LP4.Web;
using UT2024P4LP4.Web.Data;
using UT2024P4LP4.Web.Data.Dtos;
using UT2024P4LP4.Web.Data.Entities;

public partial class CategoriaService : ICategoriaServices
{
    private readonly IApplicationDbContext dbContext;
    public CategoriaService (IApplicationDbContext dbContext)
    {
        this.dbContext=dbContext;
    }

    //crud
    public async Task<Result> Create(CategoriaRequest ctg)
    {
        try
        {
            var entity = Categoria.Create(ctg.Nombre);
            dbContext.Categorias.Add(entity);
            await dbContext.SaveChangesAsync();
            return Result.Success("✅Producto registrado con exito!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"☠️ Error: {Ex.Message}");
        }
    }
    public async Task<Result> Update(CategoriaRequest ctg)
    {
        try
        {
            var entity = dbContext.Categorias.Where(c => c.Id == ctg.Id).FirstOrDefault();
            if (entity == null)
                return Result.Failure($"El ctg '{ctg.Id}' no existe!");
            if (entity.Update(ctg.Nombre))
            {
                await dbContext.SaveChangesAsync();
                return Result.Success("✅ctg modificado con exito!");
            }
            return Result.Success("🐫 No has realizado ningun cambio!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"☠️ Error: {Ex.Message}");
        }
    }
    public async Task<Result> Delete(int Id)
    {
        try
        {
            var entity = dbContext.Categorias.Where(p => p.Id == Id).FirstOrDefault();
            if (entity == null)
                return Result.Failure($"El producto '{Id}' no existe!");
            dbContext.Categorias.Remove(entity);
            await dbContext.SaveChangesAsync();
            return Result.Success("✅Producto eliminado con exito!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"☠️ Error: {Ex.Message}");
        }
    }
    public async Task<ResultList<CategoriaDto>> GetAll(string filtro = "")
    {
        try
        {
            var entities = await dbContext.Categorias
                .Where(p => p.Nombre.ToLower().Contains(filtro.ToLower()))
                .Select(p => new CategoriaDto(p.Id, p.Nombre))
                .ToListAsync();
            return ResultList<CategoriaDto>.Success(entities);
        }
        catch (Exception Ex)
        {
            return ResultList<CategoriaDto>.Failure($"☠️ Error: {Ex.Message}");
        }
    }
}