using Edument.CQRS;
using NDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBACNationals.ReadModels
{
    public abstract class AReadModel
    {
        protected string ReadModelFilePath = MBACNationalsReadModels.Properties.Settings.Default.ReadModelConnection;

        protected void Create<T>(T entity)
            where T : class, IEntity
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var exists = odb.QueryAndExecute<T>()
                                .Where(p => p.Id.Equals(entity.Id))
                                .FirstOrDefault();
                if (exists != null)
                    return;

                odb.Store(entity);
            }
        }

        protected T Read<T>(Func<T, bool> predicate)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                return Read(predicate, odb);
            }
        }

        protected T Read<T>(Func<T, bool> predicate, NDatabase.Api.IOdb odb)
        {
            if (odb == null)
                return Read(predicate);

            return odb.QueryAndExecute<T>()
                .Where(p => predicate(p))
                .FirstOrDefault();            
        }

        protected void Update<T>(Guid id, Action<T> func)
            where T : class, IEntity
        {
            Update<T>(id, (t, odb) => { func(t); });
        }

        protected void Update<T>(Guid id, Action<T, NDatabase.Api.IOdb> func)
            where T : class, IEntity
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var entity = odb.QueryAndExecute<T>()
                    .Where(p => p.Id.Equals(id))
                    .FirstOrDefault();

                if (entity == null)
                    return;

                func(entity, odb);
                odb.Store(entity);
            }
        }
    }
}
