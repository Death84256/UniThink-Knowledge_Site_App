using Basecode.Data.Repositories;
using ASI.Basecode.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Repositories
{
    public class KnowledgeSiteAppRepository : BaseRepository, IKnowledgeSiteAppRepository
    {
        public KnowledgeSiteAppRepository(IUnitOfWork unitOfWork) : base(unitOfWork) 
        { 
        
        }
    }
}
