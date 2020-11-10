namespace ComponentWebApi.Model.Base
{
    /// <summary>
    /// 主键接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public TKey Id { get; set; }
    }
}