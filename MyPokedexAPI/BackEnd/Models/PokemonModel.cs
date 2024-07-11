using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Models
{
  public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int RegionID { get; set; }
    public int BaseAttackPoints { get; set; }
    public int BaseHealthPoints { get; set; }
    public int BaseDefensePoints { get; set; }
    public int BaseSpeedPoints { get; set; }

}
}