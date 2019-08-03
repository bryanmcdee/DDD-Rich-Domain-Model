using System.Collections.Generic;
using System.Linq;
using Logic.Entities;
using Logic.Utils;

namespace Logic.Repositories
{
    public class AthleteRepository : Repository<Athlete>
    {
        public AthleteRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IReadOnlyList<Athlete> GetList()
        {
            return _unitOfWork
                .Query<Athlete>()
                .ToList();
        }

        public Athlete GetByEmail(string email)
        {
            return _unitOfWork
                .Query<Athlete>()
                .SingleOrDefault(x => x.Email.Value == email);
        }
    }
}
