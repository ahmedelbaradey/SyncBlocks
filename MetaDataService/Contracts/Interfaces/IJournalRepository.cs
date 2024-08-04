using Entities.DataModels;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IJournalRepository
    {
        Task<IEnumerable<Journal>> Get_SharedObject_Journals_Async(int sharedObject, RequestParameters requestParameters, bool trackChanges);
        Task<IEnumerable<Journal>> Get_User_SharedObject_Journals_Async(int userId, int sharedObject, RequestParameters requestParameters, bool trackChanges);
        Task<Journal> Get_Journal_Async(int journalId, bool trackChanges);
        Task<Journal> Get_SharedObject_Current_Journal_Async(int sharedObject, bool trackChanges);
        void Create_Journal(Journal journal);
    }
 }
