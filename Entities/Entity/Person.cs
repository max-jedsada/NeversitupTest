using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.Entities
{
    [Table("Person")]
    public class Person
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }
       
        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }
    }
}