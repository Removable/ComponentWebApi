namespace ComponentWebApi.Model.Base
{
    /// <summary>
    /// 删除属性接口
    /// </summary>
    public interface IDeletion
    {
        /// <summary>
        ///     逻辑删除标记
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}