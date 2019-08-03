using System.Collections.Generic;
using System.Linq;
using Logic.Entities;
using Logic.Utils;
using Logic.ValueObjects;

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

        public Athlete GetByEmail(Email email)
        {
            return _unitOfWork
                .Query<Athlete>()
                .SingleOrDefault(x => x.Email == email);
        }
    }
}
