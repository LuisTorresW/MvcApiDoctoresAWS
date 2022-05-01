using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApiDoctoresAWS.Models
{
    
    public class Doctor
    {
       
        public string IdHospital { get; set; }
     
        public string IdDoctor { get; set; }

     

        public string Apellido { get; set; }

       

        public string Especialidad { get; set; }


        public int Salario { get; set; }

   

        public string Imagen { get; set; }
    }
}
