using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        [HttpGet]
        public IActionResult Index(int id)
        {

            // TODO #1 - get the Job with the given ID and pass it into the view
           TechJobs.Models.Job newJob = jobData.Find(id);
            ViewBag.job = newJob;
            return View();
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            if (newJobViewModel.Name != null) {



                Employer employer = jobData.Employers.Find(newJobViewModel.EmployerID);
                Location location = jobData.Locations.Find(newJobViewModel.LocationID);
                CoreCompetency coreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID);
                PositionType position = jobData.PositionTypes.Find(newJobViewModel.PositionID);

                Job newJob = new Job {

                Name = newJobViewModel.Name,
                Employer = employer,
                Location = location,
                CoreCompetency = coreCompetency,
                PositionType = position
            };
                jobData.Jobs.Add(newJob);
                
                return Redirect("/Job?id=" + newJob.ID);
            }
            

            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

            return View(newJobViewModel);
        }
    }
}
