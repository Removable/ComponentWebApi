using ComponentWebApi.Model.Base;

namespace ComponentWebApi.Model.Articles
{
    public class Article : IEntity<int>
    {
        public int Id { get; set; }
    }
}