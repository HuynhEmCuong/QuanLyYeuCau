using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Request.Data.EF.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Call save change from db context
        /// </summary>
        void SaveChange();

        Task SaveChangeAsync();
    }
}
