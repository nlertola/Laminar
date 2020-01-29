using Laminar.Data.Models;

namespace Laminar.Data
{
    public class EfRepository<T> : EntityRepository<T> where T : BaseModel
    {
        public EfRepository(LaminarContext context) : base(context)
        {

        }
    }
}
