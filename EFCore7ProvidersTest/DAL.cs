using System.ComponentModel.DataAnnotations;

namespace EFCore7ProvidersTest.DAL
{
    /// <summary>
    /// Учебный год
    /// </summary>
    public class StudyYear
    {
        //Идентификатор
        [Required]
        public Int64 Id { get; set; }
        //Год первой половины учебного года
        [Required]
        public Int32 Year { get; set; }
        //Описание
        public String Description { get; set; }

        //Список учебных перодов в этот год
        public virtual IList<StudyPeriod> StudyPeriods { get; set; }

        public override string ToString()
        {
            return $"{Year}/{Year + 1}";
        }
    }



    /// <summary>
    /// Учебный период
    /// </summary>
    public class StudyPeriod
    {
        //Идентификатор
        [Required]
        public Int64 Id { get; set; }
        //Учебный год
        [Required]
        public Int64 StudyYearId { get; set; }
        public virtual StudyYear StudyYear { get; set; }
        //Дата начала периода
        [Required]
        public DateTime StartDate { get; set; }
        //Дата окончания периода
        [Required]
        public DateTime EndDate { get; set; }
        //Описание
        public String Description { get; set; }


        public override string ToString()
        {
            return $"{StartDate.ToShortDateString()}-{EndDate.ToShortDateString()} {Description}";
        }
    }
}
