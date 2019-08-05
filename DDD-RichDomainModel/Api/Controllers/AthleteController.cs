using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Dtos;
using Logic.Entities;
using Logic.Repositories;
using Logic.Services;
using Logic.ValueObjects;
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

            var dto = new AthleteDto()
            {
                Id = athlete.Id,
                Name = athlete.Name.Value,
                Email = athlete.Email.Value,
                Status = athlete.Status.Type.ToString(),
                StatusExpirationDate = athlete.Status.ExpirationDate,
                MoneySpent = athlete.MoneySpent,
                PurchasedWorkoutRoutine = athlete.PurchasedWorkoutRoutine.Select(s => new PurchasedWorkoutRoutineDto()
                {
                    WorkoutRoutine = new WorkoutRoutineDto()
                    {
                        Id = s.WorkoutRoutineId,
                        Name = s.WorkoutRoutine.Name
                    },
                    Price = s.Price,
                    PurchaseDate = s.PurchaseDate,
                    ExpirationDate = s.ExpirationDate
                }).ToList()
            };

            return Json(dto);
        }

        [HttpGet]
        public JsonResult GetList()
        {
            IReadOnlyList<Athlete> athletes = athleteRepository.GetList();
            var dto = athletes.Select(s => new AthleteInListDto()
            {
                Id = s.Id,
                Name = s.Name.Value,
                Email = s.Email.Value,
                Status = s.Status.Type.ToString(),
                StatusExpirationDate = s.Status.ExpirationDate,
                MoneySpent = s.MoneySpent,
            }).ToList();

            return Json(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAthleteDto item)
        {
            try
            {
                Result<AthleteName> athleteNameRequest = AthleteName.Create(item.Name);
                Result<Email> emailRequest = Email.Create(item.Email);
                Result result = Result.Combine(athleteNameRequest, emailRequest);
                if (result.IsFailure)
                {
                    return BadRequest(result.Error);
                }

                if (athleteRepository.GetByEmail(emailRequest.Value) != null)
                {
                    return BadRequest("Email is already in use: " + item.Email);
                }

                var athlete = new Athlete(athleteNameRequest.Value, emailRequest.Value);
                athleteRepository.Add(athlete);
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
        public IActionResult Update(long id, [FromBody] UpdateAthleteDto item)
        {
            try
            {
                Result<AthleteName> athleteNameRequest = AthleteName.Create(item.Name);
                if (athleteNameRequest.IsFailure)
                {
                    return BadRequest(athleteNameRequest.Error);
                }

                Athlete athlete = athleteRepository.GetById(id);
                if (athlete == null)
                {
                    return BadRequest("Invalid athlete Id: " + id);
                }

                athlete.Name = athleteNameRequest.Value;
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

                if (athlete.PurchasedWorkoutRoutine.Any(x => x.WorkoutRoutineId == workoutRoutine.Id && !x.ExpirationDate.IsExpired))
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

                if (athlete.Status.IsAdvanced)
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
