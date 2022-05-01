using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcApiDoctoresAWS.Models;
using MvcApiDoctoresAWS.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApiDoctoresAWS.Controllers
{
    public class DoctoresController : Controller
    {
        private ServiceDoctores service;
        private ServiceS3 services;
        public DoctoresController(ServiceDoctores service,ServiceS3 services)
        {
            this.services = services;
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

        public async Task<IActionResult>Create(Doctor doctore,IFormFile ImagenBBDD)
        {
            string extension = ImagenBBDD.FileName.Split(".")[1];
            string fileName = doctore.IdDoctor.Trim() + "." + extension;
            using (Stream stream = ImagenBBDD.OpenReadStream())
            {
                await this.services.UploadFileAsync(stream, fileName);
            }
            doctore.Imagen = fileName;

            await this.service.InsertDoctorAsync(doctore);
            return RedirectToAction("IndexServidor");
        }

        public async Task<IActionResult> Edit(string id)
        {

            Doctor doctor = await this.service.GetDoctoreAsync(id);
            return View(doctor);
        }
        [HttpPut]

        public async Task<IActionResult> Edit(Doctor doctor, IFormFile ImagenBBDD)
        {
            string fileName = doctor.Imagen;
            if (ImagenBBDD != null)
            {
                string extension = ImagenBBDD.FileName.Split(".")[1];
                fileName = doctor.IdDoctor + "." + extension;
                using (Stream stream = ImagenBBDD.OpenReadStream())
                {
                    await this.services.UploadFileAsync(stream, fileName);
                }
            }
            doctor.Imagen = fileName;
            await this.service.UpdateDoctoresAsync(doctor);
            return RedirectToAction("IndexServidor");
        }

        public async Task<IActionResult> Delete(string id)
        {
            Doctor doc = await this.service.GetDoctoreAsync(id);
            await this.services.DeleteFileAsync(doc.Imagen);
            await this.service.DeleteDoctor(id);
            return RedirectToAction("IndexServidor");
        }


    }
}
