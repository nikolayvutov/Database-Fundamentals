using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    public class Position
    {
        public Position()
        {
            this.Players = new HashSet<Player>();
        }
        
        public int PositionId { get; set; }

        public string Name { get; set; }
        
        [InverseProperty("Position")]
        public ICollection<Player> Players { get; set; }
    }
}