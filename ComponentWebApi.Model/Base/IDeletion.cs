namespace ComponentWebApi.Model.Base
{
    public interface IDeletion
    {
        /// <summary>
        ///     逻辑删除标记
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}