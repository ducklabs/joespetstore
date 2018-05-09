using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoesPetStore.Models
{
    public class Receipt : IEntity
    {
        public int Id { get; set; }
        public int PetId { get; set; }
    }
}