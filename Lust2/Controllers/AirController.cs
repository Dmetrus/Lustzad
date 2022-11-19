using ASPAirport.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPAirport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirController : ControllerBase
    {
        private static readonly IList<Flights> Airs = new List<Flights>();

        [HttpGet]
        public IEnumerable<Flights> GetList() => Airs;


        [HttpPost]
        public Flights Add(AirRequestModel model)
        {
            var air = new Flights
            {
                id = Guid.NewGuid(),
                numflight = model.numflight,
                type = model.type,
                countPas = model.countPas,
                pricePas = model.pricePas,
                countCrew = model.countCrew,
                priceCrew = model.priceCrew,
                procDop = model.procDop,
                eta = model.eta,
                sum = (model.countPas * model.pricePas + model.countCrew * model.priceCrew) + model.procDop
            };
            Airs.Add(air);
            return air;
        }


        [HttpDelete("{Id:guid}")]
        public bool Delete(Guid Id)
        {
            var taggetair = Airs.FirstOrDefault(x => x.id == Id);
            if (taggetair != null)
            {
                return Airs.Remove(taggetair);
            }
            return false;
        }


        [HttpPut("{Id:guid}")]
        public Flights Update([FromRoute] Guid Id, [FromBody] AirRequestModel model)
        {
            var targetair = Airs.FirstOrDefault(x => x.id == Id);
            if (targetair != null)
            {
                targetair.numflight = model.numflight;
                targetair.type = model.type;
                targetair.eta = model.eta;
                targetair.countPas = model.countPas;
                targetair.pricePas = model.pricePas;
                targetair.countCrew = model.countCrew;
                targetair.priceCrew = model.priceCrew;
                targetair.procDop = model.procDop;
                targetair.sum = (model.countPas * model.pricePas + model.countCrew * model.priceCrew) + model.procDop;
            }
            return targetair;
        }


        [HttpGet("статистика")]
        public StaticticAirport GetStats()
        {
            var stat = new StaticticAirport()
            {
                count = Airs.Count,
                countPassengers = Airs.Sum(x => x.countPas),
                countСrew = Airs.Sum(x => x.countCrew),
                countSum = Airs.Sum(x => x.sum)
            };
            return stat;
        }



    }
}
