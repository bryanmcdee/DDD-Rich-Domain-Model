using System.Collections.Generic;
using System.Linq;
using Logic.Entities;
using Logic.Utils;

namespace Logic.Repositories
{
    public class WorkoutRoutineRepository : Repository<WorkoutRoutine>
    {
        public WorkoutRoutineRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IReadOnlyList<WorkoutRoutine> GetList()
        {
            return _unitOfWork.Query<WorkoutRoutine>().ToList();
        }
    }
}
