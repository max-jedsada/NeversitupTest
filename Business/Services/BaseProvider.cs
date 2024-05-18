
using DAL.Context;

namespace Project.Provider
{
    public abstract class BaseProvider
    {
        protected ProjectContext _db;

        public BaseProvider(ProjectContext db)
        {
            _db = db;
        }

    }
}
