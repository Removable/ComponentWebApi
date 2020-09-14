using System;

namespace ComponentWebApi.Model.Base
{
    public interface ICreation
    {
        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        ///     创建者ID
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        ///     最后修改时间
        /// </summary>
        public DateTime LastModificationTime { get; set; }

        /// <summary>
        ///     最后修改者ID
        /// </summary>
        public string LastModifierId { get; set; }
    }
}