using lusttask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lusttask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirController : ControllerBase
    {
        private static readonly IList<Flights> Planes = new List<Flights>();

        [HttpGet]
        public IEnumerable<Flights> GetList() => Planes;


        [HttpPost]
        public Flights Add(PlaneRequestModel pln)
        {
            var plane = new Flights
            {
                id = Guid.NewGuid(),
                numflight = pln.numflight,
                type = pln.type,
                countPas = pln.countPas,
                pricePas = pln.pricePas,
                countCrew = pln.countCrew,
                priceCrew = pln.priceCrew,
                procDop = pln.procDop,
                eta = pln.eta,
                sum = (pln.countPas * pln.pricePas + pln.countCrew * pln.priceCrew) + pln.procDop
            };
            Planes.Add(plane);
            return plane;
        }


        [HttpDelete("{Id:guid}")]
        public bool Delete(Guid Id)
        {
            var targetplane = Planes.FirstOrDefault(x => x.id == Id);
            if (targetplane != null)
            {
                return Planes.Remove(targetplane);
            }
            return false;
        }


        [HttpPut("{Id:guid}")]
        public Flights Update([FromRoute] Guid Id, [FromBody] PlaneRequestModel pln)
        {
            var targetplane = Planes.FirstOrDefault(x => x.id == Id);
            if (targetplane != null)
            {
                targetplane.numflight = pln.numflight;
                targetplane.type = pln.type;
                targetplane.eta = pln.eta;
                targetplane.countPas = pln.countPas;
                targetplane.pricePas = pln.pricePas;
                targetplane.countCrew = pln.countCrew;
                targetplane.priceCrew = pln.priceCrew;
                targetplane.procDop = pln.procDop;
                targetplane.sum = (pln.countPas * pln.pricePas + pln.countCrew * pln.priceCrew) + pln.procDop;
            }
            return targetplane;
        }


        [HttpGet("статистика")]
        public StaticticAirport GetStats()
        {
            var stat = new StaticticAirport()
            {
                count = Planes.Count,
                countPassengers = Planes.Sum(x => x.countPas),
                countСrew = Planes.Sum(x => x.countCrew),
                countSum = Planes.Sum(x => x.sum)
            };
            return stat;
        }



    }
}
