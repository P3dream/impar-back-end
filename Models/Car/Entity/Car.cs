using Microsoft.Extensions.Hosting;
using MiNET.Utils;
using System.ComponentModel.DataAnnotations;

namespace impar_back_end.Models.Car.Entity
{
    public class Car
    {

        public int Id { get; set; } 

        public int PhotoId { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }
    }
}
