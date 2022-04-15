using JCMG.EntitasRedux;

namespace CorundumGames.Codegen.Redux.Runtime
{
    public interface IEntityBehaviour<in TEntity> where TEntity : IEntity
    {
        void AddComponents(TEntity entity);
    }
}
