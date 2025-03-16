using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Data.Entities
{
    public partial class Department
    {
        public Department()
        {
            DepartmentSubjects = new HashSet<DepartmetSubject>();
            Instructors = new HashSet<Instructor>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int DID { get; set; }
        [StringLength(500)]
        public string DName { get; set; }
        public int? InsManger { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Student> Students { get; set; }
        [InverseProperty("Department")]

        public virtual ICollection<DepartmetSubject> DepartmentSubjects { get; set; }
        [InverseProperty("department")]
        public virtual ICollection<Instructor> Instructors { get; set; }
        [ForeignKey("InsManger")]
        [InverseProperty("departmentManager")]

        public virtual Instructor Instructor { get; set; }

    }

}
