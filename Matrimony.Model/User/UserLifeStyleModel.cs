using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserLifeStyleModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? DietId { get; set; }
        public string Hobies { get; set; }
        public int? SmokingId { get; set; }
        public int? ChildrenChoiceId { get; set; }
        public int? WeadingStyleId { get; set; }
        public int? DrinkingId { get; set; }
        public int? HouseLivingInId { get; set; }
        public bool? OwnCar { get; set; }
        public bool? OwnPet { get; set; }
        public string Interests { get; set; }
        public string Musics { get; set; }
        public string Books { get; set; }
        public string Movies { get; set; }
        public string Fitness { get; set; }
        public string Cuisines { get; set; }
    }
}
