using Entities.DataModels;
using Shared.DataTransferObjects.Journal;
using Shared.DataTransferObjects.SharedObject;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IJournalService
    {
        Task<PagedList<JournalDto>> Get_User_SharedObject_Journals_Async(int userId, int sharedObject, RequestParameters requestParameters, bool trackChanges);
        Task<PagedList<JournalDto>> Get_SharedObject_Journals_Async(int sharedObject, RequestParameters requestParameters, bool trackChanges);
        Task<JournalDto> Get_Journal_Async(int id, bool trackChanges);
        Task<JournalDto> Create_Journal(JournalForCreationDto journalforCreation);
    }
 }
