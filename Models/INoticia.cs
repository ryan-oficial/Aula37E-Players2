using System.Collections.Generic;

namespace Aula37ASP_E_Players.Models
{
    public interface INoticia
    {
         void Create(Noticia e);
         List<Noticia> ReadAll();
         void Update(Noticia e);
         void Delete(int id);
    }
}