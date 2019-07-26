using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Entities;
using Logic.Repositories;
using Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class AthleteController : Controller
    {
        private readonly WorkoutRoutineRepository workoutRoutineRepository;
        private readonly AthleteRepository athleteRepository;
        private readonly AthleteService athleteService;

        public AthleteController(WorkoutRoutineRepository workoutRoutineRepo, AthleteRepository athleteRepo, AthleteService athleteSvc)
        {
            athleteRepository = athleteRepo;
            workoutRoutineRepository = workoutRoutineRepo;
            athleteService = athleteSvc;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            Athlete athlete = athleteRepository.GetById(id);
            if (athlete == null)
            {
                return NotFound();
            }

            return Json(athlete);
        }

        [HttpGet]
        public JsonResult GetList()
        {
            IReadOnlyList<Athlete> athletes = athleteRepository.GetList();
            return Json(athletes);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Athlete item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (athleteRepository.GetByEmail(item.Email) != null)
                {
                    return BadRequest("Email is already in use: " + item.Email);
                }

                item.Id = 0;
                item.Status = AthleteStatusType.Regular;
                athleteRepository.Add(item);
                athleteRepository.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(long id, [FromBody] Athlete item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Athlete athlete = athleteRepository.GetById(id);
                if (athlete == null)
                {
                    return BadRequest("Invalid athlete Id: " + id);
                }

                athlete.Name = item.Name;
                athleteRepository.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPost]
        [Route("{id}/workoutroutine")]
        public IActionResult PurchaseWorkoutRoutine(long id, [FromBody] long workoutRoutineId)
        {
            try
            {
                WorkoutRoutine workoutRoutine = workoutRoutineRepository.GetById(workoutRoutineId);
                if (workoutRoutine == null)
                {
                    return BadRequest("Invalid workout routine Id: " + workoutRoutineId);
                }

                Athlete athlete = athleteRepository.GetById(id);
                if (athlete == null)
                {
                    return BadRequest("Invalid athlete Id: " + id);
                }

                if (athlete.PurchasedWorkoutRoutine.Any(x => x.WorkoutRoutineId == workoutRoutine.Id && (x.ExpirationDate == null || x.ExpirationDate.Value >= DateTime.UtcNow)))
                {
                    return BadRequest("The workout routine is already purchased: " + workoutRoutine.Name);
                }

                athleteService.PurchaseWorkoutRoutine(athlete, workoutRoutine);

                athleteRepository.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPost]
        [Route("{id}/upgrade")]
        public IActionResult UpgradeAthleteStatus(long id)
        {
            try
            {
                Athlete athlete = athleteRepository.GetById(id);
                if (athlete == null)
                {
                    return BadRequest("Invalid athlete Id: " + id);
                }

                if (athlete.Status == AthleteStatusType.Advanced && (athlete.StatusExpirationDate == null || athlete.StatusExpirationDate.Value < DateTime.UtcNow))
                {
                    return BadRequest("The athlete already has the Advanced status");
                }

                bool success = athleteService.UpgradeAthleteStatus(athlete);
                if (!success)
                {
                    return BadRequest("Cannot upgrade the athlete");
                }

                athleteRepository.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}
