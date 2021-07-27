using FileTrackingSystem.BL.SchemaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FileTrackingSystem.BL.Contract
{
    public interface IDelete
    {
        Task<bool> DeleteComapny(int  id, string user);
        Task<bool> DeleteBranch(int id, string user);
        Task<bool> DeletetUser(int id, string user);
    }
}
