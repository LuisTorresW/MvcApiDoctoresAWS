using Microsoft.AspNetCore.Mvc;
using MvcApiDoctoresAWS.Models;
using MvcApiDoctoresAWS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApiDoctoresAWS.Controllers
{
    public class DoctoresController : Controller
    {
        private ServiceDoctores service;

        public DoctoresController(ServiceDoctores service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> IndexServidor()
        {
            List<Doctor> doctores = await this.service.GetDoctoresAsync();
            return View(doctores);
        }

        public async Task<IActionResult> Details(string id)
        {
            Doctor doctor = await this.service.GetDoctoreAsync(id);
            return View(doctor);

        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult>Create(Doctor doctore)
        {
            await this.service.InsertDoctorAsync(doctore);
            return RedirectToAction("IndexServidor");
        }

        public async Task<IActionResult> Edit(string id)
        {
            Doctor doctor = await this.service.GetDoctoreAsync(id);
            return View(doctor);
        }
        [HttpPut]

        public async Task<IActionResult> Edit(Doctor doctor)
        {
            await this.service.UpdateDoctoresAsync(doctor);
            return RedirectToAction("IndexServidor");
        }


    }
}
