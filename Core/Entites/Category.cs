using System.ComponentModel.DataAnnotations;
using Core.EntitesCore.Entites;

namespace Core.Entites
{
    public class Category : BaseEntity
    {
        [Required,MaxLength(225)]
        public string Name { get; set; }
    }
}