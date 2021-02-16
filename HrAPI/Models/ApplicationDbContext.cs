
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HrAPI.DTO;
using HrAPI.Models;

namespace HrAPI.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<LeaveRequest>()
            //.Property(e => e.LeavesFiles)
            //.HasConversion(
            //    v => string.Join(',', v),
            //    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Certificates> Certificates { get; set; }
        public DbSet<Compensation> Compensations { get; set; }
        public DbSet<CV_Bank> CV_Bank { get; set; }
        public DbSet<Evaluation> Evaluation { get; set; }
        public DbSet<EvaluationType> EvaluationTypes { get; set; }
        public DbSet<EvaluationProfession> EvaluationProfessions { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Excuse> Excuses { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeavesTypes> LeavesTypes { get; set; }
        public DbSet<MissionRequest> MissionRequests { get; set; }
        public DbSet<NeedsRequest> NeedsRequests { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<LeaveFiles> LeaveFiles { get; set; }
        public DbSet<NeedsCategory> NeedsCategory { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<TrainingType> TrainingTypes { get; set; }
        public DbSet<Courses>  courses { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<TrainingProfessions> TrainingProfessions { get; set; }

        public DbSet<Positions> Positions { get; set; }
        public DbSet<PositionLevel> PositionLevels { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<EmployeeDocuments> EmployeeDocuments { get; set; }
        public DbSet<FacultyDepartment> FacultyDepartments { get; set; }
        public DbSet<HrAPI.DTO.FacultyDTO> FacultyDTO { get; set; }
        public DbSet<HrAPI.DTO.FacultyDepartmentDTO> FacultyDepartmentDTO { get; set; }
        public DbSet<HrAPI.DTO.EmployeeDocumentsDTO> EmployeeDocumentsDTO { get; set; }

    }

}
