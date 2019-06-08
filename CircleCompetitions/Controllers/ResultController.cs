﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CircleCompetitions.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace CircleCompetitions.Controllers
{
    public class ResultController : Controller
    {
        DataContext db;
        static int CompetitionID;
        public ResultController(DataContext context)
        {
            db = context;
        }

        /*Методы на возврат данных*/
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public IEnumerable<Result> GetResults()
        {
            List<Result> Results = new List<Result>();
            foreach (Result result in db.Result.ToList())
            {
                if (result.Competition_ID == CompetitionID)
                {
                    Results.Add(result);
                }
            }
            return Results;
        }
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        //КОСЯК ЗДЕСЬ
        public IEnumerable<Sportsman> GetSportsmen()
        {
            List<Sportsman> Sportsmen = new List<Sportsman>();
            foreach (Result result in db.Result.ToList())
            {
                if (result.Competition_ID == CompetitionID)
                {
                    Sportsmen.Add(this.db.Sportsman.FirstOrDefault(s => s.ID_Sportsman == result.Sportsman_ID));
                }
            }
            return Sportsmen;
        }
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public IEnumerable<Competition> GetCompetition()
        {
            List<Competition> Competition = new List<Competition>();
            Competition.Add(db.Competition.FirstOrDefault(c => c.ID_Competition == CompetitionID));
            return Competition;
        }
        [HttpGet]
        [Authorize(Roles ="Admin, User")]
        public IEnumerable<Stage> GetStages()
        {
            List<Stage> Stages = new List<Stage>();
            //int temp = 0;
            foreach (Stage stage in db.Stage.ToList())
            {
                if(stage.Competition_ID == CompetitionID /*&& stage.StageNumber != temp*/)
                {
                    Stages.Add(stage);
                    //temp = stage.StageNumber;
                }
            }
            return Stages;
        }
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public IEnumerable<Circle> GetCircles()
        {
            List<Circle> Circles = new List<Circle>();
            foreach(Circle circle in db.Circle.OrderBy(c => c.Stage_ID).ToList())
            {
                if (circle.Competition_ID == CompetitionID)
                {
                    Circles.Add(circle);
                }
            }
            return Circles;
        }

        /*Методы на возврат представлений*/
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public IActionResult Completed(int IDCompetition)
        {
            CompetitionID = IDCompetition;
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public IActionResult DetailedLive(int CompetitionID)
        {
            //this.CompetitionID = CompetitionID;
            return View();
        }
        
    }
}