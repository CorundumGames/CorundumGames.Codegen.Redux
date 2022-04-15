using JCMG.EntitasRedux;

namespace CorundumGames.Codegen.Redux.Runtime
{
    public interface IEntityBehaviourPostInitialization<in TEntity> where TEntity : IEntity
    {
        void AfterComponentsAdded(TEntity entity);
    }
}
