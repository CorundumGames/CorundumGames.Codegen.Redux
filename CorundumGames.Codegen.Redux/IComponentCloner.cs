using Entitas;

public interface IComponentCloner : IEntity
{
    void CloneComponent(int index, IComponent component);
}
