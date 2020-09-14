namespace ComponentWebApi.Model.Base
{
    public interface IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}