using Microsoft.EntityFrameworkCore;

namespace BlazorAppTest_01.Models.Candidates
{
    public class CandidateAppDbContext: DbContext
    {
        public CandidateAppDbContext() :base()
        {
                
        }

        public CandidateAppDbContext(DbContextOptions<CandidateAppDbContext> options) 
            : base(options)        
        {
                
        }
    }
}
